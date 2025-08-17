using System;
using UnityEngine;

public abstract class EnterAndLeaveTile : ATile, IEnterAndLeave
{
    protected IPieceModel _piece;
    protected bool _occupied;


    public EnterAndLeaveTile(int id, int type, IPieceModel piece) : base(id, type)
    {
        _id = id;
        _type = type;
        _occupied = piece != null;
        _piece = piece;
    }


    public IPieceModel GetPiece()
    {
        return _piece;
    }

    public void Enter(IPieceModel piece)
    {
        if (_piece == null)
        {
            _piece = piece;
        }
        else
        {
            throw new ArgumentException("Tile is occupied by: " + nameof(_piece));
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
