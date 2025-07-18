using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents a Pegasus Piece in an RPS x Chess Game.
/// It's targets are a Sword pieces.
/// 
/// It moves consists of 12 tiles: 
/// - Two Ups from the Pegasus
/// - Up
/// - Upper Left
/// - Left
/// - Two Lefts from the Pegasus
/// - Lower Left
/// - Down
/// - Two Downs from the Pegasus
/// - Lower Right
/// - Right
/// - Two Rights from the Pegasus
/// - Upper Right
/// </summary>
public class Pegasus : AAttackingPiece
{
    public Pegasus(int pos, int pieceType, int targetType) : base(pos, pieceType, targetType)
    {
        if (targetType != -2 && targetType != 2)
        {
            UnityEngine.Debug.Log("Pegasus target type " + targetType);
            throw new ArgumentException("Pegasus's can only attack Sword!", nameof(targetType));
        }
        else if (pieceType != 3 && pieceType != -3)
        {
            UnityEngine.Debug.Log("Pegasus type" + pieceType);
            throw new ArgumentException("Pegasus's can only be of type 3 or -3", nameof(pieceType));
        }
        
        Debug.Log("[Pegasus] constructed");

    }

    public override List<int> GetMoveTiles()
     {
        List<int> possibleMoves = new List<int>();

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
