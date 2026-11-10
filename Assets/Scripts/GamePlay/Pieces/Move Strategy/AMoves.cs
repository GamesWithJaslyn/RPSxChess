using System.Collections.Generic;
using Mono.CSharp;
using UnityEngine;

enum BoardEdge { Top, Bottom, Left, Right }

/// <summary>
/// The Abstract class for moves.
/// Adds a Move to the list if the tile is a possible move, 
/// if the tile is enterable, piece can capture, and/or can promote itself.
/// </summary>
public abstract class AMoves : IMoveStrategy
{
    public abstract List<int> GetMoveTiles(int piecePos);
    public List<Move> GetValidMoves(IBoardModel board, IPieceModel piece)
    {
        List<int> possibleMoves = GetMoveTiles(piece.Position);
        List<Move> moves = new List<Move>();

        foreach (int targetTile in possibleMoves)
        {
            // if tile is not inside this board (out of bounds)
            if (!board.IsInside(targetTile)) { continue; }

            ITileModel tile = board.GetTileAt(targetTile);
            // if tile is not enterable
            if (!tile.Rule.CanEnter(tile, piece)) { continue; }

            // if the current piece is a king and on an enemy tile
            if (OnEnemyTileAsKing(board, (AAttackingPiece)piece, targetTile)) { continue; }

            if (!OnEnemyTileAsKing(board, (AAttackingPiece)piece, targetTile))
            {
                IPieceModel targetPiece = board.GetTileAt(targetTile).Occupant;
                if (targetPiece == null || targetPiece.IsAlive == false)
                {
                    EmptyTile(piece, targetTile, moves);
                    continue;
                }
                else if (targetPiece.Team != piece.Team && targetPiece.IsAlive)
                {
                    if (piece is AAttackingPiece attacking)
                    {
                        TargetOnTile(targetPiece, attacking, targetTile, moves);
                        continue;
                    }
                }
            }

        }

        return moves;
    }

    private void TargetOnTile(IPieceModel targetPiece, AAttackingPiece attacking,
        int targetTile, List<Move> moves)
    {
        if (targetPiece.PieceType == attacking.TargetType
                    && attacking.IsItPossibleToChange(targetTile))
        {
            moves.Add(CreateMove(attacking.Position, targetTile,
            new List<MoveFlags> { MoveFlags.Capture, MoveFlags.Promotion }));
        }
        else if (targetPiece.PieceType == attacking.TargetType)
        {
            moves.Add(CreateMove(attacking.Position, targetTile,
            new List<MoveFlags> { MoveFlags.Capture }));
        }
    }

    private void EmptyTile(IPieceModel piece, int targetTile, List<Move> moves)
    {
        if (piece.IsItPossibleToChange(targetTile))
        {
            moves.Add(CreateMove(piece.Position, targetTile,
            new List<MoveFlags> { MoveFlags.Promotion }));
        }
        else
        {
            moves.Add(CreateMove(piece.Position, targetTile,
             new List<MoveFlags>() { MoveFlags.None }));
        }
    }

    // private List<IPieceModel> GetEnemies(IBoardModel board, AAttackingPiece thisPiece)
    // {
    //     List<IPieceModel> enemies = new List<IPieceModel>();
    //     foreach (IPieceModel piece in board.GetAllPieces())
    //     {
    //         if (piece.PieceType == thisPiece.AttackerType)
    //         {
    //             enemies.Add(piece);
    //         }
    //     }
    //     return enemies;
    // }

    private bool OnEnemyTileAsKing(IBoardModel board, AAttackingPiece thisPiece, int targetTile)
    {
        if (!thisPiece.IsKing) { return false; }
        bool OnTile = false;

        List<IPieceModel> enemies = new List<IPieceModel>();
        Debug.Log($"[AMoves] - enemy is: {thisPiece.AttackerType}");
        Debug.Log($"[AMoves] - enemy is: {board.GetAllPieces().Count}");


        foreach (IPieceModel searching in board.GetAllPieces())
        {
            if (searching.PieceType == thisPiece.AttackerType && searching.Team != thisPiece.Team)
            {
                enemies.Add(searching);
            }
        }

        foreach (IPieceModel enemy in enemies)
        {
            foreach (Move tile in enemy.GetValidMoves())
            {
                if (tile.To == targetTile) { OnTile = true; }
            }
        }

        Debug.Log($"[AMoves] - count of enemies: {enemies.Count}");
        Debug.Log($"[AMoves] - it is {OnTile}");

        return OnTile;
    }



    private Move CreateMove(int from, int to, List<MoveFlags> flags)
    {
        return new MoveBuilder(new Move(from, to)).AddFlags(flags).BuildMove();
    }

}