using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Game.Domain
{
    [BsonIgnoreExtraElements]
    public class GameTurnEntity
    {
        // [BsonElement]
        // public IReadOnlyCollection<(Guid user, PlayerDecision decision)> Players { get; }

        [BsonElement]
        public Guid FirstPlayer;
        [BsonElement]
        public Guid SecondPlayer;
        [BsonElement]
        public PlayerDecision FirstPlayerDecision;
        [BsonElement]
        public PlayerDecision SecondPlayerDecision;

        [BsonElement]
        public Guid Winner{ get; }
        
        [BsonElement]
        public Guid GameId { get; }
        
        [BsonElement]
        public DateTime Time { get; }

        // private static int TurnCounter;
        
        [BsonConstructor]
        public GameTurnEntity(Guid winner, Guid gameId, PlayerDecision firstPlayerDecision, PlayerDecision secondPlayerDecision, Guid secondPlayer, Guid firstPlayer, DateTime time=default)
        {
            Winner = winner;
            GameId = gameId;
            FirstPlayerDecision = firstPlayerDecision;
            SecondPlayerDecision = secondPlayerDecision;
            SecondPlayer = secondPlayer;
            FirstPlayer = firstPlayer;
            Time = time == default ? DateTime.Now : time;
        }
    }
}