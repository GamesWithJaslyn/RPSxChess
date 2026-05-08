using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Mono.CSharp;
using UnityEngine;

public class Bow_SpecialAttack : ASpecialAttack
{
    public override bool IsSpecialAttackReady { get; protected set; } = false;

    public override List<int> GetSpecialAttackRange(IBoardModel board, IPieceModel piece)
    {
        Debug.Log("Showing Bow Special Attack Range for piece at position: " + piece.Position);
        IsSpecialAttackReady = true;
        (int dr, int dc)[] attackRange = new (int, int)[0];

        IncreasingRow(ref attackRange);
        DecreasingRow(ref attackRange);

        List<int> withinBoardBounds = WithinBoardBounds.GetTiles(piece.Position, attackRange);

        ///return WithinBoardBounds.GetTiles(piece.Position, attackRange);


        return ValidAttackTiles(board, piece, withinBoardBounds);
    }

    private List<int> ValidAttackTiles(IBoardModel board, IPieceModel piece, List<int> attackRange)
    {
        List<int> validTiles = new List<int>();

        foreach (int tileID in attackRange)
        {
            if (board.GetTileAt(tileID).Type == TileType.Water)
            {
                continue;
            }

            IPieceModel targetPiece = board.GetPieceAt(tileID);

            if (targetPiece != null && targetPiece.PieceType == PieceType.Pegasus &&
               targetPiece.IsSameTeam(piece.Team) == false && targetPiece.IsAlive)
            {
                validTiles.Add(tileID);
            }
            else if (board.GetTileAt(tileID).Occupant == null)
            {
                validTiles.Add(tileID);
            }
        }

        return validTiles;
    }

    public override bool ExecuteSpecialAttack(IBoardModel board, IPieceModel piece, int targetTileID)
    {
        List<int> specialAttackRange = GetSpecialAttackRange(board, piece);
        IPieceModel selectedPiece = board.GetSelectedPiece();
        if (selectedPiece == null || selectedPiece.PieceType != PieceType.Bow)
        {
            Debug.Log("No Bow piece is selected for the special attack.");
            CancelSpecialAttack(board);
            return false;
        }

        if (specialAttackRange.Contains(targetTileID))
        {
            if (IsSpecialAttackReady)
            {
                IPieceModel targetPiece = board.GetPieceAt(targetTileID);
                if (targetPiece != null && targetPiece.PieceType == PieceType.Pegasus
                    && targetPiece.IsSameTeam(selectedPiece.Team) == false && targetPiece.IsAlive)
                {
                    board.GetTileAt(targetTileID).EnterPiece(null);
                    targetPiece.SetDead();
                    CancelSpecialAttack(board);
                    return true;
                }
                else
                {
                    CancelSpecialAttack(board);
                    return false;
                }
            }
            else
            {
                CancelSpecialAttack(board);
                return false;
            }
        }
        else
        {
            CancelSpecialAttack(board);
            return false;
        }
    }

    private void IncreasingRow(ref (int dr, int dc)[] attackRange)
    {
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (r == 0 && c == 0)
                {
                    continue;
                }
                var range = (r, c);
                attackRange = attackRange.Append(range).ToArray();
            }

            for (int c = 0; c > -3; c--)
            {
                if (r == 0 && c == 0)
                {
                    continue;
                }

                var range = (r, c);
                attackRange = attackRange.Append(range).ToArray();

            }
        }
    }

    private void DecreasingRow(ref (int dr, int dc)[] attackRange)
    {
        for (int r = 0; r > -3; r--)
        {
            for (int c = 0; c < 3; c++)
            {
                if (r == 0 && c == 0)
                {
                    continue;
                }

                var range = (r, c);
                attackRange = attackRange.Append(range).ToArray();

            }

            for (int c = 0; c > -3; c--)
            {
                if (r == 0 && c == 0)
                {
                    continue;
                }

                var range = (r, c);
                attackRange = attackRange.Append(range).ToArray();

            }
        }
    }

}
