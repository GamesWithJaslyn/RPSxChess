using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardModel
{
    /// <summary>
    /// Unselects the previously selected IPiece.
    /// </summary>
    void UnSelectPiece();

    /// <summary>
    /// Selects an IPieceModel.
    /// </summary>
    /// <param name="pos"> The position being selected</param>
    /// <returns> Returns the Piece that matches the position given.</returns>
    IPieceModel SelectPiece(int pos);

    /// <summary>
    /// Gets the current selected IPieceModel.
    /// </summary>
    /// <returns> An IPieceModel </returns>
    IPieceModel GetSelectedPiece();

    /// <summary>
    /// Gets all Pieces on the board.
    /// </summary>
    /// <returns></returns>
    List<IPieceModel> GetAllPieces();

    /// <summary>
    /// Gets all Tiles on the board.
    /// </summary>
    /// <returns> Returns a List containing all tiles on the board. </returns>
    List<ITileModel> GetAllTiles();

    /// <summary>
    /// Gets the Piece on the given tile ID.
    /// If there is no piece, it returns null.
    /// </summary>
    /// <param name="tileID"> the given Tile being checked. </param>
    /// <returns> IPieceModel or null. </returns>
    IPieceModel GetPieceAt(int tileID);

    /// <summary>
    /// Gets the Tile given the tile ID.
    /// If there is no tile with that ID, it returns null.
    /// </summary>
    /// <param name="tileID"> the given ID being checked. </param>
    /// <returns> ITileModel or null. </returns>
    ITileModel GetTileAt(int tileID);


    void SetPiecesDictionary(Dictionary<IPieceModel, GameObject> pieceKey);

    /// <summary>
    /// Sets the valid move tiles for the selected piece.
    /// </summary>
    /// <param name="validMoveTiles"> A List of integers representing the valid move tiles. </param>
    void SetTilesAsValidMoveTiles(List<Move> validMoveTiles);


    /// <summary>
    /// Adds a Piece to the Board.
    /// </summary>
    /// <param name="piece"> The piece being added to the board</param>
    void AddPiece(IPieceModel piece);

    /// <summary>
    /// Removes a Piece from this Board. 
    /// If the piece is not on the this Board, it exits the method.
    /// </summary>
    /// <param name="piece"> The piece being removed from this Board </param>
    void RemovePiece(IPieceModel piece);

    void ShowArrow();
    void ResetArrow();


    /// <summary>
    /// Checks if the given tile ID is inside this IBoardModel.
    /// </summary>
    /// <param name="tileID"></param>
    /// <returns></returns>
    bool IsInside(int tileID);
    bool TryMovePiece(int toTile);

    /// <summary>
    /// Moves the Selected Piece 
    /// </summary>
    /// <param name="toTile">The tile that the piece will move to</param>
    bool MovePiece(int toTile);

    /// <summary>
    /// Switches the turn to the other player.
    /// </summary>
    void SwitchTurn();

    /// <summary>
    /// Checks if it is currently Blue's turn.
    /// </summary>
    /// <returns> The boolean representing if it is Blue's turn.</returns>
    bool IsBlueTurn();

    void UnHighlightAllTiles();

    /// <summary>
    /// Resets the board to its initial state.
    /// </summary>
    void ResetBoard();
}
