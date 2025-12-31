using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a Piece in the RPSxChess game.
/// This class is responsible for the visual representation of a piece in the game.
/// It handles the movement of the piece in the game world.
/// </summary>
public class PieceView : MonoBehaviour
{
    public IPieceModel _model { get; private set; }
    [SerializeField] private int _pos;
    [SerializeField] private int _type;
    [SerializeField] private int _targetType;
    [SerializeField] private List<Sprite> _images;

      public void Init(IPieceModel model, List<Sprite> images)
    {
        _model = model;
        _images = images;
        _pos = model.GetPos();
        _type = model.GetPieceType();
        _model.OnMoved += UpdatePosition;
        _model.OnDeath += SetDead;
        _model.OnChangeInto += ChangeType;

        if(model is AAttackingPiece attack)
        {
            _targetType = attack.GetTargetType();
        }

    }

    public void UpdatePosition(int pos)
    {
        Vector3 position = new Vector3(pos % 11, -1 * (pos / 11), -1);
        _pos = _model.GetPos();
        transform.position = position;
    }

    public void ChangeType(int changeInto)
    {
        Debug.Log("[Piece View] - Changing Type to: " + changeInto);
        ChangeSprite(changeInto);

        _model.GetBoardModel().RemovePiece(_model); //removing old piece from board
        _model = ChangePieceClass(changeInto); //changing the piece class
        _model.GetBoardModel().AddPiece(_model); //adding new piece to board
        UpdatingModel();

        _model.GetBoardModel().GetAllEnterableTiles().Find(t => t.GetID() == _pos).Enter(_model); //updating the tile's piece reference
    }

    private void UpdatingModel()
    {
        _model.PromotionMade();
        _type = _model.GetPieceType();
        _targetType = (_model as AAttackingPiece).GetTargetType();

        _model.OnMoved += UpdatePosition;
        _model.OnDeath += SetDead;
        _model.OnChangeInto += ChangeType;
    }

    private void ChangeSprite(int changeInto)
    {
        if(_type > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _images[changeInto - 1];
        }
        else if (_type < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _images[(-1 * changeInto) + 2];
        }
    }

    private IPieceModel ChangePieceClass(int changeInto)
    {
        RemoveEvents();

        switch(changeInto)
        {
            case 1:
                Debug.Log("[PieceView] - Changed into Bow");
                return new Bow(_pos, changeInto, -3, _model.GetBoardModel());
            case -1:
                Debug.Log("[PieceView] - Changed into Bow");
                return new Bow(_pos, changeInto, 3, _model.GetBoardModel());
            case 2:
                Debug.Log("[PieceView] - Changed into Sword");
                return new Sword(_pos, changeInto, -1, _model.GetBoardModel());
            case -2:
                Debug.Log("[PieceView] - Changed into Sword");
                return new Sword(_pos, changeInto, 1, _model.GetBoardModel());
            case 3:
                Debug.Log("[PieceView] - Changed into Pegasus");
                return new Pegasus(_pos, changeInto, -2, _model.GetBoardModel());
            case -3:
                Debug.Log("[PieceView] - Changed into Pegasus");
                return new Pegasus(_pos, changeInto, 2, _model.GetBoardModel());
            default:
                Debug.Log("[PieceView] - No valid change type found, staying the same.");
                return _model;
        }
    }

    public void SetDead()
    {
        gameObject.SetActive(false);
    }

    public IPieceModel GetModel()
    {
        return _model;
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void RemoveEvents()
    {
        _model.OnMoved -= UpdatePosition;
        _model.OnDeath -= SetDead;
        _model.OnChangeInto -= ChangeType;
    }
}
