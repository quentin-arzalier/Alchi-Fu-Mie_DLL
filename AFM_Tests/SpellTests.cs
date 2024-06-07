using AFM_DLL;
using AFM_DLL.Helpers;
using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Cards;
using AFM_DLL.Models.Enum;
using AFM_Tests.TestData;
using System.Drawing;

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

            var res = board.EvaluateSpells();
            if (res.HasMoreSpells)
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
                Element.ROCK when targetElement == Element.PAPER => SpellType.REPLACE_ENEMY_ROCK_WITH_PAPER,

                Element.PAPER when targetElement == Element.ROCK => SpellType.REPLACE_ENEMY_PAPER_WITH_ROCK,
                Element.PAPER when targetElement == Element.SCISSORS => SpellType.REPLACE_ENEMY_PAPER_WITH_SCISSORS,

                Element.SCISSORS when targetElement == Element.ROCK => SpellType.REPLACE_ENEMY_SCISSORS_WITH_ROCK,
                Element.SCISSORS when targetElement == Element.PAPER => SpellType.REPLACE_ENEMY_SCISSORS_WITH_PAPER,

                _ => throw new ArgumentException()
            });

            side.Player.Hand.Spells[0].AddToBoard(board, isBlueSideSpell, null);
            board.SetSideReady(true);
            board.SetSideReady(false);

            var res = board.EvaluateSpells();
            if (res.HasMoreSpells)
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
        public void TestReplaceEnemyRockWithPaper(Element bluePlayerElement, Element redPlayerElement, bool isBluePlayer)
        {
            TransformEnemyElementIntoElement(bluePlayerElement, redPlayerElement, Element.ROCK, Element.PAPER, isBluePlayer);
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
        public void TestReplaceEnemyPaperWithRock(Element bluePlayerElement, Element redPlayerElement, bool isBluePlayer)
        {
            TransformEnemyElementIntoElement(bluePlayerElement, redPlayerElement, Element.PAPER, Element.ROCK, isBluePlayer);
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
        public void TestReplaceEnemyPaperWithScissors(Element bluePlayerElement, Element redPlayerElement, bool isBluePlayer)
        {
            TransformEnemyElementIntoElement(bluePlayerElement, redPlayerElement, Element.PAPER, Element.SCISSORS, isBluePlayer);
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
        public void TestReplaceEnemyScissorsWithPaper(Element bluePlayerElement, Element redPlayerElement, bool isBluePlayer)
        {
            TransformEnemyElementIntoElement(bluePlayerElement, redPlayerElement, Element.SCISSORS, Element.PAPER, isBluePlayer);
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
        public void TestReplaceEnemyScissorsWithRock(Element bluePlayerElement, Element redPlayerElement, bool isBluePlayer)
        {
            TransformEnemyElementIntoElement(bluePlayerElement, redPlayerElement, Element.SCISSORS, Element.ROCK, isBluePlayer);
        }
        private void TransformEnemyElementIntoElement(Element targetElement, bool isBlueSideSpell)
        {
            var board = new Board(TestPlayers.GetVariousElementPlayer(), TestPlayers.GetVariousElementPlayer());
            TestBoards.FillBoardWithCardsFromDrawPhase(board);

            var expectedCardCount = 3;

            var side = board.GetAllyBoardSide(isBlueSideSpell);
            var enemySide = board.GetEnemyBoardSide(isBlueSideSpell);
            side.Player.AddMana(250);

            side.Player.Hand.Spells[0] = SpellCard.FromType(targetElement switch
            {
                Element.ROCK => SpellType.REPLACE_ENEMY_CARDS_WITH_ROCK,
                Element.PAPER => SpellType.REPLACE_ENEMY_CARDS_WITH_PAPER,
                Element.SCISSORS => SpellType.REPLACE_ENEMY_CARDS_WITH_SCISSORS,

                _ => throw new ArgumentException()
            });

            side.Player.Hand.Spells[0].AddToBoard(board, isBlueSideSpell, null);
            board.SetSideReady(true);
            board.SetSideReady(false);

            var res = board.EvaluateSpells();
            if (res.HasMoreSpells)
                board.EvaluateSpells();

            Assert.That(enemySide.AllElementsOfSide.Count(card => card.ActiveElement == targetElement), Is.EqualTo(expectedCardCount));
        }

        [TestCase(Element.PAPER, true)]
        [TestCase(Element.PAPER, false)]
        [TestCase(Element.ROCK, true)]
        [TestCase(Element.ROCK, false)]
        [TestCase(Element.SCISSORS, true)]
        [TestCase(Element.SCISSORS, false)]
        public void TestReplaceEnemyElementsWithAnyElement(Element targetElement, bool isBluePlayer)
        {
            TransformEnemyElementIntoElement(targetElement, isBluePlayer);
        }

        public void DisableCanBeActivatedSpell(bool isBlueSideSpell)
        {
            var board = new Board(TestPlayers.GetVariousElementPlayer(), TestPlayers.GetVariousElementPlayer());
            TestBoards.FillBoardWithCardsFromDrawPhase(board);

            var side = board.GetAllyBoardSide(isBlueSideSpell);
            var enemySide = board.GetEnemyBoardSide(isBlueSideSpell);
            side.Player.AddMana(10);
            enemySide.Player.AddMana(10);
            side.Player.Hand.Spells[0] = SpellCard.FromType(SpellType.CANCEL_ENEMY_SPELL);
            side.Player.Hand.Spells[0].AddToBoard(board, isBlueSideSpell, null);
            enemySide.Player.Hand.Spells[0].AddToBoard(board, isBlueSideSpell, null);
            board.SetSideReady(true);
            board.SetSideReady(false);
            var res = board.EvaluateSpells();

            Assert.That(res.HasMoreSpells, Is.False);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestCancelEnemySpell(bool isBlueSideSpell)
        {
            DisableCanBeActivatedSpell(isBlueSideSpell);
        }

        public void SwapElementsCards(Element bluePlayerElement, Element redPlayerElement, bool isBluePlayer)
        {
            var board = new Board(TestPlayers.FromElement(bluePlayerElement), TestPlayers.FromElement(redPlayerElement));
            TestBoards.FillBoardWithCardsFromDrawPhase(board);
            var playerCards = board.GetAllyBoardSide(isBluePlayer).AllElementsOfSide.Select(x => x.ActiveElement).ToList();
            var enemyCards = board.GetEnemyBoardSide(isBluePlayer).AllElementsOfSide.Select(x => x.ActiveElement).ToList();
            var side = board.GetAllyBoardSide(isBluePlayer);
            side.Player.AddMana(250);
            side.Player.Hand.Spells[0] = SpellCard.FromType(SpellType.SWAP_ENEMY_CARDS_WITH_PLAYER_CARDS);
            side.Player.Hand.Spells[0].AddToBoard(board, isBluePlayer, null);
            board.SetSideReady(true);
            board.SetSideReady(false);
            board.EvaluateSpells();
            Assert.That(board.GetEnemyBoardSide(isBluePlayer).AllElementsOfSide.Select(x => x.ActiveElement).ToList(),
                Is.EquivalentTo(playerCards));
            Assert.That(board.GetAllyBoardSide(isBluePlayer).AllElementsOfSide.Select(x => x.ActiveElement).ToList(), 
                Is.EquivalentTo(enemyCards));
        }

        [TestCase(Element.PAPER, Element.PAPER, true)]
        [TestCase(Element.PAPER, Element.SCISSORS, true)]
        [TestCase(Element.PAPER, Element.ROCK, true)]
        [TestCase(Element.ROCK, Element.ROCK, true)]
        [TestCase(Element.ROCK, Element.PAPER, true)]
        [TestCase(Element.ROCK, Element.SCISSORS, true)]
        [TestCase(Element.SCISSORS, Element.SCISSORS, true)]
        [TestCase(Element.SCISSORS, Element.PAPER, true)]
        [TestCase(Element.SCISSORS, Element.ROCK, true)]
        [TestCase(Element.PAPER, Element.PAPER, false)]
        [TestCase(Element.PAPER, Element.SCISSORS, false)]
        [TestCase(Element.PAPER, Element.ROCK, false)]
        [TestCase(Element.ROCK, Element.ROCK, false)]
        [TestCase(Element.ROCK, Element.PAPER, false)]
        [TestCase(Element.ROCK, Element.SCISSORS, false)]
        [TestCase(Element.SCISSORS, Element.SCISSORS, false)]
        [TestCase(Element.SCISSORS, Element.PAPER, false)]
        [TestCase(Element.SCISSORS, Element.ROCK, false)]

        public void TestSwapElementsCardsSpell(Element bluePlayerElement, Element redPlayerElement, bool isBluePlayer)
        {
            SwapElementsCards(bluePlayerElement, redPlayerElement,isBluePlayer);
        }

        public void MakePlayerWinDuelInTieCase(Element playersElement, bool isBluePlayer)
        {
            var board = new Board(TestPlayers.FromElement(playersElement), TestPlayers.FromElement(playersElement));
            TestBoards.FillBoardWithCardsFromDrawPhase(board);
            var side = board.GetAllyBoardSide(isBluePlayer);
            side.Player.AddMana(4);
            side.Player.Hand.Spells[0] = SpellCard.FromType(SpellType.WIN_DUEL_TIE);
            side.Player.Hand.Spells[0].AddToBoard(board, isBluePlayer, null);
            board.SetSideReady(true);
            board.SetSideReady(false);
            board.EvaluateSpells();
            var results = board.EvaluateCardColumns();
            foreach (var result in results.Values)
            {
                Assert.That(result.CardFightResult, Is.Not.EqualTo(FightResult.DRAW));
                Assert.That(result.HeroFightResult, Is.Null);
            }
        }

        [TestCase(Element.PAPER, true)]
        [TestCase(Element.SCISSORS, true)]
        [TestCase(Element.ROCK, true)]
        [TestCase(Element.PAPER, false)]
        [TestCase(Element.SCISSORS, false)]
        [TestCase(Element.ROCK, false)]
        public void TestWinDueTie(Element playersElement, bool isBluePlayer)
        {
            MakePlayerWinDuelInTieCase(playersElement, isBluePlayer);
        }
        #endregion
    }
}
