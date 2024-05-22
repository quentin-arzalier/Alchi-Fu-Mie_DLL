using AFM_DLL.Models.Enum;

namespace AFM_DLL.Helpers
{
    /// <summary>
    /// Contient des méthodes pratiques pour la partie
    /// </summary>
    public static class FightHelper
    {
        /// <summary>
        /// Réalise un duel entre deux éléments et indique l'issue du combat par rapport à l'élément de base
        /// </summary>
        /// <param name="element">
        ///     L'élément "joueur" pour lequel le résultat renvoyé sera appliqué.
        /// </param>
        /// <param name="enemy">
        ///     L'élément "ennemi" auquel l'élément "joueur" sera comparé
        /// </param>
        /// <returns></returns>
        public static FightResult ElementFight(Element element, Element enemy)
        {
            switch (element)
            {
                case Element.ROCK when enemy == Element.SCISSORS:
                case Element.SCISSORS when enemy == Element.PAPER:
                case Element.PAPER when enemy == Element.ROCK:
                    return FightResult.WIN;

                case Element.SCISSORS when enemy == Element.ROCK:
                case Element.ROCK when enemy == Element.PAPER:
                case Element.PAPER when enemy == Element.SCISSORS:
                    return FightResult.LOSE;

                default:
                    return FightResult.DRAW;
            }
        }
    }
}
