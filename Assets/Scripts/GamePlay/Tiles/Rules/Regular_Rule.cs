
using UnityEngine;

public class Regular_Rule : ITileRule
{
    public bool CanEnter(ITileModel tile, IPieceModel piece)
    {
        if (tile.Type == TileType.Regular)
        {
            if (tile.Occupant == null || tile.Occupant.IsAlive == false)
            {
                return true;
            }
            else if (tile.Occupant.IsAlive && tile.Occupant.Team != piece.Team)
            {
                if (piece is AAttackingPiece attacking)
                {
                    if (attacking.TargetType == tile.Occupant.PieceType)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}