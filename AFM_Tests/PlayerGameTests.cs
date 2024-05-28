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

        [TestCase(1, 11)]
        [TestCase(2, 12)]
        [TestCase(3, 13)]
        [TestCase(4, 14)]
        [TestCase(5, 15)]
        [TestCase(10, 20)]
        [TestCase(20, 30)]
        [TestCase(25, 35)]
        [TestCase(50, 60)]
        [TestCase(100, 110)]
        public void AddHealthTest(int toAdd, int expectedHealth)
        {
            _player.AddHealth((uint)toAdd);
            Assert.That(expectedHealth, Is.EqualTo(_player.HealthPoints));
        }

        [TestCase(1, 9, false)]
        [TestCase(2, 8, false)]
        [TestCase(3, 7, false)]
        [TestCase(4, 6, false)]
        [TestCase(5, 5, false)]
        [TestCase(10, 0, true)]
        [TestCase(15, -5, true)]
        [TestCase(20, -10, true)]
        [TestCase(25, -15, true)]
        [TestCase(50, -40, true)]
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
