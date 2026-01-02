using System.Collections.Generic;

public class Bow_Moves : AMoves
{
    public override List<int> GetMoveTiles(int pos)
     {
        List<int> possibleMoves = new List<int>();

        int up = pos - 11;
        int down = pos + 11;
        int left = pos - 1;
        int right = pos + 1;

        possibleMoves.Add(up);
        possibleMoves.Add(down);
        possibleMoves.Add(left);
        possibleMoves.Add(right);

        return possibleMoves;
    }
}
