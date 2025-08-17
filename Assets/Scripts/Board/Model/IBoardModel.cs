using System.Collections.Generic;
using UnityEngine;

public interface IBoardModel
{
    /// <summary>
    /// Selects an IPieceModel.
    /// </summary>
    /// <param name="pos"> The position being selected</param>
    /// <returns> Returns the Piece that matches the position given.</returns>
    AAttackingPiece SelectPiece(int pos);

    /// <summary>
    /// Adds a Piece to the Board.
    /// </summary>
    /// <param name="piece"> The piece being added to the board</param>
    void AddPiece(AAttackingPiece piece);

    /// <summary>
    /// Moves the Selected Piece 
    /// </summary>
    /// <param name="toTile">The tile that the piece will move to</param>
    void MovePiece(int toTile);

    /// <summary>
    /// Changes the Piece that reaches the opposite side of its
    /// starting position to the given type.
    /// </summary>
    /// <param name="type"> The int representing the type that the
    /// Piece will be changed into.</param>
    void ChangePieceType(int type);

    /// <summary>
    /// Switches the turn to the other player.
    /// </summary>
    void SwitchTurn();

    /// <summary>
    /// Checks if it is currently Blue's turn.
    /// </summary>
    /// <returns> The boolean representing if it is Blue's turn.</returns>
    bool IsBlueTurn();

    /// <summary>
    /// Gets all Pieces on the board.
    /// </summary>
    /// <returns></returns>
    List<AAttackingPiece> GetAllPieces();

    /// <summary>
    /// Gets all Tiles on the board.
    /// </summary>
    /// <returns> Returns a List containing all tiles on the board. </returns>
    List<ITileModel> GetAllTiles();

    /// <summary>
    /// Gets all enterable Tiles on the board.
    /// </summary>
    /// <returns> Returns a List containing all enterable tiles on the board. </returns>
    List<IEnterAndLeave> GetAllEnterableTiles();

    /// <summary>
    /// Resets the board to its initial state.
    /// </summary>
    ///void ResetBoard();
}
