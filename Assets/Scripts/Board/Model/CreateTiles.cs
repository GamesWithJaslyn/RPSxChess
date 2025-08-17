using UnityEngine;
using System.Collections.Generic;

public class CreateTiles : MonoBehaviour
{
    private List<ITileModel> _allTiles;

    public CreateTiles()
    {
        _allTiles = GetTiles();
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
