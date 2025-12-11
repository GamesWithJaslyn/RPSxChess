using UnityEngine;

/// <summary>
/// Represents a Tile in the RPSxChess game.
/// This class is responsible for the visual representation of a tile in the game.
/// It handles highlighting and unhighlighting the tile.
/// </summary>
public class TileView : MonoBehaviour
{
    [Header("Tile Properties")]
    public IEnterAndLeave _model { get; private set; }
    public TilePresenter _presenter { get; private set; }
    [SerializeField] private int _id;
    [SerializeField] private int _type;
    
    [Header("Tile Colors")]
    [SerializeField] Color32 _baseColor = new Color32(152, 87, 95, 255);
    [SerializeField] Color32 _offsetColor = new Color32(202, 154, 192, 255);
    [SerializeField] Color32 _highlightColorBase = new Color32(217, 126, 41, 255);
    [SerializeField] Color32 _highlightColorOffset = new Color32(237, 214, 123, 255);
    [SerializeField] Color32 _currentColor;

    private IBoardModel _boardModel;

    // Assign a model instance to this view
    public void Init(IEnterAndLeave model, IBoardModel boardModel)
    {
        _model = model;
        _boardModel = boardModel;
        _id = model.GetID();
        _type = model.GetTileType();
        _presenter = new TilePresenter(this, model, _boardModel);

        var isOffset = _id % 2 == 1;
        SpriteRenderer ren = this.GetComponent<SpriteRenderer>();
        ren.color = isOffset ? _offsetColor : _baseColor;
        _currentColor = ren.color;
    }

    void OnMouseDown()
    {
        Debug.Log("[Tile View] - Clicked on a Tile");
        _presenter.ClickedOn();
    }

    /// <summary>
    /// Highlights the tile by changing its color.
    /// </summary>
    public void HightLight()
    {
        var isOffset = _id % 2 == 1;
        this.GetComponent<SpriteRenderer>().color = isOffset ? _highlightColorOffset : _highlightColorBase;
    }

    /// <summary>
    /// Unhighlights the tile by resetting its color to default.
    /// </summary>
    public void UnHighlight()
    {
        GetComponent<SpriteRenderer>().color = _currentColor; // Reset to default color
    }


    public IEnterAndLeave GetTileModel()
    {
        return _model;
    }
}
