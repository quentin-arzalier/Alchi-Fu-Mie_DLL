﻿using AFM_DLL;
using AFM_DLL.Models.Cards;
using AFM_DLL.Models.Enum;
using AFM_DLL.Models.PlayerInfo;

namespace AFM_Tests.TestData
{
    public static class TestDecks
    {
        public static Deck GetElementDeck(Element elt)
        {
            var deck = new Deck()
            {
                Hero = new Hero(elt.ToString().ToLower() + "man", elt)
            };
            for (int i = 0; i < 10; i++)
            {
                deck.Elements.Add(new ElementCard(elt));
            }

            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            deck.Spells.Add(SpellCard.FromType(SpellType.DOUBLE_DAMAGE));

            return deck;
        }

        public static Deck GetVariousElementDeck()
        {
            //default chosen element
            var elt = Element.ROCK;
            var deck = new Deck()
            {
                Hero = new Hero(elt.ToString().ToLower() + "man", elt)
            };

            deck.Elements.Add(new ElementCard(Element.ROCK));
            deck.Elements.Add(new ElementCard(Element.ROCK));
            deck.Elements.Add(new ElementCard(Element.ROCK));
            deck.Elements.Add(new ElementCard(Element.SCISSORS));
            deck.Elements.Add(new ElementCard(Element.SCISSORS));
            deck.Elements.Add(new ElementCard(Element.SCISSORS));
            deck.Elements.Add(new ElementCard(Element.PAPER));
            deck.Elements.Add(new ElementCard(Element.PAPER));
            deck.Elements.Add(new ElementCard(Element.PAPER));
            deck.Elements.Add(new ElementCard(Element.PAPER));

            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_ROCK));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_SCISSORS));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            deck.Spells.Add(SpellCard.FromType(SpellType.ADD_MANA_FROM_PAPER));
            deck.Spells.Add(SpellCard.FromType(SpellType.DOUBLE_DAMAGE));

            return deck;
        }

        public static Deck GetRockDeck() => GetElementDeck(Element.ROCK);
        public static Deck GetPaperDeck() => GetElementDeck(Element.PAPER);
        public static Deck GetScissorsDeck() => GetElementDeck(Element.SCISSORS);
    }
}
