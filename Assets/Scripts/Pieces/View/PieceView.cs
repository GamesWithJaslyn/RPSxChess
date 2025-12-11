using UnityEngine;

/// <summary>
/// Represents a Piece in the RPSxChess game.
/// This class is responsible for the visual representation of a piece in the game.
/// It handles the movement of the piece in the game world.
/// </summary>
public class PieceView : MonoBehaviour
{
    public AAttackingPiece _model { get; private set; }
    public PiecePresenter _presenter { get; private set; }
    [SerializeField] private int _pos;
    [SerializeField] private int _type;
    [SerializeField] private int _targetType;
    private IBoardModel _boardModel;
 
    // Assign a model instance to this view
    public void Init(AAttackingPiece model, IBoardModel boardModel)
    {
        _model = model;
        _boardModel = boardModel;
        _pos = model.GetPos();
        _type = model.GetPieceType();
        _targetType = model.GetTargetType();
       // _presenter = new PiecePresenter(this, model, _boardModel);
    }

    public void MoveTo(int toTile)
    {
        Vector3 position = new Vector3(toTile % 11, -1 * (toTile / 11), -1);
        _model.SetPos(toTile);
        _pos = _model.GetPos();
        transform.position = position;
    }

    // void OnMouseDown()
    // {
    //     float depth = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        
    //     Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
    //     new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth));

    //     Debug.Log("[Piece View] - Mouse pos: " + mouseWorldPos);
    //     Debug.Log("[Piece View] - Piece pos: " + transform.position);

    //     if (mouseWorldPos == transform.position)
    //     {
    //         Debug.Log("[Piece View] - Clicked on a Piece");
    //         _presenter.ClickedOn(mouseWorldPos);
    //         MoveTo(mouseWorldPos);
    //     }
    // }

    public AAttackingPiece GetModel()
    {
        return _model;
    }
}
