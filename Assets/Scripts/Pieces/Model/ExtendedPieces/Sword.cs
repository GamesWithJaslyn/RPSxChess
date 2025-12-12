using System;
using System.Collections.Generic;

/// <summary>
/// Represents a Sword Piece in an RPS x Chess Game.
/// It's targets are a Bow pieces.
/// 
/// It moves consists of 8 tiles: 
/// - Up
/// - Upper Left
/// - Left
/// - Lower Left
/// - Down
/// - Lower Right
/// - Right
/// - Upper Right
/// </summary>
public class Sword : AAttackingPiece
{
    private static int numOfBluePieces;
    private static int numOfRedPieces;
    public Sword(int pos, int pieceType, int targetType, IBoardModel model) : base(pos, pieceType, targetType, model)
    {
        if(this.GetPieceType() > 0)
        {
            numOfBluePieces++;
        }
        else if(this.GetPieceType() < 0)
        {
            numOfRedPieces++;
        }


         if (targetType != -1 && targetType != 1)
        {
            UnityEngine.Debug.Log("Sword target type " + targetType);
            throw new ArgumentException("Sword's can only attack Bows!", nameof(targetType));
        }
        else if (pieceType != 2 && pieceType != -2)
        {
            UnityEngine.Debug.Log("Sword type" + pieceType);
            throw new ArgumentException("Sword's can only be of type 1 or -1", nameof(pieceType));
        }
     }

    public override List<int> GetMoveTiles()
     {
        List<int> possibleMoves = new List<int>();

        int up = _pos - 11;
        int upRight = _pos - 10;
        int upLeft = _pos - 12;
        int down = _pos + 11;
        int downRight = _pos + 10;
        int downLeft = _pos + 12;
        int left = _pos - 1;
        int right = _pos + 1;

        possibleMoves.Add(up);
        possibleMoves.Add(upRight);
        possibleMoves.Add(upLeft);
        possibleMoves.Add(down);
        possibleMoves.Add(downRight);
        possibleMoves.Add(downLeft);
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
        }
        else if(this.GetPieceType() < 0)
        {
            numOfRedPieces--;
        }
    }
}
