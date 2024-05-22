using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Cards.Spells;
using AFM_DLL.Models.Cards.Spells.ReplaceElement;
using AFM_DLL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.Cards
{
    /// <summary>
    ///     Représente toutes les cartes sortilèges et propose une méthode pour en générer
    /// </summary>
    public abstract class SpellCard : Card
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
        public static SpellCard FromType(SpellType spell)
        {
            switch (spell)
            {
                case SpellType.ADD_MANA_FROM_ROCK:
                    return new AddManaFromRockSpell();
                case SpellType.ADD_MANA_FROM_PAPER:
                    return new AddManaFromPaperSpell();
                case SpellType.ADD_MANA_FROM_SCISSORS:
                    return new AddManaFromScissorsSpell();
                case SpellType.DOUBLE_DAMAGE:
                    return new DoubleDamageSpell();
                case SpellType.REPLACE_ENEMY_ROCK_WITH_SCISSORS:
                    return new ReplaceEnemyRockWithScissors();
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
        public abstract SpellType GetSpellType();

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

        /// <summary>
        ///     Active le sort sur le plateau donné
        /// </summary>
        /// <param name="board">Le plateau d'activation, permettant au sort d'accéder aux différents éléments de la partie</param>
        /// <param name="isBlueSide">Indique si le sortilège est lancé depuis le côté bleu de la partie</param>
        public abstract void ActivateSpell(Board board, bool isBlueSide);


        public override bool AddToBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            if (position.HasValue)
                return false;

            SpellCard currSpell = null;
            var side = board.GetAllyBoardSide(isBlueSide);
            if (side.SpellCard != null)
            {
                currSpell = side.SpellCard;
                currSpell.RemoveFromBoard(board, isBlueSide, null);
            }
            if (!CanBePlayed(side.Player.ManaPoints))
            {
                currSpell?.AddToBoard(board, isBlueSide, null);
                return false;
            }
            
            side.SpellCard = this;
            side.Player.RemoveMana(GetManaCost());
            side.Player.Hand.Spells.Remove(this);
            return true;
        }
        
        public override bool RemoveFromBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            if (position.HasValue)
                return false;

            var side = board.GetAllyBoardSide(isBlueSide);
            if (side.SpellCard == null || side.SpellCard.GetSpellType() != this.GetSpellType())
                return false;

            side.SpellCard = null;
            side.Player.AddMana(GetManaCost());
            side.Player.Hand.Spells.Add(this);
            return true;
        }
    }
}
