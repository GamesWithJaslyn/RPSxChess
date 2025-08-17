using System;

public abstract class ATile : ITileModel
{
    protected int _id;
    protected int _type;

    public ATile(int id, int type)
    {
        if (id < 0)
        {
            throw new ArgumentException("Tile ID cannot be negative!");
        }

        _id = id;
        _type = type;
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
}
