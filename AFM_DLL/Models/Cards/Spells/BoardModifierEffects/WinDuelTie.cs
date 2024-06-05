using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;

namespace AFM_DLL.Models.Cards.Spells.WinDuelTie
{
    class WinDuelTie : SpellCard
    {
        /// <inheritdoc/>
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            board.Modifiers.Add(isBlueSide ? BoardModifiers.BLUE_PLAYER_WIN_TIE : BoardModifiers.RED_PLAYER_WIN_TIE);
        }

        /// <inheritdoc/>
        public override string GetDescription()
        {
            return "Fait gagner le duel en cas d'egalite au joueur ayant lance ce sort.";
        }

        /// <inheritdoc/>
        public override uint GetManaCost()
        {
            return 3;
        }

        /// <inheritdoc/>
        public override SpellType GetSpellType()
        {
            return SpellType.WIN_DUEL_TIE;
        }
    }
}
