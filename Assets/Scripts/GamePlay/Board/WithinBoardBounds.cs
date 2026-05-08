using System.Collections.Generic;
using UnityEngine;

public class WithinBoardBounds : MonoBehaviour
{
    public static List<int> GetTiles(int index, (int dRow, int dCol)[] pieceMoves)
    {
        List<int> _possibleTiles = new();
        int row = index / 11;
        int col = index % 11;

        foreach (var (dRow, dCol) in pieceMoves)
        {
            int r = row + dRow;
            int c = col + dCol;

            if (r < 0 || r > 10 || c < 0 || c > 10)
                continue;

            int target = r * 11 + c;
            _possibleTiles.Add(target);
        }

        return _possibleTiles;
    }
}
