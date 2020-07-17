using BotInterface.Game;

namespace MaryBottins
{
    public class Dynamite
    {
        private int p1Dynamite;
        private int p2Dynamite;
        private bool DynamiteJustThrown;
        private int NonDrawDynamiteCount;

        public Dynamite()
        {
            p1Dynamite = 100;
            p2Dynamite = 100;
            DynamiteJustThrown = false;
            NonDrawDynamiteCount = 0;
        }

        public Move PlayDynamite()
        {
            if (p1Dynamite > 0)
            {
                return Move.D;
            }
            return Move.P;
        }
        
        public void UpdateDynamite(Round lastRound, GameplayScores gameplayScores)
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
        }

        public void ShouldDynamiteBePlayed()
        {
            
        }
    }
}