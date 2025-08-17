using System.Collections.Generic;
using UnityEngine;

public class BoardModelImpl : IBoardModel
{
    public static List<AAttackingPiece> _allPieces;

    private List<ITileModel> _allTiles;
    private bool _bluesTurn;
    private AAttackingPiece _selectedPiece;
    private CreatePieces _createPieces = new CreatePieces();

    public BoardModelImpl()
    {
        _allPieces = new List<AAttackingPiece>();
        _allTiles = new List<ITileModel>();
        _bluesTurn = true;
        _selectedPiece = null;

        _allTiles = InstantiateTiles();
        _allPieces = _createPieces.GetPieces();
        Debug.Log("Pieces count: _allPieces.Count");
    }

     private List<ITileModel> InstantiateTiles() 
    {
        List<ITileModel> tilesList = new List<ITileModel>();

        for (int i = 0; i < 121; i++)
        {
            ITileModel tile = new RegularTile(i, 0, null);
            tilesList.Add(tile);
        }

        return tilesList;
    }

    public AAttackingPiece SelectPiece(int pos)
    {
        foreach (AAttackingPiece piece in _allPieces)
        {
            piece.SetSelected();
        }

        _selectedPiece = _allPieces.Find(piece => piece.GetPos() == pos);
        return _selectedPiece;
    }

    public List<AAttackingPiece> GetAllPieces()
    {
        return _allPieces;
    }

    public List<ITileModel> GetAllTiles()
    {
        return _allTiles;
    }

    public bool IsBlueTurn()
    {
        return _bluesTurn;
    }

    public void AddPiece(AAttackingPiece piece)
    {
        _allPieces.Add(piece);
    }

    public void MovePiece(int toTile)
    {
        List<int> validMoveTiles = new List<int>();
        validMoveTiles = _selectedPiece.GetMoveTiles();

        if(_selectedPiece != null && _selectedPiece.IsAlive())
        {
            _selectedPiece.SetPos(toTile);

            if(_selectedPiece.CanChange())
            {
                return;
            }
            else if(validMoveTiles.Contains(toTile))
            {
                SwitchTurn();
            }
            else
            {
                throw new System.ArgumentException("Invalid move");
            }
        } 
        else
        {
            throw new System.ArgumentException("No (Alive) Piece is selected!");
        }
    }

    public void ChangePieceType(int type)
    {
        _selectedPiece.ChangeInto(type);
    }

    public void SwitchTurn()
    {
        if (_selectedPiece == null || !_selectedPiece.IsAlive())
        {
            throw new System.ArgumentException("No (Alive) Piece is selected!");
        }
        else
        {
            _selectedPiece.TurnSelectedFalse();
            _selectedPiece = null;
            _bluesTurn = !_bluesTurn;
        }
    }
    

    // void IBoardModel.ResetBoard()
    // {
    //     throw new System.NotImplementedException();
    // }
}
