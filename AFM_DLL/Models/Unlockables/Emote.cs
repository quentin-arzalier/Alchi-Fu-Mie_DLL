using System;
using System.Collections.Generic;

namespace AFM_DLL.Models.Unlockables
{
    /// <summary>
    ///     Représente une emote
    /// </summary>
    public class Emote : IUnlockable
    {
        /// <summary>
        ///     Instancie une emote du type spécifié
        /// </summary>
        /// <param name="emoteType">Le type d'emote à créer</param>
        /// <returns>Une emote</returns>
        public static Emote FromType(EmoteType emoteType)
        {
            return new Emote()
            {
                EmoteType = emoteType,
            };
        }

        /// <summary>
        ///     Type de l'emote
        /// </summary>
        public EmoteType EmoteType { get; private set; }

        internal static List<Emote> GetAllTypes()
        {
            return new List<Emote>()
            {
                Emote.FromType(EmoteType.THUMBS_UP),
                Emote.FromType(EmoteType.PEACE_SIGN),
                Emote.FromType(EmoteType.OK_HAND),
                Emote.FromType(EmoteType.ALOHA_SIGN),
                Emote.FromType(EmoteType.STOP_SIGN),
                Emote.FromType(EmoteType.HUNDRED_EMOJI),
                Emote.FromType(EmoteType.FIRE_EMOJI),
                Emote.FromType(EmoteType.ROCK_HAND),
                Emote.FromType(EmoteType.PAPER_HAND),
                Emote.FromType(EmoteType.SCISSORS_HAND),
                Emote.FromType(EmoteType.CRYING_SCISSORS_HERO),
                Emote.FromType(EmoteType.ANGRY_ROCK_HERO),
                Emote.FromType(EmoteType.LAUGHING_PAPER_HERO)
            };
        }

        /// <inheritdoc/>
        public Rarity Rarity
        {
            get
            {
                if (EmoteType >= EmoteType.FIRST_COMMON && EmoteType <= EmoteType.LAST_COMMON)
                    return Rarity.COMMON;

                if (EmoteType >= EmoteType.FIRST_RARE && EmoteType <= EmoteType.LAST_RARE)
                    return Rarity.RARE;

                if (EmoteType >= EmoteType.FIRST_EPIC && EmoteType <= EmoteType.LAST_EPIC)
                    return Rarity.EPIC;

                throw new ArgumentException("Emote doesn't exist or is out of rarity bounds.");
            }
        }

        /// <inheritdoc/>
        public string Name
        {
            get
            {
                switch (EmoteType)
                {
                    case EmoteType.THUMBS_UP:
                        return "Pouce";
                    case EmoteType.PEACE_SIGN:
                        return "Peace";
                    case EmoteType.OK_HAND:
                        return "OK";
                    case EmoteType.ALOHA_SIGN:
                        return "Aloha";
                    case EmoteType.STOP_SIGN:
                        return "Stop";
                    case EmoteType.HUNDRED_EMOJI:
                        return "100";
                    case EmoteType.FIRE_EMOJI:
                        return "Feu";
                    case EmoteType.ROCK_HAND:
                        return "Pierre";
                    case EmoteType.PAPER_HAND:
                        return "Feuille";
                    case EmoteType.SCISSORS_HAND:
                        return "Ciseaux";
                    case EmoteType.CRYING_SCISSORS_HERO:
                        return "Tristesse";
                    case EmoteType.ANGRY_ROCK_HERO:
                        return "Colère";
                    case EmoteType.LAUGHING_PAPER_HERO:
                        return "Joie";
                    default:
                        throw new ArgumentException("Emote doesn't exist.");
                }
            }
        }
    }

    /// <summary>
    ///     Représente les types d'emote de l'application
    /// </summary>
    public enum EmoteType
    {
#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement
        FIRST_COMMON = 0,
        THUMBS_UP = FIRST_COMMON,
        PEACE_SIGN,
        OK_HAND,
        ALOHA_SIGN,
        LAST_COMMON = ALOHA_SIGN,

        FIRST_RARE,
        STOP_SIGN = FIRST_RARE,
        HUNDRED_EMOJI,
        FIRE_EMOJI,
        ROCK_HAND,
        PAPER_HAND,
        SCISSORS_HAND,
        LAST_RARE = SCISSORS_HAND,

        FIRST_EPIC,
        CRYING_SCISSORS_HERO = FIRST_EPIC,
        ANGRY_ROCK_HERO,
        LAUGHING_PAPER_HERO,
        LAST_EPIC = LAUGHING_PAPER_HERO

#pragma warning restore CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement
    }
}
