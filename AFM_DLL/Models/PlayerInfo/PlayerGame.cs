using AFM_DLL.Extensions;
using AFM_DLL.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.PlayerInfo
{
    /// <summary>
    ///     Représente l'état d'un joueur dans une partie
    /// </summary>
    public class PlayerGame
    {
        public PlayerGame(Deck initialDeck)
        {
            HealthPoints = 20;
            ManaPoints = 0;
            Deck = initialDeck;
            Hand = new Hand();
            Defausse = new List<Card>();
        }


        public event Action PlayerDied;
        public int HealthPoints { get; private set; }
        public int ManaPoints { get; private set; }
        public Hand Hand { get; private set; }
        public Deck Deck { get; private set; }
        public List<Card> Defausse { get; private set; }

        

        /// <summary>
        ///     Ajoute du mana au joueur
        /// </summary>
        /// <param name="mana">La quantité de mana à ajouter</param>
        /// <returns>Le nombre de mana réellement ajouté</returns>
        public int AddMana(int mana)
        {
            if (ManaPoints >= 10)
                return 0;

            var manaToAdd = Math.Min(10 - ManaPoints, mana);
            ManaPoints += manaToAdd;
            return manaToAdd;
        }

        /// <summary>
        ///     Retire du mana au joueur
        /// </summary>
        /// <param name="mana">La quantité de mana à retirer</param>
        /// <returns>Si le joueur possède suffisamment de mana</returns>
        public bool RemoveMana(int mana)
        {
            if (ManaPoints < mana)
                return false;
            ManaPoints -= mana;
            return true;
        }

        /// <summary>
        ///     Ajoute des points de vie au joueur
        /// </summary>
        /// <param name="health">La quantité de points de vie à ajouter</param>
        public void AddHealth(int health)
        {
            HealthPoints += health;
        }

        /// <summary>
        ///     Retire des points de vie au joueur
        /// </summary>
        /// <param name="health">La quantité de points de vie à retirer</param>
        public void RemoveHealth(int health)
        {
            HealthPoints -= health;

            if (HealthPoints <= 0)
                PlayerDied.Invoke();
        }


        /// <summary>
        ///     Permet au joueur d'abandonner la partie
        /// </summary>
        public void GiveUp()
        {
            HealthPoints = 0;
            PlayerDied.Invoke();
        }

        public void Draw()
        {
            while (Hand.Elements.Count < 4)
            {
                Hand.Elements.Add(Deck.Elements.PopFirst());
            }
            if (Deck.Spells.Any())
            {
                Hand.Spells.Add(Deck.Spells.PopFirst());
            }
        }
    }
}
