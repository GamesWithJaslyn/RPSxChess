using System;
using UnityEngine;

public class TeleportTile : EnterAndLeaveTile
{
    public TeleportTile(int id, int type, IPieceModel piece) : base(id, type, piece)
    {
        if (type != 2)
        {
            throw new ArgumentException("Teleport tile type has to be 2!");
        }
    }
}
