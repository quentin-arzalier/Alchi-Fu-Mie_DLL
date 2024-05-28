using AFM_DLL;
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
        public static Board GetBluePlayerPrioBoardInDrawPhase() => 
            new(TestPlayers.GetRockPlayer(), TestPlayers.GetScissorsPlayer());
        public static Board GetRedPlayerPrioBoardInDrawPhase() =>
            new(TestPlayers.GetRockPlayer(), TestPlayers.GetPaperPlayer());

        public static Board GetBoardFullOfElementInPlayPhase(Element elt)
        {
            var board = new Board(TestPlayers.GetElementPlayer(elt), TestPlayers.GetElementPlayer(elt));
            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);

            board.DrawCards();

            foreach (var position in Enum.GetValues<BoardPosition>())
            {
                bs.Player.Hand.Elements[0].AddToBoard(board, true, position);
                rs.Player.Hand.Elements[0].AddToBoard(board, false, position);
            }

            return board;
        }

        public static Board GetFullBoardInResetPhase()
        {
            var board = GetBluePlayerPrioBoardInDrawPhase();
            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);
            bs.Player.AddMana(10);
            rs.Player.AddMana(10);

            board.DrawCards();

            foreach (var position in Enum.GetValues<BoardPosition>())
            {
                bs.Player.Hand.Elements[0].AddToBoard(board, true, position);
                rs.Player.Hand.Elements[0].AddToBoard(board, false, position);
            }

            bs.Player.Hand.Spells[0].AddToBoard(board, true, null);
            rs.Player.Hand.Spells[0].AddToBoard(board, false, null);

            board.SetSideReady(isBlueSide: true);
            board.SetSideReady(isBlueSide: false);

            board.EvaluateSpells();
            board.EvaluateCardColumns();

            return board;
        }
    }
}
