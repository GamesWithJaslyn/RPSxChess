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
    public event Action<int> OnMoved;
    public event Action OnDeath;
    public event Action OnNoPiecesLeft;
    public event Action<int> OnChangeInto;
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
        Debug.Log("[ABasicPiece] - SetDead() called. Piece is now dead. Pieces Left : " + PiecesLeft());
    }

    public bool IsSelected()
    {
        return _isSelected;
    }

    public void SetSelected() {
        if(_isAlive)
        {
            foreach (IPieceModel piece in _boardModel.GetAllPieces())
            {
                piece.TurnSelectedFalse();
            }

            _isSelected = true;
        }
        
    }

    public void TurnSelectedFalse() {
        _isSelected = false;
    }

    public void SetPos(int tile)
    {
        _pos = tile;
        OnMoved?.Invoke(tile);
       TurnSelectedFalse();

       Debug.Log("[ABasicPiece] - Moved to position: " + tile);
    }

    public bool IsSameTeam(string team) 
    {
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
            return false;
        }
    }

    public bool IsSameTeam(int type) {
        if (type > 0) {
            if (_pieceType > 0) {
                return true;
            } else {
                return false;
            }
        } else if (type < 0) {
            if (_pieceType < 0) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
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
                Debug.Log("[ABasicPiece] - Set dead for changing into new piece. Pieces Left Before Change : " + PiecesLeft());
                OnChangeInto?.Invoke(changingInto);
                SetDead();
                Debug.Log("[ABasicPiece] - Changed into type: " + changingInto);

                if (PiecesLeft() <= 0) 
                {
                    //GameState.OnGameWon?.Invoke(_pieceType > 0 ? 1 : -1);
                }

            }
            else 
            {
                Debug.Log("[ABasicPiece] - Change has to be from the same team!");
                return;
            }
        } 
        else 
        {
            Debug.Log("[ABasicPiece] - Cannot change piece type at this time.");
            return;
        }
    }

    public bool CanChange()
    {
        bool location;

        if (_pieceType < 0 && _pos >= 0 && _pos < 11)
        {
            location = true;
            Debug.Log("[ABasicPiece] - red piece, " + _pieceType + " can change!");
        } 
        else if (_pieceType > 0 && _pos >= 109 && _pos < 121)
        {
            location = true;
            Debug.Log("[ABasicPiece] - blue piece, " + _pieceType + " can change!");
        } 
        else 
        {
            location = false;
            Debug.Log("[ABasicPiece] - Location is not valid for changing: " + location);
        }

        

        return _firstTimeChanging && location;
    }

    public void PromotionMade()
    {
        _firstTimeChanging = false;
    }

    public bool MoveTo(int newPos, IPieceModel piece)
    {
        if(this._isAlive && GetMoveTiles().Contains(newPos))
        {
            if(piece != null && piece.IsAlive())
            {
                AAttackingPiece target = piece as AAttackingPiece;
                AAttackingPiece thisAttacker = this as AAttackingPiece;

                if(target.GetPieceType() == thisAttacker.GetTargetType()) {
                   target.SetDead();
                   target.OnDeath?.Invoke();
                   if(target.PiecesLeft() <= 0) 
                   {
                       //GameState.OnGameWon?.Invoke(thisAttacker.GetPieceType() > 0 ? 1 : -1);
                   }
                   SetPos(newPos);
                   return true;
                }
                else 
                {
                    Debug.Log("[ABasicPiece] - MoveTo() -> Cannot attack this piece.");
                    return false;
                }
            }
            else
            {
                SetPos(newPos);
                return true;
            }
            
        }
        else
        {
            Debug.Log("[ABasicPiece] - MoveTo() -> Invalid Move");
            return false;
        }
    }

    public IBoardModel GetBoardModel()
    {
        return _boardModel;
    }

    public IPieceModel CopyPiece()
    {
        switch(_pieceType)
        {
            case 1:
                return new Bow(_pos, _pieceType, -3, _boardModel);
            case -1:
                return new Bow(_pos, _pieceType, 3, _boardModel);
            case 2:
                return new Sword(_pos, _pieceType, -1, _boardModel);
            case -2:
                return new Sword(_pos, _pieceType, 1, _boardModel);
            case 3:
                return new Pegasus(_pos, _pieceType, -2, _boardModel);
            case -3:
                return new Pegasus(_pos, _pieceType, 2, _boardModel);
            default:
                return new Bow(_pos, _pieceType, -3, _boardModel);
        }
    }
}
