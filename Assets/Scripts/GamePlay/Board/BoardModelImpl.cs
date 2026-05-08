using System;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.Common.Merge;
using Mono.CSharp;
using Mono.CSharp.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BoardModelImpl : IBoardModel
{
    private List<IPieceModel> _allPieces;
    private List<ITileModel> _allTiles;
    private Dictionary<IPieceModel, GameObject> _piecesKey;
    private bool _bluesTurn;
    private IPieceModel _selectedPiece;
    private GameObject _selectedPieceView;
    private CreateTiles _createTiles;

    public event Action<bool> OnChangeTurn;
    public List<PieceType> SpecialAttackers { get; } = new List<PieceType> { PieceType.Bow };

    public BoardModelImpl()
    {
        _createTiles = new CreateTiles();
        _allPieces = new CreatePieces(this).GetPieces();
        _allTiles = _createTiles.GetTiles();
        _piecesKey = new Dictionary<IPieceModel, GameObject>();
        _bluesTurn = true;
        _selectedPiece = null;
        _selectedPieceView = null;

        GameState.OnRematch += ResetBoard;
    }

    public IPieceModel SelectPiece(int pos)
    {
        UnSelectPiece();

        foreach (IPieceModel pieceModel in _allPieces)
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
        foreach (IPieceModel piece in _allPieces) { piece.IsSelected = false; }
    }

    public void SetPiecesDictionary(Dictionary<IPieceModel, GameObject> pk) { _piecesKey = pk; }
    public void SetTilesAsValidMoveTiles(List<Move> validMoveTiles)
    {
        UnHighlightAllTiles();
        foreach (Move move in validMoveTiles)
        {
            foreach (ITileModel tile in _allTiles)
            {
                if (move.To == tile.ID)
                {
                    tile.SetValid(true);
                }
            }
        }
    }

    public void SetTilesAsValidSpecialAttackTiles(List<int> specialAttackTiles)
    {
        UnHighlightAllTiles();
        foreach (int tileID in specialAttackTiles)
        {
            foreach (ITileModel tile in _allTiles)
            {
                if (tileID == tile.ID)
                {
                    tile.SetSpecialAttackValid(true);
                }
            }
        }
    }


    public IPieceModel GetSelectedPiece() { return _selectedPiece; }
    public List<IPieceModel> GetAllPieces() { return _allPieces; }
    public List<ITileModel> GetAllTiles() { return _allTiles; }
    public IPieceModel GetPieceAt(int tileID)
    {
        return _allPieces.FirstOrDefault(piece => piece?.Position == tileID);
    }
    public ITileModel GetTileAt(int tileID)
    {
        return _allTiles.FirstOrDefault(tile => tile?.ID == tileID);
    }

    public bool IsInside(int tileID) { return _allTiles.Any(t => t.ID == tileID); }
    public bool IsBlueTurn() { return _bluesTurn; }
    public void AddPiece(IPieceModel piece) { _allPieces.Add(piece); }
    public void RemovePiece(IPieceModel piece) { _allPieces.Remove(piece); }

    public void ShowArrow()
    {
        if (_selectedPiece is AAttackingPiece attackingPiece)
        {
            attackingPiece.ShowArrow();
        }
    }

    public void ResetArrow()
    {
        if (_selectedPiece is AAttackingPiece attackingPiece)
        {
            attackingPiece.ResetArrow();
        }
    }

    public bool TryMovePiece(int toTile)
    {
        if (MovePiece(toTile))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool MovePiece(int toTile)
    {
        if (_selectedPiece == null) return false;
        Move moveMade = _selectedPiece.MakeMove(toTile);

        if (moveMade.To == toTile)
        {
            ITileModel targetTile = GetTileAt(moveMade.To);
            GetTileAt(moveMade.From).EnterPiece(null);

            if (moveMade.Flags.Contains(MoveFlags.Capture))
            {
                IPieceModel capturedPiece = targetTile.Occupant;
                targetTile.EnterPiece(null);
                capturedPiece.SetDead();

                if (ZeroPiecesLeft(capturedPiece.Team))
                {
                    GameState.OnGameWon?.Invoke(_selectedPiece.Team);
                    return true;
                }
            }

            targetTile.EnterPiece(_selectedPiece);
            Debug.Log($"[BoardModelImpl] - {targetTile.Occupant.Team}_{targetTile.Occupant.PieceType} now on tile {targetTile.ID}");

            if (moveMade.Flags.Contains(MoveFlags.Promotion))
            {
                ChangingInto.Instance.ShowChangeOptions(_selectedPieceView);
            }

            SwitchTurn();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowSpecialAttackRange()
    {
        List<int> specialAttackRange = _selectedPiece.GetSpecialAttack()
                                                     .GetSpecialAttackRange(this, _selectedPiece);
        SetTilesAsValidSpecialAttackTiles(specialAttackRange);
    }

    public void HideSpecialAttackRange()
    {
        _selectedPiece?.GetSpecialAttack().CancelSpecialAttack(this);
    }

    public bool? ExecuteSpecialAttack(int tileID)
    {
        if (_selectedPiece != null && SpecialAttackers.Contains(_selectedPiece.PieceType))
        {
            Debug.Log($"[BoardModelImpl] - Executing special attack on tile {tileID} by piece at position {_selectedPiece.Position}");
            bool? result = _selectedPiece?.GetSpecialAttack()
                                         .ExecuteSpecialAttack(this, _selectedPiece, tileID);
            if (result == true)
            {
                if (ZeroPiecesLeft(GetPieceAt(tileID).Team))
                {
                    GameState.OnGameWon?.Invoke(_selectedPiece.Team);
                    return true;
                }

                SwitchTurn();
            }
            else if (result == false || result == null)
            {
                UnHighlightAllTiles();
                _selectedPiece.GetSpecialAttack().CancelSpecialAttack(this);
            }

            return result;
        }

        return false;
    }


    public void UnHighlightAllTiles()
    {
        foreach (ITileModel tile in _allTiles) { tile.SetValid(false); }
    }

    public void SwitchTurn()
    {
        UnSelectPiece();
        _bluesTurn = !_bluesTurn;
        OnChangeTurn?.Invoke(_bluesTurn);
    }

    public void ResetBoard()
    {
        _bluesTurn = true;
        UnSelectPiece();
        ResetTiles();

        foreach (IPieceModel piece in _allPieces)
        {
            piece.Reset();
        }
    }


    /// <summary>
    /// Resets the tiles on the board to their original state.
    /// </summary>
    private void ResetTiles()
    {
        foreach (ITileModel tile in _allTiles)
        {
            tile.EnterPiece(null);
            tile.SetValid(false);
        }

        foreach (IPieceModel piece in GetAllPieces())
        {
            ITileModel tile = GetAllTiles().Find(tile => tile.ID == piece.Position);
            tile.EnterPiece(piece);
        }

        _createTiles.ChangeToWater(_allTiles);
    }

    private bool ZeroPiecesLeft(Team team)
    {
        int bows = 0;
        int swords = 0;
        int pegasus = 0;

        foreach (AAttackingPiece piece in _allPieces)
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
