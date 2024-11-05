using AFM_DLL;
using AFM_DLL.Converters;
using AFM_DLL.Models.Cards;
using AFM_DLL.Models.Enum;
using AFM_DLL.Models.PlayerInfo;
using AFM_DLL.Models.Unlockables;
using AFM_Tests.TestData;
using Newtonsoft.Json;

namespace AFM_Tests
{
    public class JsonTests
    {
        [TestCase(Element.ROCK)]
        [TestCase(Element.PAPER)]
        [TestCase(Element.SCISSORS)]
        public void SerializeDeserializeElementCardTest(Element elt)
        {
            var card = new ElementCard(elt);
            var json = JsonConvert.SerializeObject(card);
            var jsonCard = JsonConvert.DeserializeObject<ElementCard>(json);
            Assert.That(jsonCard, Is.Not.Null);
            Assert.That(jsonCard.ActiveElement, Is.EqualTo(card.ActiveElement));
        }

        [TestCase("RockMan", Element.ROCK)]
        [TestCase("PaperBoy", Element.PAPER)]
        [TestCase("ScissorsGirl", Element.SCISSORS)]
        public void SerializeDeserializeHeroTest(string heroName, Element elt)
        {
            var hero = new Hero(heroName, elt);
            var json = JsonConvert.SerializeObject(hero);
            var jsonHero = JsonConvert.DeserializeObject<Hero>(json);
            Assert.That(jsonHero, Is.Not.Null);
            Assert.That(jsonHero.ActiveElement, Is.EqualTo(hero.ActiveElement));
        }

        [TestCase(SpellType.ADD_MANA_FROM_ROCK)]
        [TestCase(SpellType.ADD_MANA_FROM_PAPER)]
        [TestCase(SpellType.ADD_MANA_FROM_SCISSORS)]
        [TestCase(SpellType.DOUBLE_DAMAGE)]
        [TestCase(SpellType.REPLACE_ENEMY_ROCK_WITH_SCISSORS)]
        [TestCase(SpellType.REPLACE_ENEMY_ROCK_WITH_PAPER)]
        [TestCase(SpellType.REPLACE_ENEMY_PAPER_WITH_ROCK)]
        [TestCase(SpellType.REPLACE_ENEMY_PAPER_WITH_SCISSORS)]
        [TestCase(SpellType.REPLACE_ENEMY_SCISSORS_WITH_PAPER)]
        [TestCase(SpellType.REPLACE_ENEMY_SCISSORS_WITH_ROCK)]
        //[TestCase(SpellType.REPLACE_ENEMY_CARDS_WITH_PLAYER_CARDS)]
        [TestCase(SpellType.REPLACE_ENEMY_CARDS_WITH_ROCK)]
        [TestCase(SpellType.REPLACE_ENEMY_CARDS_WITH_PAPER)]
        [TestCase(SpellType.REPLACE_ENEMY_CARDS_WITH_SCISSORS)]
        [TestCase(SpellType.CANCEL_ENEMY_SPELL)]
        [TestCase(SpellType.WIN_DUEL_TIE)]
        public void SerializeDeserializeSpellTest(SpellType type)
        {
            var spellCard = SpellCard.FromType(type);
            var json = JsonConvert.SerializeObject(spellCard);
            var jsonSpellCard = JsonConvert.DeserializeObject<SpellCard>(json, new SpellCardConverter());
            Assert.That(jsonSpellCard, Is.Not.Null);
            Assert.That(jsonSpellCard.SpellType, Is.EqualTo(spellCard.SpellType));
        }

        [TestCase(CardBackType.DEFAULT)]
        [TestCase(CardBackType.ROCK)]
        [TestCase(CardBackType.PAPER)]
        [TestCase(CardBackType.SCISSORS)]
        [TestCase(CardBackType.LIGHT)]
        [TestCase(CardBackType.DARK)]
        public void SerializeDeserializeCardBackTest(CardBackType type)
        {
            var cardBack = CardBack.FromType(type);
            var json = JsonConvert.SerializeObject(cardBack);
            var jsonCardBack = JsonConvert.DeserializeObject<CardBack>(json, new CardBackConverter());
            Assert.That(jsonCardBack, Is.Not.Null);
            Assert.That(jsonCardBack.BackType, Is.EqualTo(cardBack.BackType));
        }

        [TestCase(Element.ROCK)]
        [TestCase(Element.PAPER)]
        [TestCase(Element.SCISSORS)]
        public void SerializeDeserializeWholeDeckTest(Element deckElt)
        {
            var deck = TestDecks.GetElementDeck(deckElt);
            var json = JsonConvert.SerializeObject(deck);
            var jsonDeck = JsonConvert.DeserializeObject<Deck>(json, new SpellCardConverter());
            Assert.That(jsonDeck, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(jsonDeck.Hero.ActiveElement, Is.EqualTo(deck.Hero.ActiveElement));
                Assert.That(jsonDeck.Spells, Has.Count.EqualTo(10));
                Assert.That(jsonDeck.Elements, Has.Count.EqualTo(10));
                for (var i = 0; i < 10; i++)
                {
                    Assert.That(jsonDeck.Elements[i].ActiveElement, Is.EqualTo(deck.Elements[i].ActiveElement));
                    Assert.That(jsonDeck.Spells[i].SpellType, Is.EqualTo(deck.Spells[i].SpellType));
                }
            });
        }
    }
}
