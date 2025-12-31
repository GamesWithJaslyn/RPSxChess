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
    private static int numOfBluePieces;
    private static int numOfRedPieces;
    public Bow(int pos, int pieceType, int targetType, IBoardModel model) : base(pos, pieceType, targetType, model)
    {
        if(this.GetPieceType() > 0)
        {
            numOfBluePieces++;
        }
        else if(this.GetPieceType() < 0)
        {
            numOfRedPieces++;
        }

        if (targetType != -3 && targetType != 3)
        {
            throw new ArgumentException("Bow's can only attack Pegasi!", nameof(targetType));
        }
        else if (pieceType != 1 && pieceType != -1)
        {
            throw new ArgumentException("Bow's can only be of type 1 or -1", nameof(pieceType));
        }
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

    public override int PiecesLeft()
    {
        if(this.GetPieceType() > 0)
        {
            return numOfBluePieces;
        }
        else if(this.GetPieceType() < 0)
        {
            return numOfRedPieces;
        }
        else
        {
            return -1000;
        }
    }

    public override void SetDead()
    {
        if(this.GetPieceType() > 0)
        {
            numOfBluePieces--;
            Debug.Log("[ABasicPiece] - SetDead() called. Piece is now dead. Pieces Left : " + numOfBluePieces);
        }
        else if(this.GetPieceType() < 0)
        {
            numOfRedPieces--;
            Debug.Log("[ABasicPiece] - SetDead() called. Piece is now dead. Pieces Left : " + numOfRedPieces);
        }

        base.SetDead();
    }

}
