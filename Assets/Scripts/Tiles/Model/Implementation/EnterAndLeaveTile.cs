using System;
using UnityEngine;

public abstract class EnterAndLeaveTile : ATile, IEnterAndLeave
{
    protected IPieceModel _piece;
    protected bool _occupied;
    public bool _isValidMoveTileForAPiece;


    public EnterAndLeaveTile(int id, int type, IPieceModel piece) : base(id, type)
    {
        _id = id;
        _type = type;
        _occupied = piece != null;
        _piece = piece;
        _isValidMoveTileForAPiece = false;
    }

    public void IsValidTile_CanMoveHere(bool isValid)
    {
        _isValidMoveTileForAPiece = isValid;
    }

    public bool CanPieceMoveHere()
    {
        return _isValidMoveTileForAPiece;
    } 


    public IPieceModel GetPiece()
    {
        return _piece;
    }

    public void Enter(IPieceModel piece)
    {
        _piece = piece;
    }

    public bool CanEnter(IPieceModel piece)
    {
        if (_piece == null || piece is AAttackingPiece attack 
        && _piece.GetPieceType() == attack.GetTargetType()
        || !_piece.IsAlive())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Leave()
    {
        if (_piece != null)
        {
            _piece = null;
        }
        else
        {
            throw new InvalidOperationException("Tile is already empty!");
        }
    }
    
      public bool IsOccupied()
    {
        return _piece != null;
    }
}
