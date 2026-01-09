using UnityEngine;
using System.Collections.Generic;

public class CreateTiles
{
    private List<ITileModel> _allTiles;
    private IBoardModel _boardModel;
    private IBoardModel_V2 _boardModel2;

    public CreateTiles(IBoardModel boardModel)
    {
        _allTiles = GetTiles();
        _boardModel = boardModel;
    }

    public CreateTiles(IBoardModel_V2 boardModel)
    {
        _allTiles = GetTiles();
        _boardModel2 = boardModel;
    }

    public List<ITileModel> GetTiles()
    {
        List<ITileModel> tilesList = new List<ITileModel>();

        for (int i = 0; i < 121; i++)
        {
            ITileModel tile = new TileModel(i, TileType.Regular);
            tilesList.Add(tile);
        }

        return tilesList;
    }
}
