using System;
using System.Collections.Generic;

namespace AFM_DLL.Models.Unlockables
{
    /// <summary>
    ///     Représente un type de dos de carte
    /// </summary>
    public class CardBack : IUnlockable
    {
        /// <summary>
        ///     Instancie un dos de carte du type spécifié
        /// </summary>
        /// <param name="backType">Le type de dos de carte à créer</param>
        /// <returns>Un dos de carte</returns>
        public static CardBack FromType(CardBackType backType)
        {
            return new CardBack()
            {
                BackType = backType,
            };
        }

        internal static List<CardBack> GetAllTypes()
        {
            return new List<CardBack>()
            {
                CardBack.FromType(CardBackType.ROCK),
                CardBack.FromType(CardBackType.PAPER),
                CardBack.FromType(CardBackType.SCISSORS),
                CardBack.FromType(CardBackType.LIGHT),
                CardBack.FromType(CardBackType.DARK)
            };
        }

        /// <summary>
        ///     Le type de dos du dos de carte
        /// </summary>
        public CardBackType BackType { get; private set; }

        /// <inheritdoc/>
        public Rarity Rarity
        {
            get
            {
                switch (BackType)
                {
                    case CardBackType.DEFAULT:
                        return Rarity.COMMON;
                    case CardBackType.ROCK:
                    case CardBackType.PAPER:
                    case CardBackType.SCISSORS:
                        return Rarity.EPIC;
                    case CardBackType.LIGHT:
                    case CardBackType.DARK:
                        return Rarity.LEGENDARY;
                    default: throw new ArgumentException("Le type du dos de carte n'existe pas");
                }
            }
        }

        /// <inheritdoc/>
        public string Name
        {
            get
            {
                switch (BackType)
                {
                    case CardBackType.DEFAULT: return "Dos alchimique";
                    case CardBackType.ROCK: return "Dos de la montagne";
                    case CardBackType.PAPER: return "Dos de la forêt";
                    case CardBackType.SCISSORS: return "Dos des lames";
                    case CardBackType.LIGHT: return "Dos du paradis";
                    case CardBackType.DARK: return "Dos des limbes";
                    default: throw new ArgumentException("Le type du dos de carte n'existe pas");
                };
            }
        }
    }

    /// <summary>
    ///     Types de dos de carte
    /// </summary>
    public enum CardBackType
    {
        /// <summary>
        ///     Dos de carte par défaut
        /// </summary>
        DEFAULT,
        /// <summary>
        ///     Dos de carte thème pierre
        /// </summary>
        ROCK,
        /// <summary>
        ///     Dos de carte thème feuille
        /// </summary>
        PAPER,
        /// <summary>
        ///     Dos de carte thème ciseaux
        /// </summary>
        SCISSORS,
        /// <summary>
        ///     Dos de carte thème lumière
        /// </summary>
        LIGHT,
        /// <summary>
        ///     Dos de carte thème ténèbres
        /// </summary>
        DARK
    }
}
