using AFM_DLL.Models.PlayerInfo;
using AFM_Tests.TestData;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_Tests
{
    public class PlayerGameTests
    {
        private PlayerGame _player;

        [SetUp]
        public void Setup()
        {
            _player = new PlayerGame(TestDecks.GetRockDeck());
            _player.AddMana(4);
        }

        #region Health

        [TestCase(1, 21)]
        [TestCase(2, 22)]
        [TestCase(3, 23)]
        [TestCase(4, 24)]
        [TestCase(5, 25)]
        [TestCase(10, 30)]
        [TestCase(20, 40)]
        [TestCase(25, 45)]
        [TestCase(50, 70)]
        [TestCase(100, 120)]
        public void AddHealthTest(int toAdd, int expectedHealth)
        {
            _player.AddHealth((uint)toAdd);
            Assert.That(expectedHealth, Is.EqualTo(_player.HealthPoints));
        }

        [TestCase(1, 19, false)]
        [TestCase(2, 18, false)]
        [TestCase(3, 17, false)]
        [TestCase(4, 16, false)]
        [TestCase(5, 15, false)]
        [TestCase(10, 10, false)]
        [TestCase(15, 5, false)]
        [TestCase(20, 0, true)]
        [TestCase(25, -5, true)]
        [TestCase(50, -30, true)]
        public void RemoveHealthTest(int toRemove, int expectedHealth, bool playerDied)
        {
            var didPlayerDie = false;
            _player.PlayerDied += () => { didPlayerDie = true; };
            _player.RemoveHealth((uint)toRemove);

            Assert.Multiple(() =>
            {
                Assert.That(expectedHealth, Is.EqualTo(_player.HealthPoints));
                Assert.That(playerDied, Is.EqualTo(didPlayerDie));
            });
        }

        #endregion Health

        #region Mana

        [TestCase(1, 5, 0)]
        [TestCase(2, 6, 0)]
        [TestCase(3, 7, 0)]
        [TestCase(4, 8, 0)]
        [TestCase(5, 9, 0)]
        [TestCase(10, 10, 4)]
        [TestCase(12, 10, 6)]
        [TestCase(15, 10, 9)]
        [TestCase(18, 10, 12)]
        [TestCase(20, 10, 14)]
        public void AddManaTest(int toAdd, int expectedMana, int expectedRemainingMana)
        {
            var actuallyAddedMana = _player.AddMana((uint)toAdd);

            Assert.Multiple(() =>
            {
                Assert.That(expectedMana, Is.EqualTo(_player.ManaPoints));
                Assert.That(expectedRemainingMana, Is.EqualTo(toAdd - actuallyAddedMana));
            });
        }

        [TestCase(1, 3, true)]
        [TestCase(2, 2, true)]
        [TestCase(3, 1, true)]
        [TestCase(4, 0, true)]
        [TestCase(5, 4, false)]
        public void RemoveManaTest(int toRemove, int expectedMana, bool expectedResult)
        {
            var result = _player.RemoveMana((uint)toRemove);

            Assert.Multiple(() =>
            {
                Assert.That(expectedMana, Is.EqualTo(_player.ManaPoints));
                Assert.That(expectedResult, Is.EqualTo(result));
            });
        }

        #endregion Mana

        [Test]
        public void DrawTest()
        {
            _player.Draw();

            Assert.Multiple(() =>
            {
                Assert.That(_player.Hand.Elements, Has.Count.EqualTo(4));
                Assert.That(_player.Hand.Spells, Has.Count.EqualTo(1));
            });

            _player.Draw();

            Assert.Multiple(() =>
            {
                Assert.That(_player.Hand.Elements, Has.Count.EqualTo(4));
                Assert.That(_player.Hand.Spells, Has.Count.EqualTo(2));
            });

            _player.Draw();

            Assert.Multiple(() =>
            {
                Assert.That(_player.Hand.Elements, Has.Count.EqualTo(4));
                Assert.That(_player.Hand.Spells, Has.Count.EqualTo(3));
            });
        }


        [Test]
        public void GiveUpTest()
        {
            var didPlayerDie = false;
            _player.PlayerDied += () => { didPlayerDie = true; };

            _player.GiveUp();

            Assert.Multiple(() =>
            {
                Assert.That(_player.HealthPoints, Is.EqualTo(0));
                Assert.That(didPlayerDie);
            });
        }
    }
}
