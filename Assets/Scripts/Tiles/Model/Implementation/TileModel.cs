using System;
using System.Dynamic;
using UnityEngine.Tilemaps;
public enum TileType { None, Regular, Teleport, Water }

public class TileModel : ITileModel
{
    public int ID { get; }
    public TileType Type { get; private set; }
    public ITileRule Rule { get; private set; }
    public IPieceModel_V2 Occupant { get; set; }
    public event Action<bool> OnTileValid;
    public TileModel(int id, TileType type)
    {
        ID = id;
        Type = type;
        Occupant = null;
        SetRule();
    }

    public ITileModel GetTile() { return this; }
    public void ChangeType(TileType type) { Type = type; SetRule(); }

    private void SetRule()
    {
        switch (Type)
        {
            case TileType.Regular:
                Rule = new Regular_Rule();
                break;
            case TileType.Water:
                Rule = new Water_Rule();
                break;
            case TileType.Teleport:
                Rule = new Teleport_Rule();
                break;
        }
    }

    public void SetValid(bool isvalid)
    {
        OnTileValid?.Invoke(isvalid);
    }


    /// public ITileRule GetRule() { return _tileRule; }
    /// public int GetID() { return _id; }
    /// public TileType GetTileType() { return _type; }
}
