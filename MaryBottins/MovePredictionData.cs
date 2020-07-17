using System;
using System.Collections.Generic;
using System.Linq;
using BotInterface.Game;

namespace MaryBottins
{
    public class MovePredictionData
    {
        private Dictionary<Move, double> FollowingMoveCount { get; set; }
        public int Usages;
        public Move ThisMove;

        public MovePredictionData(Move move)
        {
            Dictionary<Move, double> followingMoveCount = new Dictionary<Move, double>()
            {
                {Move.R, 0},
                {Move.P, 0},
                {Move.S, 0},
                {Move.W, 0},
                {Move.D, 0},
            };

            FollowingMoveCount = followingMoveCount;
            Usages = 0;
            ThisMove = move;
        }

        public void UpdateCounts(Move move)
        {
            FollowingMoveCount[Move.R] = FollowingMoveCount[Move.R] * 0.95;
            FollowingMoveCount[Move.P] = FollowingMoveCount[Move.P] * 0.95;
            FollowingMoveCount[Move.S] = FollowingMoveCount[Move.S] * 0.95;
            FollowingMoveCount[Move.W] = FollowingMoveCount[Move.W] * 0.95;
            FollowingMoveCount[Move.D] = FollowingMoveCount[Move.D] * 0.95;
            FollowingMoveCount[move]++;
            Usages++;
        }

        public Move PredictAMove()
        {
            return FollowingMoveCount.FirstOrDefault(x => x.Value == FollowingMoveCount.Values.Max()).Key;
        }

        
    }
}