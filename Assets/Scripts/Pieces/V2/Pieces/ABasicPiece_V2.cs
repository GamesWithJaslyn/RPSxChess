using System.Collections.Generic;
using System;
using UnityEngine;

public enum PieceType { None, Bow, Sword, Pegasus }
public enum Team { Blue, Red }

public abstract class ABasicPiece_V2 : IPieceModel_V2
{
    protected int _pos;
    protected Team _team;
    protected PieceType _pieceType;
    protected IMoveStrategy _moveStrategy;
    protected IBoardModel_V2 _boardModel;
    protected bool _isSelected;
    protected bool _isAlive;
    protected bool _isPromoted;

    public event Action OnDeath;
    public event Action<int> OnMoved;
    public event Action<PieceType> OnChangeInto;

    public ABasicPiece_V2(int pos, PieceType pieceType, Team team, IBoardModel_V2 model)
    {
        _pos = pos;
        _team = team;
        _boardModel = model;
        _pieceType = pieceType;
        
        _isAlive = true;
        _isSelected = false;
        _isPromoted = false;
    }

    public abstract List<int> GetMoveTiles();
    public abstract int PiecesLeft();


    public IMoveStrategy GetValidMoveTiles() { return _moveStrategy; }
    public IBoardModel_V2 GetBoardModel() { return _boardModel; }
    public PieceType GetPieceType() { return _pieceType; }
    public Team GetTeam() { return _team; }
    public int GetPos() { return _pos; }


    public bool IsSameTeam(Team team) { return _team == team; }
    public bool IsSelected() { return _isSelected; }
    public bool IsAlive() { return _isAlive; }
    public bool IsPromoted() { return _isPromoted; }
    public bool IsItPossibleToChange()
    {
        bool location;

        if(_team.Equals(Team.Blue) && _pos >= 109 && _pos < 121) { location = true; }
        else if (_team.Equals(Team.Red) && _pos >= 0 && _pos < 11) { location = true; }
        else { location = false; }

        return _isPromoted && location;
    }


    public void SetPieceType(PieceType type) { _pieceType = type; }
    public void SetSelected(bool selection) { _isSelected = selection; }
    public void SetPromotion(bool promotion) { _isPromoted = promotion; }
    public virtual void SetDead() { _isAlive = false;
        Debug.Log("[ABasicPiece] - SetDead() called. Piece is now dead. Pieces Left : " + PiecesLeft());
    }
    public void SetPos(int tile)
    {
        _pos = tile;
        OnMoved?.Invoke(tile);
        SetSelected(false);
    }

    public void ChangeInto(PieceType changingInto) 
    {
        if (IsItPossibleToChange()) 
        {
            SetPieceType(changingInto);
            _isPromoted = true;
            OnChangeInto?.Invoke(changingInto);

            if (PiecesLeft() <= 0) 
            {
                GameState.OnGameWon?.Invoke(_pieceType.Equals(Team.Blue) ? Team.Blue : Team.Red);
            }

        }
        else { return; }
    }

    public bool MoveTo(int newPos, IPieceModel_V2 piece)
    {
        if(this._isAlive && GetMoveTiles().Contains(newPos))
        {
            if(piece != null && piece.IsAlive())
            {
                if(piece.GetPieceType() == (this as AAttackingPiece_V2).GetTargetType()) 
                {
                   piece.SetDead();
                   (piece as AAttackingPiece_V2).OnDeath?.Invoke();
                   if(piece.PiecesLeft() <= 0)  { GameState.OnGameWon?.Invoke(_pieceType
                                                  .Equals(Team.Blue) ? Team.Blue : Team.Red); }
                   SetPos(newPos);
                   return true;
                }
                else { return false; }
            }
            else 
            {
                SetPos(newPos);
                return true;
            }
            
        }
        else { return false; }
    }
    public IPieceModel_V2 CopyPiece()
    {
        return null;
        // switch(_pieceType)
        // {
        //     case 1:
        //         return new Bow(_pos, _pieceType, -3, _boardModel);
        //     case -1:
        //         return new Bow(_pos, _pieceType, 3, _boardModel);
        //     case 2:
        //         return new Sword(_pos, _pieceType, -1, _boardModel);
        //     case -2:
        //         return new Sword(_pos, _pieceType, 1, _boardModel);
        //     case 3:
        //         return new Pegasus(_pos, _pieceType, -2, _boardModel);
        //     case -3:
        //         return new Pegasus(_pos, _pieceType, 2, _boardModel);
        //     default:
        //         return new Bow(_pos, _pieceType, -3, _boardModel);
        // }
    }
}
