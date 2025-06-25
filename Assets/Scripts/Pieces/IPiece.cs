
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
public interface IPiece
{
    /// <summary>
    /// Sets the position of this IPiece on the board.
    /// - int tile: The number of a tile on the board.
    /// </summary>
    void SetCoordinates(int tile);

    /// <summary>
    /// Gets the coordinates of this IPiece.
    /// </summary>
    /// returns> The int representing the tile this IPiece is on. </returns>
    int GetCoordinates();

    /// <summary>
    /// Gets the tiles this IPiece can attack.
    /// </summary>
    /// returns> The tiles this piece can attack based on this piece's attack rules. </returns>
    List<int> GetTargetTiles();

    /// <summary>
    /// Gets the tiles this IPiece can move to.
    /// </summary>
    /// returns> The possible moves based on the piece's movement rules. </returns>
    List<int> GetMoveTile();

    /// <summary>
    /// Gets the valid tiles this IPiece can move to 
    /// (uses GetMoveTile to parse if it can make any of those moves first).
    /// </summary>
    /// <returns> The current valid moves of this Piece. </returns>
    List<int> GetValidMoveTile();

    /// <summary>
    /// Gets the type of this IPiece.
    /// </summary>
    /// returns> A string representing the type of this IPiece. </returns>
    PieceType GetType();


}

/// <summary>
/// Represents the type of a piece in the RPSxChess game.
/// This enum defines the different types of pieces that can be used in the game.
/// Each piece type can have its own unique movement and attack patterns.
/// <br/>
/// The enum allows for easy identification and management of different piece types.
/// <br/>
/// It can be extended to include more piece types as the game evolves.
/// <br/>
/// Example piece types include Bow, Pegasus, and Sword.
/// <br/>
/// </summary>
public enum PieceType
{
    Bow,
    Pegasus,
    Sword,
    // Add more piece types as needed
}
