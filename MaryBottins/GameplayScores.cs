using System;
using BotInterface.Game;

namespace MaryBottins
{
    public class GameplayScores
    {
        public int RoundNumber;
        private int p1Score;
        private int p2Score;
        public int DrawCount;
        public decimal EstimatedGameLength;

        public GameplayScores()
        {
            RoundNumber = 0;
            p1Score = 0;
            p2Score = 0;
            DrawCount = 0;
            EstimatedGameLength = 0;
        }

        public void UpdateGameplay(Round lastRound)
        {
            RoundNumber++;
            EstimateGameLength();
            Move p1Move = lastRound.GetP1();
            Move p2Move = lastRound.GetP2();
            switch (p1Move)
            {
                case Move.R:
                    if (p2Move == Move.P || p2Move == Move.D)
                    {
                        Loss();
                    }
                    else if (p2Move == Move.R)
                    {
                        Draw();
                    }
                    else
                    {
                        Win();
                    }
                    break;
                case Move.P:
                    if (p2Move == Move.S || p2Move == Move.D)
                    {
                        Loss();
                    }
                    else if (p2Move == Move.P)
                    {
                        Draw();
                    }
                    else
                    {
                        Win();
                    }
                    break;
                case Move.S:
                    if (p2Move == Move.R || p2Move == Move.D)
                    {
                        Loss();
                    }
                    else if (p2Move == Move.S)
                    {
                        Draw();
                    }
                    else
                    {
                        Win();
                    }
                    break;
                case Move.W:
                    if (p2Move == Move.D)
                    {
                        Win();
                    }
                    else if (p2Move == Move.W)
                    {
                        Draw();
                    }
                    else
                    {
                        Loss();
                    }
                    break;
                case Move.D:
                    if (p2Move == Move.W)
                    {
                        Loss();
                    }
                    else if (p2Move == Move.D)
                    {
                        Draw();
                    }
                    else
                    {
                        Win();
                    }
                    break;
                default:
                    break;
            }
        }

        public void EstimateGameLength()
        {
            if (RoundNumber < 100)
            {
                EstimatedGameLength = 1900;
            }
            else
            {
                int maxScore = Math.Max(p1Score, p2Score);
                int maxScore2 = Math.Max(maxScore, 1);
                EstimatedGameLength = (1000 * RoundNumber) / maxScore2;
            }
        }

        private void Loss()
        {
            p1Score--;
            p2Score++;
            DrawCount = 0;
        }
        private void Win()
        {
            p1Score++;
            p2Score--;
            DrawCount = 0;
        }
        private void Draw()
        {
            DrawCount++;
        }
    }
}