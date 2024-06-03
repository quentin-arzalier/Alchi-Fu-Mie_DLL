using AFM_DLL.Extensions;
using AFM_DLL.Models.Cards;
using AFM_DLL.Models.UnityResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AFM_DLL.Models.PlayerInfo
{
    /// <summary>
    ///     Représente l'état d'un joueur dans une partie
    /// </summary>
    public class PlayerGame
    {
        /// <summary>
        ///     Initialise l'état d'un joueur
        /// </summary>
        /// <param name="initialDeck">  
        ///     Le deck du joueur tel qu'initialisé au préalable. <br/>
        ///     Il peut être le deck choisi par le joueur ou bien le deck attribué au PNJ.
        /// </param>
        public PlayerGame(Deck initialDeck)
        {
            HealthPoints = 10;
            ManaPoints = 0;
            Deck = initialDeck;
            Hand = new Hand();
            Defausse = new List<SpellCard>();
        }


        /// <summary>
        ///     Évènement se déclanchant quand le joueur meurt/perd.
        /// </summary>
        public event Action PlayerDied;

        /// <summary>
        ///     Nombre de points de vie du joueur.
        /// </summary>
        public int HealthPoints { get; private set; }

        /// <summary>
        ///     Nombre de points de mana du joueur
        /// </summary>
        public int ManaPoints { get; private set; }

        /// <summary>
        ///     Main du joueur
        /// </summary>
        public Hand Hand { get; private set; }
        /// <summary>
        ///     Cartes du joueur
        /// </summary>
        public Deck Deck { get; private set; }


        /// <summary>
        ///     Défausse du joueur
        /// </summary>
        public List<SpellCard> Defausse { get; private set; }



        /// <summary>
        ///     Ajoute du mana au joueur
        /// </summary>
        /// <param name="mana">La quantité de mana à ajouter</param>
        /// <returns>Le nombre de mana réellement ajouté</returns>
        public int AddMana(uint mana)
        {
            if (ManaPoints >= 10)
                return 0;

            var manaToAdd = Math.Min(10 - ManaPoints, (int)mana);
            ManaPoints += manaToAdd;
            return manaToAdd;
        }

        /// <summary>
        ///     Retire du mana au joueur
        /// </summary>
        /// <param name="mana">La quantité de mana à retirer</param>
        /// <returns>Si le joueur possède suffisamment de mana</returns>
        public bool RemoveMana(uint mana)
        {
            if (ManaPoints < mana)
                return false;
            ManaPoints -= (int)mana;
            return true;
        }

        /// <summary>
        ///     Ajoute des points de vie au joueur
        /// </summary>
        /// <param name="health">La quantité de points de vie à ajouter</param>
        public void AddHealth(uint health)
        {
            HealthPoints += (int)health;
        }

        /// <summary>
        ///     Retire des points de vie au joueur
        /// </summary>
        /// <param name="health">La quantité de points de vie à retirer</param>
        public void RemoveHealth(uint health)
        {
            HealthPoints -= (int)health;

            if (HealthPoints <= 0)
                PlayerDied?.Invoke();
        }


        /// <summary>
        ///     Permet au joueur d'abandonner la partie
        /// </summary>
        public void GiveUp()
        {
            HealthPoints = 0;
            PlayerDied?.Invoke();
        }

        /// <summary>
        ///     Donne jusqu'à 4 éléments au joueur ainsi qu'une carte sortilège (s'il en reste)
        /// </summary>
        /// <returns>
        ///     Un résultat de pioche qui contient des données sur les cartes piochées
        /// </returns>
        public DrawResult Draw()
        {
            var res = new DrawResult();
            while (Hand.Elements.Count < 4)
            {
                var card = Deck.Elements.PopFirst();
                res.DrawnElements.Add(card);
                Hand.Elements.Add(card);
            }
            if (Deck.Spells.Any())
            {
                res.DrawnSpell = Deck.Spells.PopFirst();
                Hand.Spells.Add(res.DrawnSpell);
            }
            return res;
        }

        internal bool ReplaceHeroType(ElementCard card)
        {
            if (card == null || !Hand.Elements.Contains(card))
                return false;

            if (Deck.Hero.OverrideCard != null && Deck.Hero.CanRevertOverride)
            {
                Hand.Elements.Add(Deck.Hero.OverrideCard);
                Deck.Hero.OverrideCard = null;
                AddMana(2);
            }

            if (!RemoveMana(2))
                return false;

            Hand.Elements.Remove(card);
            Deck.Hero.OverrideCard = card;
            Deck.Hero.CanRevertOverride = true;

            return true;
        }

        internal bool CancelHeroTypeReplacement()
        {
            if (Deck.Hero.OverrideCard != null && Deck.Hero.CanRevertOverride)
            {
                Hand.Elements.Add(Deck.Hero.OverrideCard);
                Deck.Hero.OverrideCard = null;
                AddMana(2);
                return true;
            }
            return false;
        }
    }
}
