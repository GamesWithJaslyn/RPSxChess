using System;
using System.Collections.Generic;

public class CreateTiles
{
    private IBoardModel _board;
    public List<ITileModel> GetTiles()
    {
        List<ITileModel> tilesList = new List<ITileModel>();

        for (int i = 0; i < 121; i++)
        {
            TileModelImpl tile = new TileModelImpl(i, TileType.Regular);
            tilesList.Add(tile);
        }
        ChangeToWater(tilesList);

        return tilesList;
    }

    public void ChangeToWater(List<ITileModel> tilesList)
    {
        var skip = new HashSet<int> { 47, 51, 58, 62, 69, 73 };
        Random random = new Random();

        for (int i = 44; i < 77; i++)
        {
            if (skip.Contains(i))
            {
                continue;
            }

            int rand = random.Next(1, 3);
            if (rand == 1)
            {
                tilesList.Find(t => t.ID == i).ChangeType(TileType.Water);
            }
        }
    }


}
