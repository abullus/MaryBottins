using System;
using BotInterface.Game;

namespace MaryBottins
{
    public class SuggestMove
    {
        public Move GuessAMove()
        {
            var rand = new Random();
            int pick = rand.Next(3);
            switch (pick)
            {
                case 0:
                    return Move.R;
                case 1:
                    return Move.P;
                case 2:
                    return Move.S;
                default:
                    return Move.R;
            }
        }

        public Move CounterTheirMove(Move move)
        {
            switch (move)
            {
                case Move.R:
                    return Move.P;
                case Move.P:
                    return Move.S;
                case Move.S:
                    return Move.R;
                case Move.W:
                    return Move.D;
                case Move.D:
                    return Move.D;
                default:
                    return Move.P;
            }
        }

        public Move CounterMyMove(Move move)
        {
            switch (move)
            {
                case Move.R:
                    return Move.S;
                case Move.P:
                    return Move.R;
                case Move.S:
                    return Move.P;
                case Move.W:
                    return Move.D;
                case Move.D:
                    return Move.D;
                default:
                    return Move.P;
            }
        }
    }
}