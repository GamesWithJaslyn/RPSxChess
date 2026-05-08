using Mono.CSharp.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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
        UnHighlight();
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
        if (GameState.State().Equals(GamePhase.PLAYING))
        {
            Vector3 mouseScreenPosition = Input.mousePosition;

            // Convert the screen position to a world position
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            // In 2D, the z-value from ScreenToWorldPoint might be the camera's z position (e.g., -10).
            // To work in the 2D plane, set the z position to 0 or match your object's z position.
            mouseWorldPosition.z = 0f;

            // Log the world position to the console for verification
            Debug.Log("Mouse clicked at world position: " + mouseWorldPosition);
            HandleClick();
        }
    }

    private void HandleClick()
    {
        // CASE 1: Trying to move
        if (_boardModel.MovePiece(_id)) { return; }

        var selectedPiece = _boardModel.GetSelectedPiece();
        // CASE 3: Selecting a piece
        if (selectedPiece != null)
        {
            if (!_boardModel.SpecialAttackers.Contains(selectedPiece.PieceType))
            {
                SelectingAnotherPiece(selectedPiece);
            }
            else
            {
                SpecialAttackHandling(selectedPiece);
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
            if (_boardModel.SpecialAttackers.Contains(selectedPiece.PieceType))
            {
                _boardModel?.HideSpecialAttackRange();
            }

            SelectingAnotherPiece();
        }
        else
        {
            _boardModel.UnSelectPiece();
        }
    }

    private void SpecialAttackHandling(IPieceModel selectedPiece)
    {

        if (!selectedPiece.GetSpecialAttack().IsSpecialAttackReady)
        {
            _boardModel.UnHighlightAllTiles();
            if (!selectedPiece.Equals(_piece))
            {
                SelectingAnotherPiece();
                return;
            }
            else
            {
                _boardModel.ShowSpecialAttackRange();
            }
        }
        else
        {
            _boardModel.ExecuteSpecialAttack(_id);
            _boardModel.UnSelectPiece();
        }

    }

    private void MarkAsValid(bool isvalid)
    {
        if (isvalid) { HightLight(); }
        else { UnHighlight(); }
    }

    private void MarkAsSpecialAttackValid(bool isvalid)
    {
        if (isvalid) { SpecialAttackHighlight(); }
        else { UnHighlight(); }
    }

    /// <summary>
    /// Highlights the tile by changing its color.
    /// </summary>
    public void HightLight()
    {
        gameObject.transform.Find("Highlight").GetComponent<SpriteRenderer>().enabled = true;
        var isOffset = _id % 2 == 1;
        gameObject.transform.Find("Highlight").GetComponent<SpriteRenderer>().color =
                                    isOffset ? TileColor_DataBase.Regular_HighlightColorOffset
                                             : TileColor_DataBase.Regular_HighlightColorBase;
    }

    /// <summary>
    /// Unhighlights the tile by resetting its color to default.
    /// </summary>
    public void UnHighlight()
    {
        // GetComponent<SpriteRenderer>().color = _currentColor; // Reset to default color

        gameObject.transform.Find("Highlight").GetComponent<SpriteRenderer>().enabled = false;

    }

    public void SpecialAttackHighlight()
    {
        gameObject.transform.Find("Highlight").GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("Highlight").GetComponent<SpriteRenderer>().color =
                                    TileColor_DataBase.SpecialAttackColor;
    }
    private void OnDisable()
    {
        _model.OnTileValid -= MarkAsValid;
        _model.OnChangeType -= UpdateType;
        _model.OnPieceEntered -= PieceEnteredTile;
        _model.OnTileSpecialAttackValid -= MarkAsSpecialAttackValid;
    }

    private void AddEvents()
    {
        _model.OnTileValid += MarkAsValid;
        _model.OnChangeType += UpdateType;
        _model.OnPieceEntered += PieceEnteredTile;
        _model.OnTileSpecialAttackValid += MarkAsSpecialAttackValid;
    }
}
