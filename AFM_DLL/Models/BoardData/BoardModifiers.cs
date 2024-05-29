namespace AFM_DLL.Models.BoardData
{
    /// <summary>
    ///     Correspond aux modificateurs qui peuvent être appliqués au plateau
    /// </summary>
    public enum BoardModifiers
    {
        /// <summary>
        ///     Double les dégâts lors de l'évaluation de ces derniers
        /// </summary>
        DOUBLE_DAMAGE,
        /// <summary>
        ///     Fait gagner le joueur bleu en cas d'egalite dans une colonne
        /// </summary>
        BLUE_PLAYER_WIN_TIE,
        /// <summary>
        ///     Fait gagner le joueur rouge en cas d'egalite dans une colonne
        /// </summary>
        RED_PLAYER_WIN_TIE,
    }
}
