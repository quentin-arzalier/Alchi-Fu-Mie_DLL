using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;

namespace AFM_DLL.Models.Cards.Spells
{
    /// <summary>
    ///     Représente le sortilège qui double les dégâts des deux côtés pendant un tour uniquement
    /// </summary>
    public class DoubleDamageSpell : SpellCard
    {
        /// <inheritdoc/>
        public override void ActivateSpell(Board board, bool isBlueSide)
        {
            board.Modifiers.Add(BoardModifiers.DOUBLE_DAMAGE);
        }

        /// <inheritdoc/>
        public override string GetDescription()
        {
            return "Double les dégâts infligés par vous ET votre adversaire pour ce tour uniquement";
        }

        /// <inheritdoc/>
        public override uint GetManaCost()
        {
            return 3;
        }

        /// <inheritdoc/>
        public override SpellType SpellType => SpellType.DOUBLE_DAMAGE;
    }
}
