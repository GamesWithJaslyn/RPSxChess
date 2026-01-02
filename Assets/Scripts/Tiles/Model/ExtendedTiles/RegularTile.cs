using System;

/// <summary>
/// Represents a basic tile in a RPS x Chess game.
/// </summary>
public class RegularTile : EnterAndLeaveTile
{
    public RegularTile(int id, int type, IPieceModel_V2 piece) : base(id, type, piece)
    {
        if (type != 0)
        {
            throw new ArgumentException("Regular tile type has to be 0!");
        }
    }

}
