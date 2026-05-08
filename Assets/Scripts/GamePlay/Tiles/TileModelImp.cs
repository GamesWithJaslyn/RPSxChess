using System;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum TileType { None, Regular, Teleport, Water }

public class TileModelImpl : ITileModel
{
    public int ID { get; }
    public TileType Type { get; set; }
    public ITileRule Rule { get; private set; }
    public IPieceModel Occupant { get; private set; }
    public event Action<bool> OnTileValid;
    public event Action<bool> OnTileSpecialAttackValid;
    public event Action OnChangeType;
    public event Action<IPieceModel> OnPieceEntered;
    public TileModelImpl(int id, TileType type)
    {
        ID = id;
        Type = type;
        Occupant = null;
        SetRule();
    }

    public ITileModel GetTile() { return this; }

    public void EnterPiece(IPieceModel piece)
    {
        if (Type != TileType.Water)
        {
            Occupant = piece;
            OnPieceEntered?.Invoke(piece);
        }

    }
    public void ChangeType(TileType type)
    {
        Type = type;
        SetRule();
        OnChangeType?.Invoke();
    }

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

    public void SetSpecialAttackValid(bool isvalid)
    {
        OnTileSpecialAttackValid?.Invoke(isvalid);
    }

    /// public ITileRule GetRule() { return _tileRule; }
    /// public int GetID() { return _id; }
    /// public TileType GetTileType() { return _type; }
}
