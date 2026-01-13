
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
    /// </summary>
    public event Action OnDeath;

    /// <summary>
    /// Event triggered when this Piece changes type.
    /// </summary>
    public event Action OnChangeInto;

    /// <summary>
    /// Event triggered when this Piece gets reset.
    /// </summary>
    public event Action OnReset;

    Team Team { get; }
    int Position { get; }
    PieceType PieceType { get; set; }

    /// <summary>
    /// Gets the List of tiles this IPiece can move to.
    /// </summary>
    /// <returns> The IMoveStrategy class </returns>
    List<Move> GetValidMoves();

    /// <summary>
    /// Sets the position of this Piece on the board only if the given ID is within its valid moves.
    /// Invokes the OnMove event, and turns this Pieces selected to false.
    /// <paramref name="tile"/> The ID of the tile this Piece will move into.
    /// </summary>
    void SetPos(int tile);

    Move MakeMove(int tile);
    /// <summary>
    /// Sets the type of this IPiece.
    /// Invokes the OnChangeInto event, and updates this Piece's IMoveStrategy.
    /// <paramref name="type"/> The PieceType this piece will change into.
    /// </summary>
    void SetPieceType(PieceType type);

    /// <summary>
    /// Turns this IPiece's isALive boolean to false.
    /// </summary>
    void SetDead();


    bool IsPromoted { get; set; }
    bool IsSelected { get; set; }
    bool IsAlive { get; }

    /// <summary>
    /// Returns true if this IPiece's team is the same as the given Team type.
    /// </summary
    /// <returns> A Boolean </returns>
    bool IsSameTeam(Team team);

    /// <summary>
    /// Checks if it's possible for this IPiece to change based on the given tile ID.
    /// <paramref name="pos"/> the ID of a tile being checked
    /// <summary>
    bool IsItPossibleToChange(int ID);

    /// <summary>
    /// Moves this IPiece to the given tile ID.
    /// <paramref name="newPos"/> The ID of a tile.
    /// <paramref name="pieceModel"/> The Piece that is on the given tile. (can be null)
    /// </summary>
    /// <returns> A bool indicating whether this Piece was able to move successfully </returns>
    //bool MoveTo(int newPos, IPieceModel_V2 pieceModel);


    /// <summary>
    /// Changes this Piece's type into the given type;
    /// </summary>
    /// <param name="type"> the type this Piece will change into </param>
    public void ChangeInto(PieceType type);

    /// <summary>
    /// Creates a Deep Copy of this piece.
    /// </summary>
    void Reset();

}
