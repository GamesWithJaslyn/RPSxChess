using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// Represents an IPiece that can Attack other pieces.
/// </summary>
public class AAttackingPiece : PieceModelImpl, IPieceModel
{
    public PieceType TargetType { get; private set; }
    public PieceType AttackerType { get; set; }
    private bool _hasShownArrow = false;
    public bool IsKing { get; private set; }
    public event Func<IPieceModel, Task> OnArrowShot;
    public event Action OnShowArrow;
    public event Action OnHideArrow;
    public event Action<IPieceModel> OnArrowHitTarget;
    // public event Action OnBecomeKing;

    public AAttackingPiece(int pos, PieceType pieceType, Team team,
     PieceType targetType, PieceType Predator, IBoardModel model) : base(pos, pieceType, team, model)
    {
        TargetType = targetType;
        AttackerType = Predator;
        IsKing = false;
    }

    public void ChangeTarget(PieceType type)
    {
        if (!IsKing)
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

    public void ShowArrow()
    {
        if (PieceType == PieceType.Bow)
        {
            _hasShownArrow = true;
        }
        else
        {
            HideArrow();
            return;
        }
    }

    public void BecomeKing()
    {
        IsKing = true;
        Debug.Log("[AAttackingPiece] - became king");
    }

    public void HideArrow()
    {

        _hasShownArrow = false;
    }

    public bool HasShownArrow()
    {
        return _hasShownArrow;
    }

    public void CallShowArrow()
    {
        ShowArrow();
        OnShowArrow?.Invoke();
    }

    public void CallHideArrow()
    {
        HideArrow();
        OnHideArrow?.Invoke();
    }

    public async Task CallArrowShot(IPieceModel target)
    {
        if (OnArrowShot != null)
        {
            await OnArrowShot.Invoke(target);
        }
    }

    public void ReachedTarget(bool didIthit)
    {
        Debug.Log($"[AAttackingPiece] - Arrow reached target: {didIthit}");
    }

    public void CallArrowHitTarget(IPieceModel target)
    {
        OnArrowHitTarget?.Invoke(target);
    }

    public void ResetArrow()
    {
        _hasShownArrow = false;
    }
}
