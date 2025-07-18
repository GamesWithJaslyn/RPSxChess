using UnityEngine;
using System.Collections.Generic;

public class MoveToImpl : MonoBehaviour
{
    private IPieceModel _pieceModel;
    private int _pos;
    private int _targetTile;

    public MoveToImpl(IPieceModel pieceModel, int pos, int targetTile)
    {
        _pieceModel = pieceModel;
        _pos = pos;
        _targetTile = targetTile;
    }

    public void SetPos() {
        if(this.GetValidMoveTiles().Contains(_targetTile)) {
            _pieceModel.SetPos(_targetTile);
        }
       
    }

    public List<int> GetMoveTiles() {
        List<int> list = new List<int>();
        return list;
    }

    public List<int> GetValidMoveTiles() {
        List<int> list = new List<int>();
        return list;
    }
}
