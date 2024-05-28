using AFM_DLL.Models.PlayerInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_Tests.TestData
{
    public static class TestPlayers
    {
        public static PlayerGame GetHealthyRockPlayer() => new(TestDecks.GetRockDeck());
        public static PlayerGame GetHealthyPaperPlayer() => new(TestDecks.GetPaperDeck());
        public static PlayerGame GetHealthyScissorsPlayer() => new(TestDecks.GetScissorsDeck());

        public static PlayerGame GetLowRockPlayer()
        {
            var p = GetHealthyRockPlayer();
            p.RemoveHealth(9);
            return p;
        }
        public static PlayerGame GetLowPaperPlayer()
        {
            var p = GetHealthyPaperPlayer();
            p.RemoveHealth(9);
            return p;
        }
        public static PlayerGame GetLowScissorsPlayer()
        {
            var p = GetHealthyScissorsPlayer();
            p.RemoveHealth(9);
            return p;
        }

    }
}
