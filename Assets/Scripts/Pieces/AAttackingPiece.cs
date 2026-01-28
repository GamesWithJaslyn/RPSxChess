using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents an IPiece that can Attack other pieces.
/// </summary>
public class AAttackingPiece : PieceModelImpl, IPieceModel
{
    public PieceType TargetType { get; private set; }
    private bool _hasShownArrow = false;
    public AAttackingPiece(int pos, PieceType pieceType, Team team,
     PieceType targetType, IBoardModel model) : base(pos, pieceType, team, model)
    {
        TargetType = targetType;
    }

    public void ChangeTarget(PieceType type)
    {
        switch (type)
        {
            case PieceType.Bow:
                TargetType = PieceType.Pegasus;
                break;
            case PieceType.Sword:
                TargetType = PieceType.Bow;
                break;
            case PieceType.Pegasus:
                TargetType = PieceType.Sword;
                break;
        }
    }

    public void ShowArrow()
    {
        if (PieceType == PieceType.Bow)
        {
            Debug.Log("[AAttackingPiece] - Showing arrow for Bow piece.");
            _hasShownArrow = true;
        }
    }

    public bool HasShownArrow()
    {
        return _hasShownArrow;
    }

    public void ResetArrow()
    {
        Debug.Log("[AAttackingPiece] - Hidding arrow for Bow piece.");
        _hasShownArrow = false;
    }
}
