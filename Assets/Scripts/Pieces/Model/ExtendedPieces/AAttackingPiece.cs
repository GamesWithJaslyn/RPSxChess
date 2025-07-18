using System;
using System.Collections.Generic;


/// <summary>
/// Represents an IPiece that can Attack other pieces.
/// </summary>
public abstract class AAttackingPiece : ABasicPiece
{
    protected ITargets _targets;
    protected int _targetType;

    public AAttackingPiece(int pos, int pieceType, int targetType) : base(pos, pieceType)
    {
        if (pieceType > 0 && targetType > 0 || pieceType < 0 && targetType < 0)
        {
            throw new ArgumentException("Piece and target cannot be on the same team!",
             nameof(targetType));
        }
        else if (pieceType == 0)
        {
            throw new ArgumentException("An AttackingPiece cannot be 0!",
             nameof(pieceType));
        }

        _targetType = targetType;
    }

    public override abstract List<int> GetMoveTiles();

    public int GetTargetType()
    {
        return _targetType;
    }
}
