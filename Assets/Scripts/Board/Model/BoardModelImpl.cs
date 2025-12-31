using System.Collections.Generic;
using UnityEngine;

public class BoardModelImpl : IBoardModel
{
    public List<IPieceModel> _allPieces;
    public List<GameObject> _allPieceViews;
    public List<GameObject> _allTileViews;
    public List<ITileModel> _allTiles;
    
    private bool _bluesTurn;
    private IPieceModel _selectedPiece;
    private GameObject _selectedPieceView;
    private CreatePieces _createPieces;
    private CreateTiles _createTiles;

    public BoardModelImpl()
    {
        _allPieces = new List<IPieceModel>();
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

    public IPieceModel SelectPiece(int pos)
    {
        _selectedPiece = null;
        _selectedPieceView = null;

        foreach (GameObject pieceView in _allPieceViews)
        {
            IPieceModel pieceModel = pieceView.GetComponent<PieceView>().GetModel();
            
            if(pieceModel.IsAlive())
            {
                if (pieceModel.GetPos() == pos && 
                    ((_bluesTurn && pieceModel.IsSameTeam("Blue")) ||
                    (!_bluesTurn && pieceModel.IsSameTeam("Red"))))
                {
                    Debug.Log("[BoardModelImpl] - Selecting piece at position: " + pos);
                    _selectedPiece = pieceModel;
                    _selectedPieceView = pieceView;
                    _selectedPiece.SetSelected();
                    break;
                }
            }
            
        }

        // _selectedPieceView = LookUp(pos);
        // _selectedPiece =  _selectedPieceView.GetComponent<PieceView>().GetModel();

        if (_selectedPiece != null)
        {
            List<int> validMoveTiles = _selectedPiece.GetMoveTiles();
            SetTilesAsValidMoveTiles(validMoveTiles);
        }

        return _selectedPiece;
    }

    public IPieceModel GetSelectedPiece()
    {
        return _selectedPiece;
    }
    public GameObject LookUp(int pos, bool ignoreTeam)
    {
        foreach (GameObject pieceView in _allPieceViews)
        {
            PieceView pv = pieceView.GetComponent<PieceView>();
            IPieceModel pieceModel = pv.GetModel();

            if (pieceModel.GetPos() == pos && pieceModel.IsAlive())
            {
                if (!ignoreTeam)
                {
                    // Normal: only return your own team
                    if ((_bluesTurn && pieceModel.IsSameTeam("Blue")) ||
                        (!_bluesTurn && pieceModel.IsSameTeam("Red")))
                    {
                        pieceModel.SetSelected();
                        return pieceView;
                    }
                }
                else
                {
                    // Capture: return whatever is on that tile
                    return pieceView;
                }
            }
        }

        return null;

    }

    public void UnSelectPiece()
    {
        UnHighlightAllTiles();

        if(_selectedPiece == null) return;

        _selectedPiece.TurnSelectedFalse();
        _selectedPiece = null;
        _selectedPieceView = null;
    }
    public List<IPieceModel> GetAllPieces()
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

    public void AddPiece(IPieceModel piece)
    {
        _allPieces.Add(piece);
    }

    public void RemovePiece(IPieceModel piece)
    {
        _allPieces.Remove(piece);
    }

    public bool TryMovePiece(int toTile)
    {
        if(MovePiece(toTile))
        {
            return true;
        }
        else
        {
            UnSelectPiece();
            return false;
        }
    }


    // public void MovePiece(int toTile)
    // {
    //     List<int> validMoveTiles = _selectedPiece.GetMoveTiles();

    //     if(_selectedPiece != null && _selectedPiece.IsAlive())
    //     {
    //         if(validMoveTiles.Contains(toTile))
    //         {
    //             List<IEnterAndLeave> tileList = GetAllEnterableTiles();
    //             IEnterAndLeave oldTile = tileList.Find(t => t.GetID() == _selectedPiece.GetPos());
    //             IEnterAndLeave newTile = tileList.Find(t => t.GetID() == toTile);
                
    //             IPieceModel piece = newTile.GetPiece();
    //             bool canMove = newTile.CanEnter(_selectedPiece);
    //             UnHighlightAllTiles();

    //             if(canMove)
    //             {
    //                 if(piece != null)
    //                 {
    //                     GameObject obj = LookUp(toTile, true);
    //                     PieceView view = obj.GetComponent<PieceView>();
    //                     _allPieceViews.Remove(obj);
    //                     view.SetDead();
    //                     newTile.Leave();
                        
    //                     if(view.GetModel().PiecesLeft() == 0)
    //                     {
    //                         GameState.Win(_selectedPiece.GetPieceType());
    //                     }
    //                 }

    //                 if(GameState.StillPlaying())
    //                 {
    //                     oldTile.Leave();
    //                     newTile.Enter(_selectedPiece);
    //                     _selectedPiece.MoveTo(toTile, piece);
    //                     if(_selectedPiece.CanChange())
    //                     {
    //                         Debug.Log("[BoardModelImpl] - Piece can Change");
    //                         CoroutineHost.Instance.Starting_ButtonPress();
    //                         CoroutineHost.Instance.Waiting_ButtonPress();
    //                     }

    //                     SwitchTurn();
                        
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             throw new System.ArgumentException("Invalid move");
    //         }
    //     } 
    //     else
    //     {
    //         throw new System.ArgumentException("No (Alive) Piece is selected!");
    //     }
    // }


    public bool MovePiece(int toTile)
    {
        if(GameState.StillPlaying())
        {
            if(_selectedPiece != null && _selectedPiece.IsAlive())
            {
                List<int> validMoveTiles = _selectedPiece.GetMoveTiles();
                if(validMoveTiles.Contains(toTile)) 
                {
                    List<IEnterAndLeave> tileList = GetAllEnterableTiles();
                    IEnterAndLeave newTile = tileList.Find(t => t.GetID() == toTile);
                    tileList.Find(t => t.GetID() == _selectedPiece.GetPos()).Leave(); // leave old tile
                    bool valid = _selectedPiece.MoveTo(toTile, newTile.GetPiece());
                    UnHighlightAllTiles();

                    if(valid)
                    {
                        tileList.Find(t => t.GetID() == _selectedPiece.GetPos()).Leave(); // leave old tile
                        if(newTile.GetPiece() != null)
                        {
                            GameObject obj = LookUp(toTile, true); // get the piece being captured, if any
                            _allPieceViews.Remove(LookUp(toTile, true));
                            newTile.Leave(); // remove piece from tile

                            if(obj.GetComponent<PieceView>().GetModel().PiecesLeft() == 0)
                            {
                                GameState.Win(_selectedPiece.GetPieceType());
                            }
                        }
                        
                        newTile.Enter(_selectedPiece); // current piece entering new tile
                        if(_selectedPiece.CanChange())
                        {
                            ChangingInto.Instance.ShowChangeOptions(_selectedPieceView);
                        }

                        SwitchTurn();
                        return true;
                    }
                    else
                    {
                        tileList.Find(t => t.GetID() == _selectedPiece.GetPos()).Enter(_selectedPiece); 
                        return false;
                    }

                }

                    return false;
                }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
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
                IPieceModel target = tileModel.GetPiece();
                if(target != null && target.IsAlive())
                {
                    if(_selectedPiece is AAttackingPiece attack &&
                     target.GetPieceType() == attack.GetTargetType())
                    {
                        tileView.HightLight();
                        tileModel.IsValidTile_CanMoveHere(true);
                    }
                }
                else
                {
                    tileView.HightLight();
                    tileModel.IsValidTile_CanMoveHere(true);
                }

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
