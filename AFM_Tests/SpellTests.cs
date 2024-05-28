using AFM_DLL;
using AFM_DLL.Models.Cards;
using AFM_Tests.TestData;

namespace AFM_Tests
{
    public class SpellTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void AddManaFromPaperSpellTest(bool isPaperBoard)
        {
            var board = isPaperBoard
                ? TestBoards.GetBoardFullOfElementInPlayPhase(Element.PAPER)
                : TestBoards.GetBoardFullOfElementInPlayPhase(Element.ROCK);

            var side = board.GetAllyBoardSide(true);
            side.Player.AddMana(1);

            side.Player.Hand.Spells[0] = SpellCard.FromType(AFM_DLL.Models.Enum.SpellType.ADD_MANA_FROM_PAPER);
            side.Player.Hand.Spells[0].AddToBoard(board, true, null);
            board.SetSideReady(true);
            board.SetSideReady(false);

            board.EvaluateSpells();

            Assert.That(side.Player.ManaPoints, Is.EqualTo(isPaperBoard ? 6 : 0));
        }
    }
}
