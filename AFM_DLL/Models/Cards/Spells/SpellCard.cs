using AFM_DLL.Models.Cards.Spells;
using AFM_DLL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.Cards
{
    /// <summary>
    ///     Représente toutes les cartes sortilèges et propose une méthode pour en générer
    /// </summary>
    public abstract class SpellCard
    {
        /// <summary>
        ///     Génère une instance de carte sortilège en fonction du type de sort donné
        /// </summary>
        /// <param name="spell">
        ///     Le type de sort qui sera instancié par cette méthode
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">
        ///     Il n'existe pas encore de cartes sortilèges pour ce type de carte
        /// </exception>
        public static SpellCard GetOfType(SpellType spell)
        {
            switch (spell)
            {
                case SpellType.ADD_MANA_FROM_ROCK:
                    return new AddManaFromRockSpell();
                case SpellType.ADD_MANA_FROM_PAPER:
                    return new AddManaFromPaperSpell();
                case SpellType.ADD_MANA_FROM_SCISSORS:
                    return new AddManaFromScissorsSpell();
                default:
                    throw new NotImplementedException($"Le sort de type {spell} n'a pas de classe attitrée.");
            }
        }


        /// <summary>
        ///     Le coût en mana du sort.
        /// </summary>
        public abstract int GetManaCost();
        /// <summary>
        ///     La description du sort
        /// </summary>
        public abstract string GetDescription();
        /// <summary>
        ///     Le type du sort tel qu'indiqué à l'instanciation du sort
        /// </summary>
        public SpellType SpellType { get; private set; }

        /// <summary>
        ///     Indique si le sort peut être joué en fonction du mana actuel d'un joueur
        /// </summary>
        /// <param name="currPlayerMana">
        ///     Le mana actuel du joueur qui souhaite jouer la carte    
        /// </param>
        /// <returns>
        ///     Si la carte peut être jouée
        /// </returns>
        public bool CanBePlayed(int currPlayerMana) => GetManaCost() <= currPlayerMana;

        // Pas de commentaire intellisense ici, il faudra l'écrire sur chaque sous méthode.
        public abstract void ActivateSpell(); // TODO : pass board
    }
}
