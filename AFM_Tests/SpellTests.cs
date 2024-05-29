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
            var board = new Board(TestPlayers.FromElement(bluePlayerElement), TestPlayers.FromElement(redPlayerElement));
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
            });

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

        #region Transform enemy elements to element

        private void TransformEnemyElementIntoElement(Element bluePlayerElement, Element redPlayerElement, Element sourceElement, Element targetElement, bool isBlueSideSpell)
        {
            var board = new Board(TestPlayers.FromElement(bluePlayerElement), TestPlayers.FromElement(redPlayerElement));
            TestBoards.FillBoardWithCardsFromDrawPhase(board);

            var expectedCardCount = 0;

            var toCompare = isBlueSideSpell ? redPlayerElement : bluePlayerElement;
            if (toCompare == targetElement || toCompare == sourceElement)
                expectedCardCount = 3;


            var side = board.GetAllyBoardSide(isBlueSideSpell);
            var enemySide = board.GetEnemyBoardSide(isBlueSideSpell);
            side.Player.AddMana(2);

            side.Player.Hand.Spells[0] = SpellCard.FromType(sourceElement switch
            {
                Element.ROCK when targetElement == Element.SCISSORS => SpellType.REPLACE_ENEMY_ROCK_WITH_SCISSORS,
                Element.ROCK when targetElement == Element.PAPER => throw new NotImplementedException(),

                Element.PAPER when targetElement == Element.ROCK => throw new NotImplementedException(),
                Element.PAPER when targetElement == Element.SCISSORS => throw new NotImplementedException(),

                Element.SCISSORS when targetElement == Element.ROCK => throw new NotImplementedException(),
                Element.SCISSORS when targetElement == Element.PAPER => throw new NotImplementedException(),

                _ => throw new ArgumentException()
            });

            side.Player.Hand.Spells[0].AddToBoard(board, isBlueSideSpell, null);
            board.SetSideReady(true);
            board.SetSideReady(false);

            board.EvaluateSpells();

            Assert.That(enemySide.AllElementsOfSide.Count(card => card.ActiveElement == targetElement), Is.EqualTo(expectedCardCount));
        }

        [TestCase(Element.PAPER, Element.PAPER, false)]
        [TestCase(Element.PAPER, Element.ROCK, false)]
        [TestCase(Element.PAPER, Element.SCISSORS, false)]
        [TestCase(Element.ROCK, Element.PAPER, false)]
        [TestCase(Element.ROCK, Element.ROCK, false)]
        [TestCase(Element.ROCK, Element.SCISSORS, false)]
        [TestCase(Element.SCISSORS, Element.PAPER, false)]
        [TestCase(Element.SCISSORS, Element.ROCK, false)]
        [TestCase(Element.SCISSORS, Element.SCISSORS, false)]
        [TestCase(Element.PAPER, Element.PAPER, true)]
        [TestCase(Element.PAPER, Element.ROCK, true)]
        [TestCase(Element.PAPER, Element.SCISSORS, true)]
        [TestCase(Element.ROCK, Element.PAPER, true)]
        [TestCase(Element.ROCK, Element.ROCK, true)]
        [TestCase(Element.ROCK, Element.SCISSORS, true)]
        [TestCase(Element.SCISSORS, Element.PAPER, true)]
        [TestCase(Element.SCISSORS, Element.ROCK, true)]
        [TestCase(Element.SCISSORS, Element.SCISSORS, true)]
        public void TestReplaceEnemyRockWithScissors(Element bluePlayerElement, Element redPlayerElement, bool isBluePlayer)
        {
            TransformEnemyElementIntoElement(bluePlayerElement, redPlayerElement, Element.ROCK, Element.SCISSORS, isBluePlayer);
        }

        #endregion
    }
}
