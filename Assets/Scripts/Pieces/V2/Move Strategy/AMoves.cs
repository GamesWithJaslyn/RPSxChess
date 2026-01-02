using System.Collections.Generic;

/// <summary>
/// The Abstract class for moves.
/// Adds a Move to the list if the tile is a possible move, 
/// if the tile is enterable, piece can capture, and/or can promote itself.
/// </summary>
public abstract class AMoves : IMoveStrategy
{
    public abstract List<int> GetMoveTiles(int piecePos);
    public List<Move> GetValidMoves(IBoardModel_V2 board, IPieceModel_V2 piece)
    {
        List<int> possibleMoves = GetMoveTiles(piece.GetPos());
        List<Move> moves = new List<Move>();

        foreach(int targetTile in possibleMoves)
        {
            
            if (!board.IsInside(targetTile)) { break; } //if tile is not inside this board (out of bounds), break.
            if (!(board.GetTileAt(targetTile) is IEnterAndLeave)) { break; } //if tile is not enterable, break.

            IPieceModel_V2 targetPiece = board.GetPieceAt(targetTile);

            if (targetPiece == null) //no piece on the target tile, can move onto it
            { 
                moves.Add(new MoveBuilder(new Move( piece.GetPos(), targetTile)).BuildMove()); 
            }
            else if (targetPiece.GetTeam() != piece.GetTeam())
            {
                if (piece is AAttackingPiece_V2 attacking)
                {
                    if (targetPiece.GetPieceType() == attacking.GetTargetType()
                        && piece.IsItPossibleToChange())
                    {
                        moves.Add(new MoveBuilder(new Move( piece.GetPos(), targetTile))
                                                .AddFlags(MoveFlags.Capture | MoveFlags.Promotion).BuildMove());
                    }
                    else if ( targetPiece.GetPieceType() == attacking.GetTargetType())
                    {
                        moves.Add(new MoveBuilder(new Move( piece.GetPos(), targetTile))
                                                .AddFlags(MoveFlags.Capture).BuildMove());
                    }
                }
                else if (piece.IsItPossibleToChange())
                {
                    moves.Add(new MoveBuilder(new Move( piece.GetPos(), targetTile))
                                            .AddFlags(MoveFlags.Promotion).BuildMove());
                }
            }
        }

        return moves;
    }
}