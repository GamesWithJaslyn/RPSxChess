using System.Collections.Generic;
using UnityEngine;

public interface ISpecialAttack
{
    public bool IsSpecialAttackReady { get; }

    /// <summary>
    /// Executes a special attack for a piece.
    /// </summary>
    /// <param name="board"> The board the given Piece is on. </param>
    /// <param name="piece"> The Piece that is going to make the special attack. </param>
    bool ExecuteSpecialAttack(IBoardModel board, IPieceModel piece, int targetTileID);

    /// <summary>
    /// Shows the special attack range for a piece.
    /// </summary>
    /// <param name="board"> The board the given Piece is on. </param>
    /// <param name="piece"> The Piece that is going to show the special attack range. </param>
    /// <returns> A list of tile IDs that are in the special attack range. </returns>
    List<int> GetSpecialAttackRange(IBoardModel board, IPieceModel piece);

    void CancelSpecialAttack(IBoardModel board);
}
