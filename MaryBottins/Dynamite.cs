using System;
using BotInterface.Game;

namespace MaryBottins
{
    public class Dynamite
    {
        private int p1Dynamite;
        private int p2Dynamite;

        private bool DynamiteJustThrown;
        //private int NonDrawDynamiteCount;

        public Dynamite()
        {
            p1Dynamite = 100;
            p2Dynamite = 100;
            DynamiteJustThrown = false;
            //NonDrawDynamiteCount = 0;
        }

        public Move PlayDynamite()
        {
            if (p1Dynamite > 0)
            {
                return Move.D;
            }

            return Move.P;
        }

        public void UpdateDynamite(Round lastRound)
        {
            if (lastRound.GetP1() == Move.D || lastRound.GetP2() == Move.D)
            {
                DynamiteJustThrown = true;
                if (lastRound.GetP1() == Move.D)
                {
                    p1Dynamite--;
                }

                if (lastRound.GetP2() == Move.D)
                {
                    p2Dynamite--;
                }
            }
            else
            {
                DynamiteJustThrown = false;
            }
        }

        public Move ShouldDynamiteBePlayed(GameplayScores gameplayScores, Move moveToMake)
        {
            if (p1Dynamite == 0)
            {
                if (moveToMake == Move.D)
                {
                    return Move.P;
                }

                return moveToMake;
            }

            decimal RoundsBetweenUsage =
                (gameplayScores.EstimatedGameLength - gameplayScores.RoundNumber) / (p1Dynamite);
            // Calculate the number of rounds between each dynamite if thrown evenly throughout match
            decimal ProbOfThrowingInv;
            if (gameplayScores.DrawCount > 0)
            {
                double NumberOfDrawsExponent = 1.3;
                if (p2Dynamite == 0)
                {
                    NumberOfDrawsExponent = 1.5;
                }
                ProbOfThrowingInv = RoundsBetweenUsage / ((decimal) (Math.Pow(gameplayScores.DrawCount, NumberOfDrawsExponent) * 5));
                // Inverse of the probability we are aiming for to throw dynamite. 
            }
            else if (moveToMake == Move.D)
            {
                ProbOfThrowingInv = RoundsBetweenUsage / 3;
            }
            else
            {
                return moveToMake;
            }

            int RoundedProbabilityInv = (int) Math.Round(ProbOfThrowingInv);
            int FinalProbability = Math.Max(RoundedProbabilityInv, 1);
            var rand = new Random();
            int pick = rand.Next(FinalProbability);
            if (pick == 0)
            {
                if (DynamiteJustThrown)
                {
                    return moveToMake;
                }

                return Move.D;
            }

            return Move.P;
        }
    }
}