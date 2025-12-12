using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class ABasicPiece : IPieceModel
{
    protected int _pieceType;
    protected int _pos;
    protected List<int> _validMoveTiles;
    protected bool _isSelected;
    protected bool _isAlive;
    protected bool _firstTimeChanging;
    protected int _changingInto;
    protected IBoardModel _boardModel;

    public static List<IPieceModel> _allPieces = new List<IPieceModel>();

    public ABasicPiece(int pos, int pieceType, IBoardModel model)
    {
        _pos = pos;
        _pieceType = pieceType;
        _validMoveTiles = new List<int>();
        _isSelected = false;
        _isAlive = true;
        _firstTimeChanging = true;
        _boardModel = model;
    }

    public abstract List<int> GetMoveTiles();
    public abstract int PiecesLeft();


    public int GetPieceType()
    {
        return _pieceType;
    }

    public void SetPieceType(int type)
    {
        _pieceType = type;
    }

    public int GetPos()
    {
        return _pos;
    }

    public bool IsAlive()
    {
        return _isAlive;
    }
    
    public virtual void SetDead()
    {   
        _isAlive = false;
    }

    public bool IsSelected()
    {
        return _isSelected;
    }

    public void SetSelected() {
        if(_isAlive)
        {
            _isSelected = true;

            foreach (IPieceModel piece in _boardModel.GetAllPieces())
            {
                if (piece != this)
                {
                    piece.TurnSelectedFalse();
                }
            }
        }
        
    }

    public void TurnSelectedFalse() {
        _isSelected = false;
    }

    public void SetPos(int tile)
    {
        _pos = tile;
       TurnSelectedFalse();
    }

    public bool IsSameTeam(string team) {
        if (team == "Blue") {
            if (_pieceType > 0) {
                return true;
            } else {
                return false;
            }
        } else if (team == "Red") {
            if (_pieceType < 0) {
                return true;
            } else {
                return false;
            }
        } else {
            throw new ArgumentException("Invalid Team", nameof(team));
        }
    }

    public void ChangeInto(int changingInto) 
    {
        if (CanChange()) 
        {
            if((_pieceType > 0 && changingInto > 0) || (_pieceType < 0 && changingInto < 0))
            {
                SetPieceType(changingInto);
                _firstTimeChanging = false;
            }
            else 
            {
                throw new System.ArgumentException("Change has to be from the same team!");
            }
        } 
        else 
        {
            throw new System.ArgumentException("Cannot change piece type at this time.");
        }
    }

    public bool CanChange()
    {
        bool location;

        if (_pieceType < 0 && _pos >= 0 && _pos < 11)
        {
            location = true;
        } 
        else if (_pieceType > 0 && _pos >= 109 && _pos < 121)
        {
            location = true;
        } 
        else 
        {
            location = false;
        }
        return _firstTimeChanging && location;
    }


}
