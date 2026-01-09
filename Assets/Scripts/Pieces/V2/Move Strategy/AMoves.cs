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
            Debug.Log("[AMove] - possible tiles: " + targetTile);
            //if tile is not inside this board (out of bounds), break.
            if (!board.IsInside(targetTile)) { continue; ; }
            ITileModel tile = board.GetTileAt(targetTile);
            //if tile is not enterable, break.
            if (!tile.Rule.CanEnter(tile, piece)) { continue; }

            IPieceModel_V2 targetPiece = board.GetPieceAt(targetTile);
            //no piece on the target tile, can move onto it
            if (targetPiece == null || targetPiece.IsAlive == false)
            {
                moves.Add(new MoveBuilder(new Move(piece.Position, targetTile)).BuildMove());
            }
            else if (targetPiece.Team != piece.Team)
            {
                if (piece is AAttackingPiece_V2 attacking)
                {
                    if (targetPiece.PieceType == attacking.TargetType
                        && piece.IsItPossibleToChange())
                    {
                        Debug.Log("[AMove] - other piece IS a target and can promote!");
                        moves.Add(new MoveBuilder(new Move(piece.Position, targetTile))
                                    .AddFlags(MoveFlags.Capture | MoveFlags.Promotion).BuildMove());
                    }
                    else if (targetPiece.PieceType == attacking.TargetType)
                    {
                        Debug.Log("[AMove] - other piece IS a target");

                        moves.Add(new MoveBuilder(new Move(piece.Position, targetTile))
                                                .AddFlags(MoveFlags.Capture).BuildMove());
                    }
                }
                else if (piece.IsItPossibleToChange())
                {
                    Debug.Log("[AMove] - can promote piece");
                    moves.Add(new MoveBuilder(new Move(piece.Position, targetTile))
                                            .AddFlags(MoveFlags.Promotion).BuildMove());
                }
            }
        }

        return moves;
    }
}