using System;

public abstract class ATile : ITileModel
{
    protected int _id;
    protected bool _occupied;
    protected IPieceModel _piece;
    protected int _type;

    public ATile(int id, int type, IPieceModel piece)
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

    public int GetID()
    {
        return _id;
    }


    public int GetTileType()
    {
        return _type;
    }

    public ITileModel GetTile()
    {
        return this;
    }

    public bool IsOccupied()
    {
        return _piece != null;
    }
}
