using System;
using MongoDB.Driver;

namespace Game.Domain
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> userCollection;
        public const string CollectionName = "users";

        public MongoUserRepository(IMongoDatabase database)
        {
            userCollection = database.GetCollection<UserEntity>(CollectionName);
            var options = new CreateIndexOptions { Unique = true };
            userCollection.Indexes.CreateOne("{ Login : 1 }", options);
        }

        public UserEntity Insert(UserEntity user)
        {
            userCollection.InsertOne(user);
            return user;
        }

        public UserEntity FindById(Guid id)
        {
            return userCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public UserEntity GetOrCreateByLogin(string login)
        {
            var user =  userCollection.Find(x => x.Login == login).FirstOrDefault();
            if (user is not null) return user;
            
            user = new UserEntity(Guid.NewGuid(), login, null, null, 0, null);
            userCollection.InsertOne(user);
            return user;

        }

        public void Update(UserEntity user)
        {
            userCollection.ReplaceOne(x => x.Id == user.Id, user);
        }

        public void Delete(Guid id)
        {
            userCollection.DeleteOne(x => x.Id == id);
        }

        // Для вывода списка всех пользователей (упорядоченных по логину)
        // страницы нумеруются с единицы
        public PageList<UserEntity> GetPage(int pageNumber, int pageSize)
        {
            var all = userCollection.Find(x => true);
            return new PageList<UserEntity>(all.SortBy(x => x.Login).Skip(pageSize * (pageNumber - 1)).Limit(pageSize).ToList(), pageSize + 1, pageNumber, pageSize);
        }

        // Не нужно реализовывать этот метод-
        public void UpdateOrInsert(UserEntity user, out bool isInserted)
        {
            throw new NotImplementedException();
        }
    }
}