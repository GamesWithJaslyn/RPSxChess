using System.Collections.Generic;
using Unity;

public enum MoveFlags { None, Capture, Promotion }

public struct Move
{
    public int _from { get; set; }
    public int _to { get; set; }
    public MoveFlags _flags { get; set; }
    public PieceType _promotionType { get; set; }

    public Move (int from, int to) : this ()
    {
        _from = from;
        _to = to;
    }
}

public class MoveBuilder
{
    private Move _move;
    public MoveBuilder(Move move)
    {
        _move = move;
    } 

    public MoveBuilder AddFlags(MoveFlags addFlag)
    {
        _move._flags = addFlag;
        return this;
    }

    public MoveBuilder SetPromotionType(PieceType type)
    {
        _move._promotionType = type;
        return this;
    }

    public Move BuildMove() { return _move; }
}

public interface IMoveStrategy 
{ 
    /// <summary>
    /// Gets the valid moves of a Piece.
    /// </summary>
    /// <param name="board"> The board the given Piece is on. </param>
    /// <param name="piece"> The Piece that is going to make a move. </param>
    /// <returns> A List of Move </returns>
    List<Move> GetValidMoves(IBoardModel_V2 board, IPieceModel_V2 piece); 
}

