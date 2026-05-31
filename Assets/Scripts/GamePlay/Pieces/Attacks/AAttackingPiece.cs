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
    private bool _hasShownArrow = false;
    public event Func<IPieceModel, Task> OnArrowShot;
    public event Action OnShowArrow;
    public event Action OnHideArrow;
    public event Action<IPieceModel> OnArrowHitTarget;

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
        else
        {
            HideArrow();
            return;
        }
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
        Debug.Log("[AAttackingPiece] - Hidding arrow for Bow piece.");
        _hasShownArrow = false;
    }
}
