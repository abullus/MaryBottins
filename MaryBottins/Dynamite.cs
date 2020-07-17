using System;
using BotInterface.Game;

namespace MaryBottins
{
    public class Dynamite
    {
        private int p1Dynamite;
        private int p2Dynamite;
        private int ConsecutiveDynamiteCount;

        private bool DynamiteJustThrown;
        //private int NonDrawDynamiteCount;

        public Dynamite()
        {
            p1Dynamite = 100;
            p2Dynamite = 100;
            DynamiteJustThrown = false;
            ConsecutiveDynamiteCount = 0;
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
                    ConsecutiveDynamiteCount++;
                }

                if (lastRound.GetP2() == Move.D)
                {
                    p2Dynamite--;
                }
            }
            else
            {
                DynamiteJustThrown = false;
                ConsecutiveDynamiteCount = 0;
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
                (1950 - gameplayScores.RoundNumber) / (p1Dynamite);
            // Calculate the number of rounds between each dynamite if thrown evenly throughout match
            decimal ProbOfThrowingInv;
            //decimal RoundsBetweenUsage = 19; //del

            if (gameplayScores.DrawCount > 0)
            {
                double NumberOfDrawsExponent = 1.5;
                if (p2Dynamite == 0)
                {
                    NumberOfDrawsExponent = 1.7;
                }

                double NumberOfDrawsFactor = 4;
                if (DynamiteJustThrown)
                {
                    NumberOfDrawsFactor = 2;
                }

                ProbOfThrowingInv = RoundsBetweenUsage /
                                    ((decimal) (Math.Pow(gameplayScores.DrawCount, NumberOfDrawsExponent) *
                                                NumberOfDrawsFactor));
                // Inverse of the probability we are aiming for to throw dynamite. 
            }
            else if (moveToMake == Move.D)
            {
                ProbOfThrowingInv = RoundsBetweenUsage / (decimal) 1.2;
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
                if (ConsecutiveDynamiteCount > 3)
                {
                    return Move.W;
                }
                return Move.D;
            }

            return Move.P;
        }
    }
}