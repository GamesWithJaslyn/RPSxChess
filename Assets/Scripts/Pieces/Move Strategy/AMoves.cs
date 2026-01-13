using System.Collections.Generic;
using Mono.CSharp;
using UnityEngine;

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
        List<int> possibleMoves = GetMoveTiles(piece.Position);
        List<Move> moves = new List<Move>();

        foreach (int targetTile in possibleMoves)
        {
            //if tile is not inside this board (out of bounds), break.
            if (!board.IsInside(targetTile)) { continue; ; }
            ITileModel tile = board.GetTileAt(targetTile);
            //if tile is not enterable, break.
            if (!tile.Rule.CanEnter(tile, piece)) { continue; }

            IPieceModel_V2 targetPiece = board.GetPieceAt(targetTile);
            //no piece on the target tile, can move onto it
            if (targetPiece == null || targetPiece.IsAlive == false)
            {
                if (piece.IsItPossibleToChange(targetTile))
                {
                    moves.Add(new MoveBuilder(new Move(piece.Position, targetTile))
                                            .AddFlags(new List<MoveFlags> { MoveFlags.Promotion })
                                            .BuildMove());
                    continue;
                }
                else
                {
                    moves.Add(new MoveBuilder(new Move(piece.Position, targetTile))
                    .AddFlags(new List<MoveFlags>()).BuildMove());
                    continue;
                }

            }
            else if (targetPiece.Team != piece.Team)
            {
                if (piece is AAttackingPiece_V2 attacking)
                {
                    if (targetPiece.PieceType == attacking.TargetType
                    && attacking.IsItPossibleToChange(targetTile))
                    {
                        moves.Add(new MoveBuilder(new Move(piece.Position, targetTile))
                        .AddFlags(new List<MoveFlags> { MoveFlags.Capture, MoveFlags.Promotion })
                        .BuildMove());
                        continue;
                    }
                    else if (targetPiece.PieceType == attacking.TargetType)
                    {
                        moves.Add(new MoveBuilder(new Move(piece.Position, targetTile))
                                            .AddFlags(new List<MoveFlags> { MoveFlags.Capture })
                                            .BuildMove());
                        continue;
                    }
                }
            }
        }

        return moves;
    }
}