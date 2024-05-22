using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Cards;
using AFM_DLL.Models.Enum;
using AFM_DLL.Models.UnityResults;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        ///     Renvoie les cartes sortilèges dans l'ordre d'évaluation en fonction des héros.
        ///     Si les héros ont le même type, le résultat est aléatoire.
        /// </summary>
        /// <param name="board">Le plateau qui contient les cartes sortilèges à évaluer</param>
        /// <returns>La liste ordonnées des cartes sortilèges ainsi que leur côté</returns>
        public static SpellCardEvaluationResult GetOrderedSpellCards(Board board)
        {
            var res = new SpellCardEvaluationResult();

            if (board.BlueSide.SpellCard != null && board.RedSide.SpellCard != null)
            {
                res.HeroFightResult = ElementFight(
                    board.BlueSide.Player.Deck.Hero.ActiveElement,
                    board.RedSide.Player.Deck.Hero.ActiveElement
                );

                if (res.HeroFightResult == FightResult.WIN || new Random().Next(2) == 0)
                {
                    if (res.HeroFightResult.Value == FightResult.DRAW)
                        res.BlueSideStartedOnDraw = true;

                    res.SpellsInOrder.Add((board.BlueSide.SpellCard, true));
                    res.SpellsInOrder.Add((board.RedSide.SpellCard, false));
                }
                else
                {
                    res.SpellsInOrder.Add((board.RedSide.SpellCard, false));
                    res.SpellsInOrder.Add((board.BlueSide.SpellCard, true));
                }
            }
            else if (board.BlueSide.SpellCard != null)
            {
                res.SpellsInOrder.Add((board.BlueSide.SpellCard, true));
            }
            else if (board.RedSide.SpellCard != null)
            {
                res.SpellsInOrder.Add((board.RedSide.SpellCard, false));
            }
            return res;
        }
    }
}
