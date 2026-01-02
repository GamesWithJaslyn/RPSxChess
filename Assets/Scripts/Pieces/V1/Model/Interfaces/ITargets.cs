using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents a piece that can attack other pieces in an RPSxChess game.
/// This interface defines the methods required for a piece to determine its attack targets.
/// It is used to manage the interactions between pieces, allowing them
///  to identify which other pieces they can attack.
/// </summary>
public interface ITargets 
{

    /// <summary>
    /// Gets the list of piece types this piece can attack.
    /// </summary>
    /// <returns> The list of the types of pieces this piece can attack. </returns>
    List<int> GetTargetTypes();

    /// <summary>
    /// Gets the list of pieces this piece can attack.
    /// </summary>
    /// <returns> The list of pieces this piece can attack. </returns>
    List<AAttackingPiece> GetTargets();

    /// <summary>
    /// Gets the tiles this piece can attack.
    /// </summary>
    /// <returns> The tiles this piece can attack based on its attack rules. </returns>
    List<int> GetTargetTiles();
}
