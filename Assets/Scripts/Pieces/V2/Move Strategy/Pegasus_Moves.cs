using System.Collections.Generic;

public class Pegasus_Moves : AMoves
{
    public override List<int> GetMoveTiles(int pos)
     {
        List<int> possibleMoves = new List<int>();

        int upUp = pos - 22;
        int up = pos - 11;
        int upRight = pos - 10;
        int upLeft = pos - 12;
        int down = pos + 11;
        int downDown = pos + 22;
        int downRight = pos + 10;
        int downLeft = pos + 12;
        int left = pos - 1;
        int leftLeft = pos - 2;
        int right = pos + 1;
        int rightRight = pos + 2;

        possibleMoves.Add(upUp);
        possibleMoves.Add(up);
        possibleMoves.Add(upRight);
        possibleMoves.Add(upLeft);
        possibleMoves.Add(down);
        possibleMoves.Add(downDown);
        possibleMoves.Add(downRight);
        possibleMoves.Add(downLeft);
        possibleMoves.Add(left);
        possibleMoves.Add(leftLeft);
        possibleMoves.Add(right);
        possibleMoves.Add(rightRight);

        return possibleMoves;
    }
}
