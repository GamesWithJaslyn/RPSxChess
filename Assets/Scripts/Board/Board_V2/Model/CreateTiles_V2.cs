using UnityEngine;
using System.Collections.Generic;

public class CreateTiles_V2
{
    private List<ITileModel> _allTiles;
    private IBoardModel_V2 _boardModel;

    public CreateTiles_V2(IBoardModel_V2 boardModel)
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
