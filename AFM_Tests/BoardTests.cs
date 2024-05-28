using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.PlayerInfo;
using AFM_Tests.TestData;
using AFM_DLL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFM_DLL.Models.Cards;
using System.Drawing;
using AFM_DLL.Models.Enum;

namespace AFM_Tests
{
    public class BoardTests
    {

        [Test]
        public void BoardDrawTest()
        {
            var board = TestBoards.GetBluePlayerPrioBoard();
            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);
            var res = board.DrawCards();
            Assert.Multiple(() =>
            {
                Assert.That(board.NextAction, Is.EqualTo(BoardState.PLAY_CARDS));
                Assert.That(bs.Player.Hand.Elements.Count, Is.EqualTo(4));
                Assert.That(rs.Player.Hand.Elements.Count, Is.EqualTo(4));
                Assert.That(res.BlueSideDrawResult.DrawnElements, Is.EquivalentTo(bs.Player.Hand.Elements));
                Assert.That(res.RedSideDrawResult.DrawnElements, Is.EquivalentTo(rs.Player.Hand.Elements));
                Assert.That(bs.Player.Hand.Spells.Count, Is.EqualTo(1));
                Assert.That(rs.Player.Hand.Spells.Count, Is.EqualTo(1));
                Assert.That(bs.Player.Hand.Spells.First(), Is.EqualTo(res.BlueSideDrawResult.DrawnSpell));
                Assert.That(rs.Player.Hand.Spells.First(), Is.EqualTo(res.RedSideDrawResult.DrawnSpell));
                Assert.That(rs.ElementCards.Values.Count(v => v == null), Is.EqualTo(3));
                Assert.That(bs.ElementCards.Values.Count(v => v == null), Is.EqualTo(3));
                Assert.That(rs.SpellCard, Is.EqualTo(null));
                Assert.That(bs.SpellCard, Is.EqualTo(null));
            });
        }

        #region Play Elements

        [TestCase(BoardPosition.LEFT, true)]
        [TestCase(BoardPosition.LEFT, false)]
        [TestCase(BoardPosition.MIDDLE, true)]
        [TestCase(BoardPosition.MIDDLE, false)]
        [TestCase(BoardPosition.RIGHT, true)]
        [TestCase(BoardPosition.RIGHT, false)]
        public void AddElementCardTest(BoardPosition position, bool isBlueSide)
        {
            var board = TestBoards.GetBluePlayerPrioBoard();
            board.DrawCards();


            var side = board.GetAllyBoardSide(isBlueSide);

            var card = side.Player.Hand.Elements[0];

            var success = card.AddToBoard(board, isBlueSide, position);

            Assert.Multiple(() =>
            {
                Assert.That(side.ElementCards[position], Is.EqualTo(card));
                Assert.That(side.Player.Hand.Elements, Is.Not.Contains(card));
                Assert.That(success);
            });
        }

        [Test]
        public void AddAllElementCards()
        {
            var board = TestBoards.GetBluePlayerPrioBoard();
            var drawres = board.DrawCards();

            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);

            foreach(var position in Enum.GetValues<BoardPosition>())
            {
                bs.Player.Hand.Elements[0].AddToBoard(board, true, position);
                rs.Player.Hand.Elements[0].AddToBoard(board, false, position);
            }

            Assert.Multiple(() =>
            {
                Assert.That(bs.Player.Hand.Elements, Has.Count.EqualTo(1));
                Assert.That(rs.Player.Hand.Elements, Has.Count.EqualTo(1));
                Assert.That(bs.ElementCards.Values.Any(c => c == null), Is.False);
                Assert.That(rs.ElementCards.Values.Any(c => c == null), Is.False);
            });
        }

        [TestCase(BoardPosition.LEFT, true)]
        [TestCase(BoardPosition.LEFT, false)]
        [TestCase(BoardPosition.MIDDLE, true)]
        [TestCase(BoardPosition.MIDDLE, false)]
        [TestCase(BoardPosition.RIGHT, true)]
        [TestCase(BoardPosition.RIGHT, false)]
        public void RemoveElementCardTest(BoardPosition position, bool isBlueSide)
        {
            var board = TestBoards.GetBluePlayerPrioBoard();
            board.DrawCards();
            var side = board.GetAllyBoardSide(isBlueSide);

            var card = side.Player.Hand.Elements[0];

            var addSuccess = card.AddToBoard(board, isBlueSide, position);

            Assume.That(side.ElementCards[position], Is.EqualTo(card));
            Assume.That(side.Player.Hand.Elements, Is.Not.Contains(card));
            Assume.That(addSuccess);

            var removeSuccess = card.RemoveFromBoard(board, isBlueSide, position);

            Assert.Multiple(() =>
            {
                Assert.That(side.ElementCards[position] == null);
                Assert.That(side.Player.Hand.Elements, Does.Contain(card));
                Assert.That(removeSuccess);
            });
        }

        #endregion Play Elements    

        #region Play Spells

        [TestCase(false)]
        [TestCase(true)]
        public void AddSpellCardSuccessTest(bool isBlueSide)
        {
            var board = TestBoards.GetBluePlayerPrioBoard();
            board.DrawCards();
            var side = board.GetAllyBoardSide(isBlueSide);
            side.Player.AddMana(10);

            var card = side.Player.Hand.Spells[0];

            var success = card.AddToBoard(board, isBlueSide, null);

            Assert.Multiple(() =>
            {
                Assert.That(side.SpellCard, Is.EqualTo(card));
                Assert.That(side.Player.Hand.Spells, Is.Not.Contains(card));
                Assert.That(success);
                Assert.That(side.Player.ManaPoints, Is.EqualTo(10 - card.GetManaCost()));
            });
        }

        [TestCase(false)]
        [TestCase(true)]
        public void AddSpellCardFailTest(bool isBlueSide)
        {
            var board = TestBoards.GetBluePlayerPrioBoard();
            board.DrawCards();
            var side = board.GetAllyBoardSide(isBlueSide);
            side.Player.RemoveMana((uint)side.Player.ManaPoints);

            var card = side.Player.Hand.Spells[0];
            side.Player.AddMana(card.GetManaCost() - 1);

            var success = card.AddToBoard(board, isBlueSide, null);

            Assert.Multiple(() =>
            {
                Assert.That(side.SpellCard, Is.Null);
                Assert.That(side.Player.Hand.Spells, Does.Contain(card));
                Assert.That(!success);
                Assert.That(side.Player.ManaPoints, Is.EqualTo(card.GetManaCost() - 1));
            });
        }

        [TestCase(false)]
        [TestCase(true)]
        public void RemoveSpellCardTest(bool isBlueSide)
        {
            var board = TestBoards.GetBluePlayerPrioBoard();
            board.DrawCards();
            var side = board.GetAllyBoardSide(isBlueSide);
            side.Player.AddMana(10);
            var card = side.Player.Hand.Spells[0];

            var addSuccess = card.AddToBoard(board, isBlueSide, null);

            Assume.That(side.SpellCard, Is.EqualTo(card));
            Assume.That(side.Player.Hand.Spells, Is.Not.Contains(card));
            Assume.That(addSuccess);
            Assume.That(side.Player.ManaPoints, Is.EqualTo(10 - card.GetManaCost()));

            var removeSuccess = card.RemoveFromBoard(board, isBlueSide, null);


            Assert.Multiple(() =>
            {
                Assert.That(side.SpellCard, Is.EqualTo(null));
                Assert.That(side.Player.Hand.Spells, Does.Contain(card));
                Assert.That(removeSuccess);
                Assert.That(side.Player.ManaPoints, Is.EqualTo(10));
            });
        }

        #endregion Play Spells

        #region Evaluate Spells 

        [TestCase(false, false, false, 0)]
        [TestCase(false, false, true, 1)]
        [TestCase(false, true, false, 1)]
        [TestCase(false, true, true, 2)]
        [TestCase(true, false, false, 0)]
        [TestCase(true, false, true, 1)]
        [TestCase(true, true, false, 1)]
        [TestCase(true, true, true, 2)]
        public void EvaluateSpellsTest(bool blueSidePrio, bool blueSidePlays, bool redSidePlays, int expectedNbSpellsPlayed)
        {
            var board = blueSidePrio ? TestBoards.GetBluePlayerPrioBoard() : TestBoards.GetRedPlayerPrioBoard();
            board.DrawCards();
            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);
            bs.Player.AddMana(10);
            rs.Player.AddMana(10);

            SpellCard? bc = null;
            SpellCard? rc = null;
            
            if (blueSidePlays)
            {
                bc = bs.Player.Hand.Spells[0];
                bc.AddToBoard(board, true, null);
            }
            board.SetSideReady(isBlueSide: true);

            if (redSidePlays)
            {
                rc = rs.Player.Hand.Spells[0];
                rc.AddToBoard(board, false, null);
            }
            board.SetSideReady(isBlueSide: false);

            Assume.That(board.NextAction, Is.EqualTo(BoardState.EVALUATE_SPELLS));

            var spellsResult = board.EvaluateSpells();

            Assert.Multiple(() =>
            {
                Assert.That(spellsResult.SpellsInOrder, Has.Count.EqualTo(expectedNbSpellsPlayed));
                Assert.That(spellsResult.BlueSideStartedOnDraw, Is.Null);
                if (blueSidePlays)
                {
                    if (blueSidePrio)
                    {
                        Assert.That(spellsResult.SpellsInOrder.First().isBlueSide, Is.True);
                        Assert.That(spellsResult.SpellsInOrder.First().card, Is.EqualTo(bc));
                        if (redSidePlays)
                            Assert.That(spellsResult.HeroFightResult, Is.EqualTo(FightResult.BLUE_WIN));
                        else
                            Assert.That(spellsResult.HeroFightResult.HasValue, Is.False);
                    }
                    else
                    {
                        Assert.That(spellsResult.SpellsInOrder.Last().isBlueSide, Is.True);
                        Assert.That(spellsResult.SpellsInOrder.Last().card, Is.EqualTo(bc));
                        if (redSidePlays)
                            Assert.That(spellsResult.HeroFightResult, Is.EqualTo(FightResult.RED_WIN));
                        else
                            Assert.That(spellsResult.HeroFightResult.HasValue, Is.False);
                    }
                }
                if (redSidePlays)
                {
                    if (!blueSidePrio)
                    {
                        Assert.That(spellsResult.SpellsInOrder.First().isBlueSide, Is.False);
                        Assert.That(spellsResult.SpellsInOrder.First().card, Is.EqualTo(rc));
                        if (blueSidePlays)
                            Assert.That(spellsResult.HeroFightResult, Is.EqualTo(FightResult.RED_WIN));
                        else
                            Assert.That(spellsResult.HeroFightResult.HasValue, Is.False);
                    }
                    else
                    {
                        Assert.That(spellsResult.SpellsInOrder.Last().isBlueSide, Is.False);
                        Assert.That(spellsResult.SpellsInOrder.Last().card, Is.EqualTo(rc));
                        if (blueSidePlays)
                            Assert.That(spellsResult.HeroFightResult, Is.EqualTo(FightResult.BLUE_WIN));
                        else
                            Assert.That(spellsResult.HeroFightResult.HasValue, Is.False);
                    }
                }
                if (!blueSidePlays && !redSidePlays)
                    Assert.That(spellsResult.HeroFightResult.HasValue, Is.False);
            });


        }



        #endregion

        #region ResetBoard

        [Test]
        public void ResetBoardTest()
        {
            var board = TestBoards.GetFullBoardAfterEvaluation();
            board.ResetBoard();
            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);

            Assert.Multiple(() =>
            {
                Assert.That(bs.IsSideReady, Is.False);
                Assert.That(rs.IsSideReady, Is.False);
                Assert.That(bs.Player.Defausse, Has.Count.EqualTo(1));
                Assert.That(rs.Player.Defausse, Has.Count.EqualTo(1));
                Assert.That(bs.Player.Hand.Elements, Has.Count.EqualTo(1));
                Assert.That(rs.Player.Hand.Elements, Has.Count.EqualTo(1));
                Assert.That(bs.Player.Deck.Elements, Has.Count.EqualTo(9));
                Assert.That(rs.Player.Deck.Elements, Has.Count.EqualTo(9));
                Assert.That(bs.Player.Deck.Elements.Any(e => e.OverrideElement != null), Is.False);
                Assert.That(rs.Player.Deck.Elements.Any(e => e.OverrideElement != null), Is.False);
                Assert.That(bs.Player.Hand.Spells, Is.Empty);
                Assert.That(rs.Player.Hand.Spells, Is.Empty);

                Assert.That(board.AllElementsOfBoard.Any(c => c != null), Is.False);
                Assert.That(board.NextAction, Is.EqualTo(BoardState.DRAW_CARDS));

            });
        }

        #endregion

        #region Boucle de jeu

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(10)]
        public void SkipTurns(int turnstoSkip)
        {
            var board = TestBoards.GetBluePlayerPrioBoard();

            for (int i = 0; i < turnstoSkip; i++)
            {
                board.DrawCards();
                board.SetSideReady(isBlueSide: true);
                board.SetSideReady(isBlueSide: false);
                board.EvaluateSpells();
                board.EvaluateCardColumns();
                board.ResetBoard();
            }
        }

        [Test]
        public void PlayAllCardsUntilDeath()
        {
            var board = TestBoards.GetBluePlayerPrioBoard();

            var noDeath = true;
            void death() { noDeath = false; }

            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);

            bs.Player.PlayerDied += death;
            rs.Player.PlayerDied += death;

            while (noDeath)
            {
                board.DrawCards();

                foreach (var position in Enum.GetValues<BoardPosition>())
                {
                    bs.Player.Hand.Elements[0].AddToBoard(board, true, position);
                    rs.Player.Hand.Elements[0].AddToBoard(board, false, position);
                }

                board.SetSideReady(isBlueSide: true);
                board.SetSideReady(isBlueSide: false);

                board.EvaluateSpells();
                board.EvaluateCardColumns();
                board.ResetBoard();
            }
        }

        [Test]
        public void PlayAllCardsWithSpellsUntilDeath()
        {
            var board = TestBoards.GetBluePlayerPrioBoard();

            var noDeath = true;
            void death() { noDeath = false; }

            var bs = board.GetAllyBoardSide(true);
            var rs = board.GetEnemyBoardSide(true);

            bs.Player.PlayerDied += death;
            rs.Player.PlayerDied += death;

            while (noDeath)
            {
                board.DrawCards();

                foreach (var position in Enum.GetValues<BoardPosition>())
                {
                    bs.Player.Hand.Elements[0].AddToBoard(board, true, position);
                    rs.Player.Hand.Elements[0].AddToBoard(board, false, position);
                }

                if (bs.Player.Hand.Spells.Count != 0)
                {
                    var spellCard = bs.Player.Hand.Spells[0];
                    if (spellCard.CanBePlayed(bs.Player.ManaPoints))
                    {
                        spellCard.AddToBoard(board, true, null);
                    }
                }

                if (rs.Player.Hand.Spells.Count != 0)
                {
                    var spellCard = rs.Player.Hand.Spells[0];
                    if (spellCard.CanBePlayed(rs.Player.ManaPoints))
                    {
                        spellCard.AddToBoard(board, false, null);
                    }
                }

                board.SetSideReady(isBlueSide: true);
                board.SetSideReady(isBlueSide: false);

                board.EvaluateSpells();
                board.EvaluateCardColumns();
                board.ResetBoard();
            }
        }

        #endregion Boucle de jeu

    }
}
