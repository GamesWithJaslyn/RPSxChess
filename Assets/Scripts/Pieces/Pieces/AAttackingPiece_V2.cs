using System;
using System.Collections.Generic;


/// <summary>
/// Represents an IPiece that can Attack other pieces.
/// </summary>
public class AAttackingPiece_V2 : PieceModelImpl, IPieceModel_V2
{
    public PieceType TargetType { get; private set; }
    public AAttackingPiece_V2(int pos, PieceType pieceType, Team team,
     PieceType targetType, IBoardModel_V2 model) : base(pos, pieceType, team, model)
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
}
