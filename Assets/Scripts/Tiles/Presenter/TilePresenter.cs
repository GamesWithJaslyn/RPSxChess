using UnityEngine;
using UnityEngine.PlayerLoop;

public class TilePresenter
{
    [Header("Tile Files")]
    //public ITileModel _tileModel { get; private set; }
    public IEnterAndLeave _enterModel { get; private set; }
    public TileView _view { get; private set; }

    [Header("Tile Properties")]
    [SerializeField] private int _id;
    [SerializeField] private int _type;
    [SerializeField] private IPieceModel _piece;

    private IBoardModel _boardModel;

    public TilePresenter(TileView view, IEnterAndLeave enterModel, IBoardModel boardModel)
    {
        _view = view;
        _enterModel = enterModel;
        _id = enterModel.GetID();
        _type = enterModel.GetTileType();
        _boardModel = boardModel;
        _piece = null;
    }

    /// <summary>
    /// Called when the piece is clicked on.
    /// </summary>
    /// <param name="mouseClick"></param>
    public void ClickedOn()
    {
        Debug.Log("[TilePresenter] - Clicked on tile at " + _id);

        // CASE 1: Trying to move
        if (_boardModel.TryMovePiece(_id))
            return;

        // CASE 2: Selecting a piece
        var piece = _boardModel.SelectPiece(_id);

        if (piece != null)
        {
            piece.SetSelected();
            Debug.Log("[TilePresenter] - Selected piece at " + _id);
        }
        else
        {
            _boardModel.UnSelectPiece();
            Debug.Log("[TilePresenter] - No piece selected at " + _id);
        }
    }


    // // Assign a model instance to this view
    // public void Init(ITileModel model)
    // {
    //     _model = model;
    //     _id = model.GetID();
    //     _type = model.GetTileType();
    //     var isOffset = _id % 2 == 1;

    //     SpriteRenderer ren = this.GetComponent<SpriteRenderer>();
    //     ren.color = isOffset ? _offsetColor : _baseColor;
    //     // Example: you can also update visuals here based on Model

    // }
}
