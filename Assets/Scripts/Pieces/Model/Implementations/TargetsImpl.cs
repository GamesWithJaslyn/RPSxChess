using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Represents a piece that can attack other pieces in an RPSxChess game.
/// This class implements the ITargets interface, defining the methods required for a piece to determine its
/// attack targets.
/// It is used to manage the interactions between pieces, allowing them
/// to identify which other pieces they can attack.
/// </summary>
public class TargetsImpl : ITargets
{
    private AAttackingPiece _piece;
    private int _targetType;
    private List<int> _targetTypeList = new List<int>();
    private List<AAttackingPiece> _targets = new List<AAttackingPiece>();

    public TargetsImpl(AAttackingPiece piece, int targetType, IBoardModel IBoardModel)
    {
        _piece = piece;
        _targetType = targetType;
        _targetTypeList.Add(_targetType);

        foreach (IPieceModel model in IBoardModel.GetAllPieces())
        {
            if (model is AAttackingPiece attacker && attacker.GetPieceType() == _piece.GetTargetType() && attacker.IsAlive())
            {
                _targets.Add(attacker);
            }

        }

        if (_targets.Count == 0)
        {
            Debug.LogWarning("No targets found for target type: " + _targetType);
        }
    }

    public List<int> GetTargetTypes()
    {
        List<int> targetTypes = new List<int>();
        for (int i = 0; i < _targets.Count; i++)
        {
            targetTypes.Add(_targets[i].GetPieceType());
        }
        return targetTypes;
    }

    public List<AAttackingPiece> GetTargets()
    {
        return _targets;
    }


    public List<int> GetTargetTiles()
    {
        List<int> targetTiles = new List<int>();
        foreach (AAttackingPiece target in _targets)
        {
            if (target == null || target.IsAlive() == false)
            {
                // If the target is null or not alive, skip it
                Debug.LogWarning("Target is null or not alive, skipping.");
                break;
            }
            else
            {
                targetTiles.Add(target.GetPos());
            }
        }
        Debug.Log("Target tiles for piece at position " + _piece.GetPos() + ": " + string.Join(", ", targetTiles));
        return targetTiles;
    }
}
