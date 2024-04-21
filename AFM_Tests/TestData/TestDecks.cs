using AFM_DLL;
using AFM_DLL.Models.Cards;
using AFM_DLL.Models.Enum;
using AFM_DLL.Models.PlayerInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_Tests.TestData
{
    public static class TestDecks
    {
        public static Deck GetRockDeck()
        {
            var rockDeck = new Deck()
            {
                Hero = new Hero("RockMan", Element.ROCK)
            };
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));
            rockDeck.Elements.Add(new ElementCard(Element.ROCK));

            rockDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            rockDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            rockDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            rockDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            rockDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            rockDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            rockDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            rockDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            rockDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            rockDeck.Spells.Add(SpellCard.FromType(SpellType.DOUBLE_DAMAGE));

            return rockDeck;
        }
        public static Deck GetPaperDeck()
        {
            var paperDeck = new Deck()
            {
                Hero = new Hero("PaperMan", Element.PAPER)
            };
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));
            paperDeck.Elements.Add(new ElementCard(Element.PAPER));

            paperDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            paperDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            paperDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            paperDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            paperDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            paperDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            paperDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            paperDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            paperDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            paperDeck.Spells.Add(SpellCard.FromType(SpellType.DOUBLE_DAMAGE));

            return paperDeck;
        }
        public static Deck GetScissorsDeck()
        {
            var scissorsDeck = new Deck()
            {
                Hero = new Hero("ScissorsMan", Element.SCISSORS)
            };
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));
            scissorsDeck.Elements.Add(new ElementCard(Element.SCISSORS));

            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            scissorsDeck.Spells.Add(SpellCard.FromType(SpellType.DOUBLE_DAMAGE));

            return scissorsDeck;
        }
    }
}
