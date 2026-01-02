using System;
using System.Collections.Generic;


/// <summary>
/// Represents an IPiece that can Attack other pieces.
/// </summary>
public abstract class AAttackingPiece_V2 : ABasicPiece_V2, IPieceModel_V2
{
    protected PieceType _targetType;

    public AAttackingPiece_V2(int pos, PieceType pieceType, Team team,
     PieceType targetType, IBoardModel_V2 model) : base(pos, pieceType, team, model)
    {
        _targetType = targetType;
    }

    public override abstract List<int> GetMoveTiles();

    public PieceType GetTargetType()
    {
        return _targetType;
    }
}
