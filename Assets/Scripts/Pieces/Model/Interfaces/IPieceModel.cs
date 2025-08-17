
using System.Collections.Generic;

/// <summary>
/// Represents a Piece in an RPSxChess game.
/// This interface can be implemented by various piece types such as Bow, Pegasus, and Sword pieces.
/// <br/>
/// The pieces can be used in a chess-like game where players can move them on a board
/// and engage in battles based on their unique targets, and move and attack pattern.
/// <br/>
/// The interface serves as a common contract for all piece types, allowing for polymorphism
/// and easier management and scalibility of different and new pieces in the game.
/// </summary>
public interface IPieceModel
{
    /// <summary>
    /// Sets the position of this IPiece on the board.
    /// - int tile: The number of a tile on the board.
    /// </summary>
    void SetPos(int tile);

    /// <summary>
    /// Gets the position of this IPiece.
    /// </summary>
    /// <returns> The int representing the ID of the tile this IPiece is on. </returns>
    int GetPos();

    /// <summary>
    /// Gets the tiles this IPiece can move to.
    /// </summary>
    /// <returns> The possible moves based on the piece's movement rules. </returns>
    List<int> GetMoveTiles();

    /// <summary>
    /// Gets the type of this IPiece.
    /// </summary>
    /// <returns> An int representing the type of this IPiece. </returns>
    int GetPieceType();

    /// <summary>
    /// Sets the type of this IPiece.
    /// </summary>
    void SetPieceType(int type);

    /// <summary>
    /// Checks if this IPiece is selected.
    /// A piece is considered selected if it is currently being interacted with by the player.
    /// </summary>
    /// <returns> A boolean indicating whether this IPiece is selected. </returns>
    bool IsSelected();

    /// <summary>
    /// Sets the selected state of this IPiece to true, and the rest to false.
    /// </summary>
    void SetSelected();

    /// <summary>
    /// Sets the selected state of this IPiece to false.
    /// </summary>
    void TurnSelectedFalse();

     /// <summary>
    /// Checks if this IPiece is alive.
    /// A piece is considered alive if it has not been captured or removed from the game.
    /// </summary>
    bool IsAlive();

    /// <summary>
    /// Turns this IPiece's isALive boolean to false.
    /// </summary>
    void SetDead();

    /// <summary>
    /// Returns true if this IPiece's team is the same as the given String.
    /// </summary
    /// <returns> A Boolean </returns>
    bool IsSameTeam(string team);

    /// <summary>
    /// Changes this IPiece's type into the given one.
    /// <summary>
    void ChangeInto(int newType);

    /// <summary>
    /// Checks if it's possible for this IPiece to change.
    /// <summary>
    bool CanChange();

}
