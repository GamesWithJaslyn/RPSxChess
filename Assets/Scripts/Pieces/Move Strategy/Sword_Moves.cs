using System.Collections.Generic;

public class Sword_Moves : AMoves
{
      public override List<int> GetMoveTiles(int pos)
     {
        List<int> possibleMoves = new List<int>();

        int up = pos - 11;
        int upRight = pos - 10;
        int upLeft = pos - 12;
        int down = pos + 11;
        int downRight = pos + 10;
        int downLeft = pos + 12;
        int left = pos - 1;
        int right = pos + 1;

        possibleMoves.Add(up);
        possibleMoves.Add(upRight);
        possibleMoves.Add(upLeft);
        possibleMoves.Add(down);
        possibleMoves.Add(downRight);
        possibleMoves.Add(downLeft);
        possibleMoves.Add(left);
        possibleMoves.Add(right);

        return possibleMoves;
    }
}
