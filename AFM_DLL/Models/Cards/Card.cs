using AFM_DLL.Models.BoardData;

namespace AFM_DLL.Models.Cards
{
    /// <summary>
    /// Classe abstraite qui représente toutes les cartes du jeu
    /// </summary>
    public abstract class Card
    {
        /// <summary>
        ///     Permet d'ajouter une carte au plateau donné
        /// </summary>
        /// <param name="board">Le plateau de jeu sur lequel l'opération a lieu</param>
        /// <param name="isBlueSide">Indique si c'est le joueur bleu ou le joueur rouge qui joue la carte</param>
        /// <param name="position">Indique sur quelle colonne la carte doit être ajoutée (null pour les sortilèges)</param>
        /// <returns>Si l'ajout de carte a eu lieu avec succès</returns>
        public virtual bool AddToBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            return board.CanCardsBePlayedOrRemoved(isBlueSide);
        }

        /// <summary>
        ///     Permet de retirer une carte au plateau donné
        /// </summary>
        /// <param name="board">Le plateau de jeu sur lequel l'opération a lieu</param>
        /// <param name="isBlueSide">Indique si c'est le joueur bleu ou le joueur rouge qui retire la carte</param>
        /// <param name="position">Indique sur quelle colonne la carte doit être retirée (null pour les sortilèges)</param>
        /// <returns>Si le retrait de carte a eu lieu avec succès</returns>
        public virtual bool RemoveFromBoard(Board board, bool isBlueSide, BoardPosition? position)
        {
            return board.CanCardsBePlayedOrRemoved(isBlueSide);
        }

        /// <summary>
        ///     Retire la carte du plateau une fois le tour fini
        /// </summary>
        /// <param name="side"></param>
        /// <param name="position">Indique sur quelle colonne la carte doit être retirée (null pour les sortilèges)</param>
        /// 
        /// <returns>Si le retrait de carte a eu lieu avec succès</returns>
        internal abstract bool DiscardFromBoardSide(BoardSide side, BoardPosition? position);
    }
}
