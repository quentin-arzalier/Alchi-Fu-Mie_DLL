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

namespace AFM_Tests
{
    public class BoardTests
    {
        private Board _board;

        [SetUp]
        public void Setup()
        {
            _board = new Board();

            _board.BlueSide.Player = new PlayerGame(TestDecks.GetRockDeck());
            _board.BlueSide.Player.Draw();
            _board.BlueSide.Player.AddMana(10);

            _board.RedSide.Player = new PlayerGame(TestDecks.GetPaperDeck());
            _board.RedSide.Player.Draw();
            _board.RedSide.Player.AddMana(10);
        }

        [Test]
        public void BoardSetupTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_board.BlueSide.Player?.Hand.Elements.Count, Is.EqualTo(4));
                Assert.That(_board.RedSide.Player?.Hand.Elements.Count, Is.EqualTo(4));
                Assert.That(_board.BlueSide.Player?.Hand.Spells.Count, Is.EqualTo(1));
                Assert.That(_board.RedSide.Player?.Hand.Spells.Count, Is.EqualTo(1));

                Assert.That(_board.RedSide.ElementCards.Values.Count(v => v == null), Is.EqualTo(3));
                Assert.That(_board.BlueSide.ElementCards.Values.Count(v => v == null), Is.EqualTo(3));

                Assert.That(_board.RedSide.SpellCard, Is.EqualTo(null));
                Assert.That(_board.BlueSide.SpellCard, Is.EqualTo(null));
            });
        }


        [TestCase(BoardPosition.LEFT, true)]
        [TestCase(BoardPosition.LEFT, false)]
        [TestCase(BoardPosition.MIDDLE, true)]
        [TestCase(BoardPosition.MIDDLE, false)]
        [TestCase(BoardPosition.RIGHT, true)]
        [TestCase(BoardPosition.RIGHT, false)]
        public void AddElementCardTest(BoardPosition position, bool isBlueSide)
        {
            var side = _board.GetAllyBoardSide(isBlueSide);

            var card = side.Player.Hand.Elements[0];

            var success = card.AddToBoard(_board, isBlueSide, position);

            Assert.Multiple(() =>
            {
                Assert.That(side.ElementCards[position], Is.EqualTo(card));
                Assert.That(side.Player.Hand.Elements, Is.Not.Contains(card));
                Assert.That(success);
            });
        }

        [TestCase(false)]
        [TestCase(true)]
        public void AddSpellCardTest(bool isBlueSide)
        {
            var side = _board.GetAllyBoardSide(isBlueSide);

            var card = side.Player.Hand.Spells[0];

            var success = card.AddToBoard(_board, isBlueSide, null);

            Assert.Multiple(() =>
            {
                Assert.That(side.SpellCard, Is.EqualTo(card));
                Assert.That(side.Player.Hand.Spells, Is.Not.Contains(card));
                Assert.That(success);
                Assert.That(side.Player.ManaPoints, Is.EqualTo(10 - card.GetManaCost()));
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
            var side = _board.GetAllyBoardSide(isBlueSide);

            var card = side.Player.Hand.Elements[0];

            var addSuccess = card.AddToBoard(_board, isBlueSide, position);

            Assume.That(side.ElementCards[position], Is.EqualTo(card));
            Assume.That(side.Player.Hand.Elements, Is.Not.Contains(card));
            Assume.That(addSuccess);

            var removeSuccess = card.RemoveFromBoard(_board, isBlueSide, position);

            Assert.Multiple(() =>
            {
                Assert.That(side.ElementCards[position] == null);
                Assert.That(side.Player.Hand.Elements, Does.Contain(card));
                Assert.That(removeSuccess);
            });
        }


        [TestCase(false)]
        [TestCase(true)]
        public void RemoveSpellCardTest(bool isBlueSide)
        {
            var side = _board.GetAllyBoardSide(isBlueSide);

            var card = side.Player.Hand.Spells[0];

            var addSuccess = card.AddToBoard(_board, isBlueSide, null);

            Assume.That(side.SpellCard, Is.EqualTo(card));
            Assume.That(side.Player.Hand.Spells, Is.Not.Contains(card));
            Assume.That(addSuccess);
            Assume.That(side.Player.ManaPoints, Is.EqualTo(10 - card.GetManaCost()));

            var removeSuccess = card.RemoveFromBoard(_board, isBlueSide, null);


            Assert.Multiple(() =>
            {
                Assert.That(side.SpellCard, Is.EqualTo(null));
                Assert.That(side.Player.Hand.Spells, Does.Contain(card));
                Assert.That(removeSuccess);
                Assert.That(side.Player.ManaPoints, Is.EqualTo(10));
            });
        }
    }
}
