using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Game.Domain
{
    public class MongoGameTurnRepository : IGameTurnRepository
    {
        private readonly IMongoCollection<GameTurnEntity> turnCollection;
        public const string CollectionName = "turns";
        
        public MongoGameTurnRepository(IMongoDatabase db)
        {
            turnCollection = db.GetCollection<GameTurnEntity>(CollectionName);
            var options = new CreateIndexOptions { Unique = true };
            turnCollection.Indexes.CreateOne("{ GameId : 1, Time : 1 }", options);
        }
        
        public GameTurnEntity Insert(GameTurnEntity turn)
        {
            turnCollection.InsertOne(turn);
            return turn;
        }

        public List<GameTurnEntity> FindLast(Guid gameId, int count)
        {
            var result =  turnCollection.Find(x => x.GameId == gameId)
                .SortByDescending(x => x.Time)
                .Limit(count)
                .ToList();
            // result.Reverse();
            return result;
        }
    }
}