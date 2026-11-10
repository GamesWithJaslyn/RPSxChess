using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class BoardModelImpl : IBoardModel
{
    public bool _bluesTurn { get; private set; }
    private List<IPieceModel> _allPieces;
    private List<ITileModel> _allTiles;
    private Dictionary<IPieceModel, GameObject> _piecesKey;

    private IPieceModel _selectedPiece;
    private GameObject _selectedPieceView;
    private CreateTiles _createTiles;

    private List<AAttackingPiece> _kingList;

    public event Action<bool> OnChangeTurn;
    public List<PieceType> SpecialAttackers { get; } = new List<PieceType> { PieceType.Bow };

    public BoardModelImpl()
    {
        _createTiles = new CreateTiles();
        _allPieces = new CreatePieces(this).GetPieces();
        _allTiles = _createTiles.GetTiles();
        _piecesKey = new Dictionary<IPieceModel, GameObject>();
        _kingList = new List<AAttackingPiece>();
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

                if (ZeroPiecesLeftOfType((AAttackingPiece)capturedPiece))
                {
                    GameState.OnGameWon?.Invoke(_selectedPiece.Team);
                    return true;
                }
            }

            if (SpecialAttackers.Contains(_selectedPiece.PieceType)
            && _selectedPiece.GetSpecialAttack().IsSpecialAttackReady)
            {
                _selectedPiece?.GetSpecialAttack().CancelSpecialAttack(this);
            }

            targetTile.EnterPiece(_selectedPiece);

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
        ((AAttackingPiece)_selectedPiece)?.CallShowArrow();
    }

    public void HideSpecialAttackRange()
    {
        _selectedPiece?.GetSpecialAttack().CancelSpecialAttack(this);
        ((AAttackingPiece)_selectedPiece)?.CallHideArrow();
    }

    public async Task<bool?> ExecuteSpecialAttack(int tileID)
    {
        if (_selectedPiece != null && SpecialAttackers.Contains(_selectedPiece.PieceType))
        {
            Team selectedPieceTeam = _selectedPiece.Team;
            bool? result = await _selectedPiece?.GetSpecialAttack()
                                .ExecuteSpecialAttack(this, _selectedPiece, tileID);

            if (result == true)
            {
                if (ZeroPiecesLeftOfType((AAttackingPiece)GetPieceAt(tileID)))
                {
                    GameState.OnGameWon?.Invoke(selectedPieceTeam);
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
        if (KingCornered()) { return; }
        _bluesTurn = !_bluesTurn;
        ChangeBorder();
    }

    public bool KingCornered()
    {
        foreach (AAttackingPiece king in _kingList)
        {
            if (king.GetValidMoves().Count == 0)
            {
                if (king.Team == Team.Blue)
                {
                    GameState.OnGameWon?.Invoke(Team.Red);
                }
                else
                {
                    GameState.OnGameWon?.Invoke(Team.Blue);
                }

                return true;
            }
        }

        return false;
    }

    public void ChangeBorder()
    {
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

    private bool ZeroPiecesLeftOfType(AAttackingPiece target)
    {
        int pieceType = 0;
        AAttackingPiece king = null;

        foreach (AAttackingPiece piece in _allPieces)
        {
            if (piece.Team == target.Team && piece.IsAlive == true && piece.PieceType == target.PieceType)
            {
                pieceType++;
                king = piece;
            }
        }

        if (pieceType == 1)
        {
            if (king != null)
            {
                king.BecomeKing();
                _kingList.Add(king);
                Debug.Log("[BoardModelImpl] - Should become king");
            }
        }

        return pieceType == 0;
    }
}
