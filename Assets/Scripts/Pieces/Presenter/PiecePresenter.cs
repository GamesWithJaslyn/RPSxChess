using System.Collections.Generic;
using UnityEngine;

public class PiecePresenter : MonoBehaviour
{
    public AAttackingPiece _model { get; private set; }
    [SerializeField] private int _pos;
    [SerializeField] private int _type;
    [SerializeField] private int _targetType;

    // Assign a model instance to this view
    public void Init(AAttackingPiece model)
    {
        _model = model;
        _pos = model.GetPos();
        _type = model.GetPieceType();
        _targetType = model.GetTargetType();
    }
}
