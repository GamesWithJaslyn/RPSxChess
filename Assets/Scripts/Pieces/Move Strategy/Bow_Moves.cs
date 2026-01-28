using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Bow_Moves : AMoves
{
    public override List<int> GetMoveTiles(int pos)
    {
        (int dr, int dc)[] bowMoves =
        {
            ( 0,  1), // right
            ( 0, -1), // left
            ( 1,  0), // down
            (-1,  0)  // up
        };

        return WithinBoardBounds(pos, bowMoves);
    }

}
