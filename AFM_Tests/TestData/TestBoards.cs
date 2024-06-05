using AFM_DLL;
using AFM_DLL.Models.BoardData;

namespace AFM_Tests.TestData
{
    public static class TestBoards
    {
        public static Board GetBluePlayerPrioBoardInDrawPhase() =>
            new(TestPlayers.GetRockPlayer(), TestPlayers.GetScissorsPlayer());
        public static Board GetRedPlayerPrioBoardInDrawPhase() =>
            new(TestPlayers.GetRockPlayer(), TestPlayers.GetPaperPlayer());

        public static void FillBoardWithCardsFromDrawPhase(Board board)
        {
            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);

            board.DrawCards();

            foreach (var position in Enum.GetValues<BoardPosition>())
            {
                bs.Player.Hand.Elements[0].AddToBoard(board, true, position);
                rs.Player.Hand.Elements[0].AddToBoard(board, false, position);
            }
        }

        public static Board GetBoardFullOfElementInPlayPhase(Element elt)
        {
            var board = new Board(TestPlayers.FromElement(elt), TestPlayers.FromElement(elt));
            FillBoardWithCardsFromDrawPhase(board);

            return board;
        }

        public static Board GetFullBoardInResetPhase()
        {
            var board = GetBluePlayerPrioBoardInDrawPhase();
            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);
            bs.Player.AddMana(10);
            rs.Player.AddMana(10);

            FillBoardWithCardsFromDrawPhase(board);

            bs.Player.Hand.Spells[0].AddToBoard(board, true, null);
            rs.Player.Hand.Spells[0].AddToBoard(board, false, null);

            board.SetSideReady(isBlueSide: true);
            board.SetSideReady(isBlueSide: false);

            var res = board.EvaluateSpells();
            if (res.HasMoreSpells)
                board.EvaluateSpells();
            board.EvaluateCardColumns();

            return board;
        }
    }
}
