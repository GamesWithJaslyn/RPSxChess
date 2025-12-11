using System.Collections.Generic;
using UnityEngine;

public class PiecePresenter
{
    public AAttackingPiece _model { get; private set; }
    public PieceView _view { get; private set; }
    [SerializeField] private int _pos;
    [SerializeField] private int _type;
    [SerializeField] private int _targetType;
    private IBoardModel _boardModel;
    
    public PiecePresenter(PieceView view, AAttackingPiece model, IBoardModel boardModel)
    {
        _model = model;
        _view = view;
        _pos = model.GetPos();
        _type = model.GetPieceType();
        _targetType = model.GetTargetType();
        _boardModel = boardModel;
    }

    /// <summary>
    /// Called when the piece is clicked on.
    /// </summary>
    /// <param name="mouseClick"></param>
    public void MoveTo(int tilePos)
    {
        Debug.Log("[PiecePresenter] - Clicked on piece at " + tilePos);
        try
        {
            _boardModel.MovePiece(tilePos);
            Debug.Log("[PiecePresenter] - Moved piece to " + tilePos);
        }
        catch
        {
            _boardModel.SelectPiece(tilePos).SetSelected();
            Debug.Log("[PiecePresenter] - Selected piece at " + tilePos);

        }
    }
}
