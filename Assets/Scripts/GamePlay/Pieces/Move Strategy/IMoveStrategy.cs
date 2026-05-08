using System.Collections.Generic;
using Unity;

public enum MoveFlags { None, Capture, Promotion }

public struct Move
{
    public int From { get; set; }
    public int To { get; set; }
    public List<MoveFlags> Flags { get; set; }
    public PieceType PromotionType { get; set; }

    public Move(int from, int to) : this()
    {
        From = from;
        To = to;
    }
}

public class MoveBuilder
{
    private Move _move;
    public MoveBuilder(Move move)
    {
        _move = move;
    }

    public MoveBuilder AddFlags(List<MoveFlags> addFlag)
    {
        _move.Flags = addFlag;
        return this;
    }

    public MoveBuilder SetPromotionType(PieceType type)
    {
        _move.PromotionType = type;
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
    List<Move> GetValidMoves(IBoardModel board, IPieceModel piece);
}

