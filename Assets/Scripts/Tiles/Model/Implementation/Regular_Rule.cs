
using UnityEngine;

public class Regular_Rule : ITileRule
{
    public bool CanEnter(ITileModel tile, IPieceModel_V2 piece)
    {
        if (tile.Type == TileType.Regular)
        {
            if (tile.Occupant == null || tile.Occupant.IsAlive == false)
            {
                return true;
            }
            else if (tile.Occupant.IsAlive && tile.Occupant.Team != piece.Team)
            {
                if (piece is AAttackingPiece_V2 attacking)
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