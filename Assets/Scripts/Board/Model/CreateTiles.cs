using UnityEngine;
using System.Collections.Generic;

public class CreateTiles
{
    private List<ITileModel> _allTiles;
    private IBoardModel _boardModel;

    public CreateTiles(IBoardModel boardModel)
    {
        _allTiles = GetTiles();
        _boardModel = boardModel;
    }
    
    public List<ITileModel> GetTiles() 
    {
        List<ITileModel> tilesList = new List<ITileModel>();

        for (int i = 0; i < 121; i++)
        {
            ITileModel tile = new RegularTile(i, 0, null);
            tilesList.Add(tile);
        }

        return tilesList;
    }
}
