using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents a piece that can move to different tiles on the board in an RPSxChess game.
/// This interface defines the methods required for a piece to determine its movement capabilities,
/// allowing for dynamic gameplay and strategy.
/// </summary>
public interface IMoveTo
{
    /// <summary>
    /// Sets the position of this IPiece on the board.
    /// - int tile: The number of a tile on the board.
    /// </summary>
    void SetPos(int tile);

    /// <summary>
    /// Gets the valid tiles this IPiece can move to 
    /// (uses GetMoveTile to parse if it can make any of those moves first).
    /// </summary>
    /// <returns> The current valid moves of this Piece. </returns>
    List<int> GetValidMoveTiles();
}
