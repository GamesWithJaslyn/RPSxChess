using System.Collections.Generic;

public class Pegasus_Moves : AMoves
{
    public override List<int> GetMoveTiles(int pos)
    {
        (int dr, int dc)[] swordMoves =
        {
            ( 0,  1), // right
            ( 0, -1), // left
            ( 1,  0), // down
            ( 1,  1), // down-right
            ( 1, -1), // down-left
            (-1,  1), // up-right
            (-1, -1), // up-left
            (-1,  0), // up
            (-2,  0), // up-up
            ( 2,  0), // down-down
            ( 0, -2), // left-left
            ( 0,  2)  // right-right
        };

        return WithinBoardBounds(pos, swordMoves);
    }
}
