namespace AFM_DLL.Models.BoardData
{
    /// <summary>
    ///     Représente l'état du plateau
    /// </summary>
    public enum BoardState
    {
        /// <summary>
        ///     Phase de distribution des cartes
        /// </summary>
        DRAW_CARDS,

        /// <summary>
        ///     Phase de placement des cartes
        /// </summary>
        PLAY_CARDS,

        /// <summary>
        ///     Phase d'évaluation des sortilèges
        /// </summary>
        EVALUATE_SPELLS,

        /// <summary>
        ///     Phase d'évaluation des éléments
        /// </summary>
        EVALUATE_ELEMENTS,

        /// <summary>
        ///     Phase de réinitialisation du plateau
        /// </summary>
        RESET_BOARD
    }
}
