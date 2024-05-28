using AFM_DLL.Models.BoardData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_Tests.TestData
{
    public static class TestBoards
    {
        public static Board GetBluePlayerPrioBoard() => 
            new(TestPlayers.GetHealthyRockPlayer(), TestPlayers.GetHealthyScissorsPlayer());
        public static Board GetRedPlayerPrioBoard() =>
            new(TestPlayers.GetHealthyRockPlayer(), TestPlayers.GetHealthyPaperPlayer());
    }
}
