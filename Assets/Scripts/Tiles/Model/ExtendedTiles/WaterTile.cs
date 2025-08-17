using System;
using UnityEngine;

public class WaterTile : ATile
{
    public WaterTile(int id, int type, IPieceModel piece) : base(id, type)
    {
        if (type != 1)
        {
            throw new ArgumentException("Water tile type has to be 1!");
        }
    }
}
