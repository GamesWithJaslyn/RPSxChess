using System.Collections.Generic;
using UnityEngine;

public class BoardModelImpl : IBoardModel
{
    public List<AAttackingPiece> _allPieces;
    public List<GameObject> _allPieceViews;
    public List<GameObject> _allTileViews;
    public List<ITileModel> _allTiles;
    
    private bool _bluesTurn;
    private AAttackingPiece _selectedPiece;
    private GameObject _selectedPieceView;
    private CreatePieces _createPieces;
    private CreateTiles _createTiles;

    // Static instance, accessible globally
    private static BoardModelImpl _instance;

    // Public property to access the instance
    // public static BoardModelImpl Board
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = new BoardModelImpl();
    //         }
    //         return _instance;
    //     }
    // }

    public BoardModelImpl()
    {
        _allPieces = new List<AAttackingPiece>();
        _allTiles = new List<ITileModel>();
        _bluesTurn = true;
        _selectedPiece = null;
        _selectedPieceView = null;
        _createPieces = new CreatePieces(this);
        _createTiles = new CreateTiles(this);

        _allTiles = _createTiles.GetTiles();
        _allPieces = _createPieces.GetPieces();

        Debug.Log("Pieces count: " + _allPieces.Count);
    }

    public AAttackingPiece SelectPiece(int pos)
    {
        _selectedPiece = null;
        _selectedPieceView = null;

        foreach (GameObject pieceView in _allPieceViews)
        {
            AAttackingPiece pieceModel = pieceView.GetComponent<PieceView>().GetModel();
            if (pieceModel.GetPos() == pos && pieceModel.IsAlive() && 
                ((_bluesTurn && pieceModel.IsSameTeam("Blue")) || (!_bluesTurn && pieceModel.IsSameTeam("Red"))))
            {
                _selectedPiece = pieceModel;
                _selectedPieceView = pieceView;
                _selectedPiece.SetSelected();
                break;
            }
        }

        if (_selectedPiece != null)
        {
            List<int> validMoveTiles = _selectedPiece.GetMoveTiles();
            SetTilesAsValidMoveTiles(validMoveTiles);
        }

        return _selectedPiece;
    }

    public void UnSelectPiece()
    {
        UnHighlightAllTiles();

        if(_selectedPiece == null) return;

        _selectedPiece.TurnSelectedFalse();
        _selectedPiece = null;
        _selectedPieceView = null;
    }
    public List<AAttackingPiece> GetAllPieces()
    {
        return _allPieces;
    }

    public List<ITileModel> GetAllTiles()
    {
        return _allTiles;
    }

    public List<IEnterAndLeave> GetAllEnterableTiles()
    {
        var list = new List<IEnterAndLeave>();
        foreach (var tile in _allTiles)
        {
            if (tile is IEnterAndLeave enterable)
                list.Add(enterable);
        }
        return list;
    }

    public bool IsBlueTurn()
    {
        return _bluesTurn;
    }

    public void AddPiece(AAttackingPiece piece)
    {
        _allPieces.Add(piece);
    }

    public bool TryMovePiece(int toTile)
    {
        try
        {
            MovePiece(toTile);
            return true;
        }
        catch
        {
            UnSelectPiece();
            return false;
        }
    }

    public void MovePiece(int toTile)
    {
        List<int> validMoveTiles = _selectedPiece.GetMoveTiles();

        if(_selectedPiece != null && _selectedPiece.IsAlive())
        {

            if(_selectedPiece.CanChange())
            {
                return;
            }
            else if(validMoveTiles.Contains(toTile))
            {
                List<IEnterAndLeave> tileList = GetAllEnterableTiles();
                IEnterAndLeave oldTile = tileList.Find(t => t.GetID() == _selectedPiece.GetPos());
                IEnterAndLeave newTile = tileList.Find(t => t.GetID() == toTile);

                oldTile.Leave();
                newTile.Enter(_selectedPiece);

                _selectedPieceView.GetComponent<PieceView>().MoveTo(toTile);
                UnHighlightAllTiles();
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

    public void SetTilesAsValidMoveTiles(List<int> validMoveTiles)
    {
        UnHighlightAllTiles();

        foreach (GameObject tile in _allTileViews)
        {
            TileView tileView = tile.GetComponent<TileView>();
            IEnterAndLeave tileModel = tileView.GetTileModel();
            
            if (validMoveTiles.Contains(tileModel.GetID()))
            {
                tileView.HightLight();
                tileModel.IsValidTile_CanMoveHere(true);
            }
        }
    }

    public void UnHighlightAllTiles()
    {
        foreach (GameObject tile in _allTileViews)
        {
            tile.GetComponent<TileView>().UnHighlight();
            tile.GetComponent<TileView>().GetTileModel().IsValidTile_CanMoveHere(false);
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

    public void SetPieceViewList(List<GameObject> pieceViews)
    {
        _allPieceViews = pieceViews;
    }

    public void SetTileViewList(List<GameObject> tileViews)
    {
        _allTileViews = tileViews;
    }


    // void IBoardModel.ResetBoard()
    // {
    //     throw new System.NotImplementedException();
    // }
}
