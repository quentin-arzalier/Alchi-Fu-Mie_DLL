using AFM_DLL;
using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Cards;
using AFM_DLL.Models.Enum;
using AFM_Tests.TestData;

namespace AFM_Tests
{
    public class SpellTests
    {
        #region Add Mana From Element

        private void AddManaFromElementSpellTest(Element bluePlayerElement, Element redPlayerElement, Element spellElement)
        {
            var board = new Board(TestPlayers.GetElementPlayer(bluePlayerElement), TestPlayers.GetElementPlayer(redPlayerElement));
            TestBoards.FillBoardWithCardsFromDrawPhase(board);

            var expectedMana = 0;
            if (bluePlayerElement == spellElement)
                expectedMana += 3;
            if (redPlayerElement == spellElement)
                expectedMana += 3;


            var side = board.GetAllyBoardSide(true);
            side.Player.AddMana(1);

            side.Player.Hand.Spells[0] = SpellCard.FromType(spellElement switch
            {
                Element.ROCK => SpellType.ADD_MANA_FROM_ROCK,
                Element.PAPER => SpellType.ADD_MANA_FROM_PAPER,
                Element.SCISSORS => SpellType.ADD_MANA_FROM_SCISSORS,
                _ => throw new ArgumentException()
            })
                ;
            side.Player.Hand.Spells[0].AddToBoard(board, true, null);
            board.SetSideReady(true);
            board.SetSideReady(false);

            board.EvaluateSpells();

            Assert.That(side.Player.ManaPoints, Is.EqualTo(expectedMana));
        }



        [TestCase(Element.PAPER, Element.PAPER)]
        [TestCase(Element.PAPER, Element.ROCK)]
        [TestCase(Element.PAPER, Element.SCISSORS)]
        [TestCase(Element.ROCK, Element.PAPER)]
        [TestCase(Element.ROCK, Element.ROCK)]
        [TestCase(Element.ROCK, Element.SCISSORS)]
        [TestCase(Element.SCISSORS, Element.PAPER)]
        [TestCase(Element.SCISSORS, Element.ROCK)]
        [TestCase(Element.SCISSORS, Element.SCISSORS)]
        public void AddManaFromPaperSpellTest(Element bluePlayerElement, Element redPlayerElement)
        {
            AddManaFromElementSpellTest(bluePlayerElement, redPlayerElement, Element.PAPER);
        }

        [TestCase(Element.PAPER, Element.PAPER)]
        [TestCase(Element.PAPER, Element.ROCK)]
        [TestCase(Element.PAPER, Element.SCISSORS)]
        [TestCase(Element.ROCK, Element.PAPER)]
        [TestCase(Element.ROCK, Element.ROCK)]
        [TestCase(Element.ROCK, Element.SCISSORS)]
        [TestCase(Element.SCISSORS, Element.PAPER)]
        [TestCase(Element.SCISSORS, Element.ROCK)]
        [TestCase(Element.SCISSORS, Element.SCISSORS)]
        public void AddManaFromRockSpellTest(Element bluePlayerElement, Element redPlayerElement)
        {
            AddManaFromElementSpellTest(bluePlayerElement, redPlayerElement, Element.ROCK);
        }

        [TestCase(Element.PAPER, Element.PAPER)]
        [TestCase(Element.PAPER, Element.ROCK)]
        [TestCase(Element.PAPER, Element.SCISSORS)]
        [TestCase(Element.ROCK, Element.PAPER)]
        [TestCase(Element.ROCK, Element.ROCK)]
        [TestCase(Element.ROCK, Element.SCISSORS)]
        [TestCase(Element.SCISSORS, Element.PAPER)]
        [TestCase(Element.SCISSORS, Element.ROCK)]
        [TestCase(Element.SCISSORS, Element.SCISSORS)]
        public void AddManaFromScissorsSpellTest(Element bluePlayerElement, Element redPlayerElement)
        {
            AddManaFromElementSpellTest(bluePlayerElement, redPlayerElement, Element.SCISSORS);
        }

        #endregion


    }
}
