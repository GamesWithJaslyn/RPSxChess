using System;

/// <summary>
/// Represents a basic tile in a RPS x Chess game.
/// </summary>
public class RegularTile : EnterAndLeaveTile
{
    public RegularTile(int id, int type, IPieceModel piece) : base(id, type, piece)
    {
        if (id < 0)
        {
            throw new ArgumentException("Tile ID cannot be negative!");
        }
        else if (type != 0)
        {
            throw new ArgumentException("Regular tile type has to be 0!");
        }
    }

}
