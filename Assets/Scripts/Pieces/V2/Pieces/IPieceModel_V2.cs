
using System;
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
public interface IPieceModel_V2
{

    /// <summary>
    /// Event triggered when this Piece moves to a new position.
    /// The int parameter represents the new position of the piece.
    /// </summary>
    public event Action<int> OnMoved;

    /// <summary>
    /// Event triggered when the Piece dies.
    /// The bool parameter indicates whether this Piece is dead.
    /// </summary>
    public event Action OnDeath;

    /// <summary>
    /// Event triggered when this Piece changes type.
    /// The PieceType parameter indicates the type this Piece will change into.
    /// </summary>
    public event Action<PieceType> OnChangeInto;

    /// <summary>
    /// Gets the IBoardModel of this Piece.
    /// </summary>
    /// <returns> an IBoardModel </returns>
    IBoardModel_V2 GetBoardModel();

    /// <summary>
    /// Gets the position of this IPiece.
    /// </summary>
    /// <returns> The int representing the ID of the tile this IPiece is on. </returns>
    int GetPos();

    /// <summary>
    /// Gets the type of this IPiece.
    /// </summary>
    /// <returns> An int representing the type of this IPiece. </returns>
    PieceType GetPieceType();

    /// <summary>
    /// Gets the List of tiles this IPiece can move to.
    /// </summary>
    /// <returns> The IMoveStrategy class </returns>
    IMoveStrategy GetValidMoveTiles();

    Team GetTeam();

    /// <summary>
    /// Sets the position of this Piece on the board.
    /// <paramref name="tile"/> The ID of the tile this Piece will move into.
    /// </summary>
    void SetPos(int tile);

    /// <summary>
    /// Sets the bool for if this IPiece has been promoted.
    /// <paramref name="promotion"/> The bool that indicates if this Piece has been promoted or not.
    /// </summary>
    void SetPromotion(bool promotion);

    /// <summary>
    /// Sets the type of this IPiece.
    /// <paramref name="type"/> The PieceType this piece will change into.
    /// </summary>
    void SetPieceType(PieceType type);

    /// <summary>
    /// Turns this IPiece's isALive boolean to false.
    /// </summary>
    void SetDead();

    /// <summary>
    /// Sets the selected state of this IPiece.
    /// <paramref name="selection"/> The bool indicating whether this Piece will be selected or unselected.
    /// </summary>
    void SetSelected(bool selection);

    /// <summary>
    /// Returns whether this Piece has been promoted.
    /// </summary>
    /// <returns> A bool </returns>
    bool IsPromoted();

    /// <summary>
    /// Checks if this IPiece is selected.
    /// A piece is considered selected if it is currently being interacted with by the player.
    /// </summary>
    /// <returns> A boolean indicating whether this IPiece is selected. </returns>
    bool IsSelected();

    /// <summary>
    /// Checks if this IPiece is alive.
    /// A piece is considered alive if it has not been captured or removed from the game.
    /// </summary>
    bool IsAlive();

    /// <summary>
    /// Returns true if this IPiece's team is the same as the given Team type.
    /// </summary
    /// <returns> A Boolean </returns>
    bool IsSameTeam(Team team);

    /// <summary>
    /// Checks if it's possible for this IPiece to change.
    /// <summary>
    bool IsItPossibleToChange();

    /// <summary>
    /// Returns how many pieces of a type is left.
    /// </summary>
    /// <returns> The int representing how many pieces, of that type that died, is left.</returns>
    public int PiecesLeft();

    /// <summary>
    /// Moves this IPiece to the given tile ID.
    /// <paramref name="newPos"/> The ID of a tile.
    /// <paramref name="pieceModel"/> The Piece that is on the given tile. (can be null)
    /// </summary>
    /// <returns> A bool indicating whether this Piece was able to move successfully </returns>
    bool MoveTo(int newPos, IPieceModel_V2 pieceModel);

    /// <summary>
    /// Changes this Piece's type into the given type;
    /// </summary>
    /// <param name="type"> the type this Piece will change into </param>
    public void ChangeInto(PieceType type);

    /// <summary>
    /// Creates a Deep Copy of this piece.
    /// </summary>
    IPieceModel_V2 CopyPiece();

}
