using System;
using System.Collections.Generic;
using System.Linq;
using BotInterface.Bot;
using BotInterface.Game;

namespace MaryBottins
{
    public class MaryBottins : IBot
    {
        public Move MakeMove(Gamestate gamestate)
        {
            Round[] rounds = gamestate.GetRounds();
            if (rounds != null && rounds.Length > 2)
            {
                Move suggestedMove;
                Move predictedMove;
                UpdateClasses(rounds);
                var rand = new Random();
                int pick = rand.Next(5);
                switch (pick)
                {
                    case 0:
                    case 1:
                        case 2:
                        predictedMove = PredictOpponentMoves[rounds.Last().GetP2()].PredictAMove();
                        suggestedMove = SuggestMove.CounterTheirMove(predictedMove);
                        break;
                    
                    case 3:
                        predictedMove = PredictMyMoves[rounds.Last().GetP1()].PredictAMove();
                        suggestedMove = SuggestMove.CounterMyMove(predictedMove);
                        break;
                    case 4:
                        suggestedMove = SuggestMove.GuessAMove();
                        break;
                    default:
                        suggestedMove = Move.P;
                        break;
                }

                var moveToMake = Dynamite.ShouldDynamiteBePlayed(GameplayScores, suggestedMove);
                return moveToMake;
            }

            return Move.P;
        }

        public SuggestMove SuggestMove;
        public GameplayScores GameplayScores;
        public Dynamite Dynamite;
        public Dictionary<Move, MovePredictionData> PredictOpponentMoves { get; set; }
        public Dictionary<Move, MovePredictionData> PredictMyMoves { get; set; }
        public Move[] MoveArray;

        public MaryBottins()
        {
            Dynamite = new Dynamite();
            GameplayScores = new GameplayScores();
            PredictOpponentMoves = new Dictionary<Move, MovePredictionData>();
            PredictMyMoves = new Dictionary<Move, MovePredictionData>();
            MoveArray = new[] {Move.R, Move.P, Move.S, Move.W, Move.D};
            SuggestMove = new SuggestMove();
            foreach (var move in MoveArray)
            {
                MovePredictionData p1predictMoves = new MovePredictionData(move);
                MovePredictionData p2predictMoves = new MovePredictionData(move);
                PredictMyMoves.Add(move, p1predictMoves);
                PredictOpponentMoves.Add(move, p2predictMoves);
            }
        }

        public void UpdateClasses(Round[] rounds)
        {
            Round secondLastRound = rounds[rounds.Length - 2];
            Round lastRound = rounds.Last();
            GameplayScores.UpdateGameplay(lastRound);
            Dynamite.UpdateDynamite(lastRound);
            PredictMyMoves[secondLastRound.GetP1()].UpdateCounts(lastRound.GetP1());
            PredictOpponentMoves[secondLastRound.GetP2()].UpdateCounts(lastRound.GetP2());
        }
    }
}