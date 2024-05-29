using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Cards.Spells;
using AFM_DLL.Models.Cards.Spells.CancelSpell;
using AFM_DLL.Models.Cards.Spells.ReplaceElement;
using AFM_DLL.Models.Cards.Spells.WinDuelTie;
using AFM_DLL.Models.Enum;
using System;

namespace AFM_DLL.Models.Cards
{
    /// <summary>
    ///     Représente toutes les cartes sortilèges et propose une méthode pour en générer
    /// </summary>
    public abstract class SpellCard : Card
    {
        /// <summary>
        ///     Indique si un sort peut être joué ou non
        /// </summary>
        public bool CanBeActived {
            get
            {
                return true;
            }
            set
            {
                CanBeActived = value;
            }
        }
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
                case SpellType.REPLACE_ENEMY_ROCK_WITH_PAPER:
                    return new ReplaceEnemyRockWithPaper();
                case SpellType.REPLACE_ENEMY_PAPER_WITH_SCISSORS:
                    return new ReplaceEnemyPaperWithScissors();
                case SpellType.REPLACE_ENEMY_PAPER_WITH_ROCK:
                    return new ReplaceEnemyPaperWithRock();
                case SpellType.REPLACE_ENEMY_SCISSORS_WITH_PAPER:
                    return new ReplaceEnemyScissorsWithPaper();
                case SpellType.REPLACE_ENEMY_SCISSORS_WITH_ROCK:
                    return new ReplaceEnemyScissorsWithRock();
                case SpellType.REPLACE_ENEMY_CARDS_WITH_PAPER:
                    return new ReplaceEnemyCardsWithPaper();
                case SpellType.REPLACE_ENEMY_CARDS_WITH_ROCK:
                    return new ReplaceEnemyCardsWithRock();
                case SpellType.REPLACE_ENEMY_CARDS_WITH_SCISSORS:
                    return new ReplaceEnemyCardsWithScissors();
                case SpellType.CANCEL_ENEMY_SPELL:
                    return new CancelEnemySpell();
                case SpellType.WIN_DUEL_TIE:
                    return new WinDuelTie();
                default:
                    throw new NotImplementedException($"Le sort de type {spell} n'a pas de classe attitrée.");
            }
        }


        /// <summary>
        ///     Le coût en mana du sort.
        /// </summary>
        public abstract uint GetManaCost();
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


        /// <inheritdoc/>
        public override bool AddToBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            if (base.AddToBoard(board, isBlueSide, position) == false)
                return false;

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

        /// <inheritdoc/>
        public override bool RemoveFromBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            if (base.RemoveFromBoard(board, isBlueSide, position) == false)
                return false;

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

        internal override bool DiscardFromBoardSide(BoardSide side, BoardPosition? position)
        {
            if (position.HasValue)
                return false;

            side.SpellCard = null;
            side.Player.Defausse.Add(this);

            return true;
        }
    }
}
