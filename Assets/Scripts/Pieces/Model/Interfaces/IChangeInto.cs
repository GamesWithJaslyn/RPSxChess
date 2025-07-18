using UnityEngine;

/// <summary>
/// Represents a piece that can change into another type in an RPSxChess game.
/// This interface defines the methods required for a piece to change its type,
/// allowing for dynamic gameplay and strategy.
/// </summary>
public interface IChangeInto
{
    /// <summary>
    /// Changes the piece into another type.
    /// </summary>
    void ChangeInto();

    /// <summary>
    /// Checks if the piece can change into another type.
    /// </summary>
    /// <returns>True if the piece can change, otherwise false.</returns>
    /// bool CanChange();
}
