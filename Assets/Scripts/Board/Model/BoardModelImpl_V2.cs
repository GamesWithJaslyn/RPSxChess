using System.Collections.Generic;
using System.Linq;
using Codice.CM.Common.Merge;
using Mono.CSharp;
using Mono.CSharp.Linq;
using UnityEngine;

public class BoardModelImpl_V2 : IBoardModel_V2
{
    private List<IPieceModel_V2> _allPieces;
    private List<ITileModel> _allTiles;
    private Dictionary<IPieceModel_V2, GameObject> _piecesKey;
    private bool _bluesTurn;
    private IPieceModel_V2 _selectedPiece;
    private GameObject _selectedPieceView;

    public BoardModelImpl_V2()
    {
        _allPieces = new CreatePieces_V2(this).GetPieces();
        _allTiles = new CreateTiles_V2(this).GetTiles();
        _piecesKey = new Dictionary<IPieceModel_V2, GameObject>();
        _bluesTurn = true;
        _selectedPiece = null;
        _selectedPieceView = null;

        GameState.OnRematch += ResetBoard;
    }

    public IPieceModel_V2 SelectPiece(int pos)
    {
        UnSelectPiece();

        foreach (IPieceModel_V2 pieceModel in _allPieces)
        {
            if (pieceModel.IsAlive)
            {
                if (pieceModel.Position == pos && ((_bluesTurn && pieceModel.IsSameTeam(Team.Blue))
                || (!_bluesTurn && pieceModel.IsSameTeam(Team.Red))))
                {
                    Debug.Log("[BoardModelImpl] - Selecting piece at position: " + pos);
                    _selectedPiece = pieceModel;
                    _selectedPieceView = _piecesKey[_selectedPiece];
                    _selectedPiece.IsSelected = true;
                    SetTilesAsValidMoveTiles(_selectedPiece.GetValidMoves());
                    break;
                }
            }

        }

        return _selectedPiece;
    }
    public void UnSelectPiece()
    {
        UnHighlightAllTiles();
        if (_selectedPiece != null)
        {
            _selectedPiece.IsSelected = false;
        }
        _selectedPiece = null;
        _selectedPieceView = null;
    }
    private void UnselectAllPieces()
    {
        foreach (IPieceModel_V2 piece in _allPieces) { piece.IsSelected = false; }
    }

    public void SetPiecesDictionary(Dictionary<IPieceModel_V2, GameObject> pk) { _piecesKey = pk; }
    public void SetTilesAsValidMoveTiles(List<Move> validMoveTiles)
    {
        UnHighlightAllTiles();
        foreach (Move move in validMoveTiles)
        {
            foreach (ITileModel tile in _allTiles)
            {
                if (move._to == tile.ID)
                {
                    tile.SetValid(true);
                }
            }
        }
    }


    public IPieceModel_V2 GetSelectedPiece() { return _selectedPiece; }
    public List<IPieceModel_V2> GetAllPieces() { return _allPieces; }
    public List<ITileModel> GetAllTiles() { return _allTiles; }
    public IPieceModel_V2 GetPieceAt(int tileID)
    {
        return _allPieces.FirstOrDefault(piece => piece?.Position == tileID);
    }
    public ITileModel GetTileAt(int tileID)
    {
        return _allTiles.FirstOrDefault(tile => tile?.ID == tileID);
    }

    public bool IsInside(int tileID) { return _allTiles.Any(t => t.ID == tileID); }
    public bool IsBlueTurn() { return _bluesTurn; }
    public void AddPiece(IPieceModel_V2 piece) { _allPieces.Add(piece); }
    public void RemovePiece(IPieceModel_V2 piece) { _allPieces.Remove(piece); }

    public bool TryMovePiece(int toTile)
    {
        if (MovePiece(toTile))
        {
            return true;
        }
        else
        {
            UnSelectPiece();
            return false;
        }
    }

    public bool MovePiece(int toTile)
    {
        if (_selectedPiece == null) return false;
        Move moveMade = _selectedPiece.MakeMove(toTile);
        if (moveMade._to == toTile)
        {
            if (moveMade._flags.Contains(MoveFlags.Capture))
            {
                IPieceModel_V2 target = GetTileAt(toTile).Occupant;
                target.SetDead();

                if (ZeroPiecesLeft(target.Team))
                {
                    GameState.OnGameWon?.Invoke(_selectedPiece.Team);
                }

                GetTileAt(toTile).Occupant = null;
            }

            GetTileAt(moveMade._from).Occupant = null;
            GetTileAt(toTile).Occupant = _selectedPiece;

            if (moveMade._flags.Contains(MoveFlags.Promotion))
            {
                ChangingInto.Instance.ShowChangeOptions(_selectedPieceView);
            }

            SwitchTurn();
            return true;

        }
        else { return false; }
    }


    public void UnHighlightAllTiles()
    {
        foreach (ITileModel tile in _allTiles) { tile.SetValid(false); }
    }

    public void SwitchTurn()
    {
        UnSelectPiece();
        _bluesTurn = !_bluesTurn;
    }

    public void ResetBoard()
    {
        _bluesTurn = true;
        UnSelectPiece();
        ResetTiles();

        foreach (IPieceModel_V2 piece in _allPieces)
        {
            piece.Reset();
        }
    }


    /// <summary>
    /// Resets the tiles on the board to their original state.
    /// </summary>
    private void ResetTiles()
    {
        foreach (TileModel tile in _allTiles)
        {
            tile.Occupant = null;
            tile.SetValid(false);
        }
    }

    private bool ZeroPiecesLeft(Team team)
    {
        int bows = 0;
        int swords = 0;
        int pegasus = 0;

        foreach (AAttackingPiece_V2 piece in _allPieces)
        {
            if (piece.Team == team && piece.IsAlive == true)
            {
                switch (piece.PieceType)
                {
                    case PieceType.Bow:
                        bows++;
                        break;
                    case PieceType.Sword:
                        swords++;
                        break;
                    case PieceType.Pegasus:
                        pegasus++;
                        break;
                }
            }
        }

        return (bows == 0) || (swords == 0) || (pegasus == 0);
    }
}
