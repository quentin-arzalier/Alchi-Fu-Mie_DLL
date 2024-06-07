using AFM_DLL.Models.BoardData;
using AFM_DLL.Models.Enum;
using AFM_DLL.Models.UnityResults;
using System;
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
        /// <param name="blueCard">
        ///     L'élément bleu du duel.
        /// </param>
        /// <param name="redCard">
        ///     L'élément rouge du duel
        /// </param>
        /// <returns>
        ///     Une enum indiquant l'issue du combat
        /// </returns>
        public static FightResult ElementFight(Element blueCard, Element redCard)
        {
            switch (blueCard)
            {
                case Element.ROCK when redCard == Element.SCISSORS:
                case Element.SCISSORS when redCard == Element.PAPER:
                case Element.PAPER when redCard == Element.ROCK:
                    return FightResult.BLUE_WIN;

                case Element.SCISSORS when redCard == Element.ROCK:
                case Element.ROCK when redCard == Element.PAPER:
                case Element.PAPER when redCard == Element.SCISSORS:
                    return FightResult.RED_WIN;

                default:
                    return FightResult.DRAW;
            }
        }

        /// <summary>
        ///     Effectue les opérations d'un combat de colonne et inflige les dégâts corrects aux joueurs.
        /// </summary>
        /// <param name="board">
        ///     Le plateau de jeu à évaluer
        /// </param>
        /// <param name="column">
        ///     La colonne à évaluer
        /// </param>
        /// <returns>
        ///     Le résultat du combat pour la colonne
        /// </returns>
        public static ColumnFightResult EvaluateColumnFight(Board board, BoardPosition column)
        {
            var res = new ColumnFightResult();

            var blueBoard = board.GetAllyBoardSide(true);
            var redBoard = board.GetEnemyBoardSide(true);
            var blueCard = blueBoard.ElementCards[column];
            var redCard = redBoard.ElementCards[column];
            if (blueCard == null)
                res.CardFightResult = FightResult.RED_WIN;
            else if (redCard == null)
                res.CardFightResult = FightResult.BLUE_WIN;
            else
                res.CardFightResult = FightHelper.ElementFight(blueCard.ActiveElement, redCard.ActiveElement);

            var blueHero = blueBoard.Player.Deck.Hero;
            var redHero = redBoard.Player.Deck.Hero;

            bool blueWinsTie = board.Modifiers.Any(c => c == BoardModifiers.BLUE_PLAYER_WIN_TIE) && res.CardFightResult == FightResult.DRAW;
            bool redWinsTie = board.Modifiers.Any(c => c == BoardModifiers.RED_PLAYER_WIN_TIE) && res.CardFightResult == FightResult.DRAW;

            if (res.CardFightResult == FightResult.DRAW && blueWinsTie == redWinsTie)
                res.HeroFightResult = FightHelper.ElementFight(blueHero.ActiveElement, redHero.ActiveElement);

            // TODO : Voir si on souhaite cumuler les double damage
            //var damageMultiplier = (uint)Math.Pow(2, board.Modifiers.Count(c => c == BoardModifiers.DOUBLE_DAMAGE));
            uint damageMultiplier = (uint)(board.Modifiers.Any(c => c == BoardModifiers.DOUBLE_DAMAGE) ? 2 : 1);

            if (res.CardFightResult == FightResult.BLUE_WIN || res.HeroFightResult == FightResult.BLUE_WIN || blueWinsTie)
            {
                redBoard.Player.RemoveHealth(damageMultiplier);
                res.RedDamage = damageMultiplier;
                res.CardFightResult = FightResult.BLUE_WIN;
            }
            if (res.CardFightResult == FightResult.RED_WIN || res.HeroFightResult == FightResult.RED_WIN || redWinsTie)
            {
                blueBoard.Player.RemoveHealth(damageMultiplier);
                res.BlueDamage = damageMultiplier;
                res.CardFightResult = FightResult.RED_WIN;
            }
            if (res.CardFightResult == FightResult.DRAW && res.HeroFightResult == FightResult.DRAW && blueWinsTie == redWinsTie)
            {
                redBoard.Player.RemoveHealth(damageMultiplier);
                blueBoard.Player.RemoveHealth(damageMultiplier);
                res.BlueDamage = damageMultiplier;
                res.RedDamage = damageMultiplier;
            }

            return res;
        }

        /// <summary>
        ///     Renvoie les cartes sortilèges dans l'ordre d'évaluation en fonction des héros.
        ///     Si les héros ont le même type, le résultat est aléatoire.
        /// </summary>
        /// <param name="board">Le plateau qui contient les cartes sortilèges à évaluer</param>
        /// <returns>La liste ordonnées des cartes sortilèges ainsi que leur côté</returns>
        internal static SpellCardEvaluationResultInternal GetOrderedSpellCards(Board board)
        {
            var res = new SpellCardEvaluationResultInternal();

            if (board.BlueSide.SpellCard != null && board.RedSide.SpellCard != null)
            {
                res.HeroFightResult = ElementFight(
                    board.BlueSide.Player.Deck.Hero.ActiveElement,
                    board.RedSide.Player.Deck.Hero.ActiveElement
                );

                if (res.HeroFightResult == FightResult.BLUE_WIN || (res.HeroFightResult == FightResult.DRAW && new Random().Next(2) == 0))
                {
                    if (res.HeroFightResult.Value == FightResult.DRAW)
                        res.BlueSideStartedOnDraw = true;
                    if (board.BlueSide.SpellCard.CanBeActived)
                        res.SpellsInOrder.Add((board.BlueSide.SpellCard, true));
                    if (board.RedSide.SpellCard.CanBeActived)
                        res.SpellsInOrder.Add((board.RedSide.SpellCard, false));
                }
                else
                {
                    if (board.RedSide.SpellCard.CanBeActived)
                        res.SpellsInOrder.Add((board.RedSide.SpellCard, false));
                    if (board.BlueSide.SpellCard.CanBeActived)
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
