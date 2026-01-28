using Mono.CSharp.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Represents a Tile in the RPSxChess game.
/// This class is responsible for the visual representation of a tile in the game.
/// It handles highlighting and unhighlighting the tile.
/// </summary>
public class TileView : MonoBehaviour
{
    [Header("Tile Properties")]
    [SerializeField] private int _id;
    [SerializeField] Color32 _currentColor;

    public ITileModel _model { get; private set; }
    private IPieceModel _piece;
    private IBoardModel _boardModel;

    // Assign a model instance to this view
    public void Init(ITileModel model, IBoardModel boardModel)
    {
        _model = model;
        _boardModel = boardModel;
        _id = model.ID;
        _piece = null;
        AddEvents();

        UpdateType();
    }

    private void PieceEnteredTile(IPieceModel piece)
    {
        _piece = piece;
    }

    private void UpdateType()
    {
        SpriteRenderer ren = this.GetComponent<SpriteRenderer>();

        switch (_model.Type)
        {
            case TileType.Regular:
                var isOffset = _id % 2 == 1;
                ren.color = isOffset ? TileColor_DataBase.Regular_OffsetColor
                                      : TileColor_DataBase.Regular_BaseColor;
                break;
            case TileType.Water:
                ren.color = TileColor_DataBase.WaterColor;
                break;
            case TileType.Teleport:
                ren.color = TileColor_DataBase.TeleportColor;
                break;
        }

        _currentColor = ren.color;

    }

    void OnMouseDown()
    {
        if (GameState.State().Equals(GamePhase.PLAYING)) { HandleClick(); }
    }

    private void HandleClick()
    {
        // CASE 1: Trying to move
        if (_boardModel.MovePiece(_id)) { return; }

        var selectedPiece = _boardModel.GetSelectedPiece();
        // CASE 2: Selecting a piece
        if (selectedPiece != null)
        {
            if (selectedPiece.PieceType != PieceType.Bow)
            {
                SelectingAnotherPiece(selectedPiece);
            }
            else
            {
                ArrowHandling(selectedPiece);
            }
        }
        else
        {
            SelectingAnotherPiece();
        }
    }

    private void SelectingAnotherPiece()
    {
        _boardModel.UnSelectPiece();
        _boardModel.SelectPiece(_id);
    }

    private void SelectingAnotherPiece(IPieceModel selectedPiece)
    {
        // Clicking on a piece of the same team
        if (_piece != null && selectedPiece.IsSameTeam(_piece.Team))
        {
            SelectingAnotherPiece();
        }
        else
        {
            _boardModel.UnSelectPiece();
        }
    }

    private void ArrowHandling(IPieceModel selectedPiece)
    {
        if (!selectedPiece.Equals(_piece))
        {
            SelectingAnotherPiece();
            return;
        }

        if (selectedPiece is AAttackingPiece bowPiece)
        {
            if (bowPiece.HasShownArrow())
            {
                _boardModel.ResetArrow();
                _boardModel.UnSelectPiece();
            }
            else
            {
                _boardModel.ShowArrow();
            }
        }
    }

    private void MarkAsValid(bool isvalid)
    {
        if (isvalid) { HightLight(); }
        else { UnHighlight(); }
    }

    /// <summary>
    /// Highlights the tile by changing its color.
    /// </summary>
    public void HightLight()
    {
        var isOffset = _id % 2 == 1;
        GetComponent<SpriteRenderer>().color =
                                    isOffset ? TileColor_DataBase.Regular_HighlightColorOffset
                                             : TileColor_DataBase.Regular_HighlightColorBase;
    }

    /// <summary>
    /// Unhighlights the tile by resetting its color to default.
    /// </summary>
    public void UnHighlight()
    {
        GetComponent<SpriteRenderer>().color = _currentColor; // Reset to default color
    }
    private void OnDisable()
    {
        _model.OnTileValid -= MarkAsValid;
        _model.OnChangeType -= UpdateType;
        _model.OnPieceEntered -= PieceEnteredTile;
    }

    private void AddEvents()
    {
        _model.OnTileValid += MarkAsValid;
        _model.OnChangeType += UpdateType;
        _model.OnPieceEntered += PieceEnteredTile;
    }
}
