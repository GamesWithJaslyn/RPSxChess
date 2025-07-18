using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Represents a Bow Piece in an RPS x Chess Game.
/// It's targets are Pegasus pieces.
/// 
/// It moves consists of 8 tiles: 
/// - Up
/// - Left
/// - Down
/// - Right
/// A Bow has the most limited movement, but has a range around it to shoot arrows.
/// Its range consists of 32 tiles. Tiles all around the bow, and tilesall around again.
/// </summary>
public class Bow : AAttackingPiece
{
    public Bow(int pos, int pieceType, int targetType) : base(pos, pieceType, targetType)
    {
        if (targetType != -3 && targetType != 3)
        {
            throw new ArgumentException("Bow's can only attack Pegasi!", nameof(targetType));
        }
        else if (pieceType != 1 && pieceType != -1)
        {
            throw new ArgumentException("Bow's can only be of type 1 or -1", nameof(pieceType));
        }

        Debug.Log("[Bow] constructed");
    }

    public override List<int> GetMoveTiles()
     {
        List<int> possibleMoves = new List<int>();
        Debug.Log("Piece Pos: " + _pos);

        int up = _pos - 11;
        int down = _pos + 11;
        int left = _pos - 1;
        int right = _pos + 1;

        possibleMoves.Add(up);
        possibleMoves.Add(down);
        possibleMoves.Add(left);
        possibleMoves.Add(right);

        return possibleMoves;
    }
}
