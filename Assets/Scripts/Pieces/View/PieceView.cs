using UnityEngine;

/// <summary>
/// Represents a Piece in the RPSxChess game.
/// This class is responsible for the visual representation of a piece in the game.
/// It handles the movement of the piece in the game world.
/// </summary>
public class PieceView : MonoBehaviour
{
    public IPieceModel _model { get; private set; }
    [SerializeField] private int _pos;
    [SerializeField] private int _type;
    [SerializeField] private int _targetType;
    private IBoardModel _boardModel;
 
    // Assign a model instance to this view
    public void Init(IPieceModel model, IBoardModel boardModel)
    {
        _model = model;
        _boardModel = boardModel;
        _pos = model.GetPos();
        _type = model.GetPieceType();
        if(model is AAttackingPiece attack)
        {
            _targetType = attack.GetTargetType();
        }
        
    }

    public void MoveTo(int toTile)
    {
        Vector3 position = new Vector3(toTile % 11, -1 * (toTile / 11), -1);
        _model.SetPos(toTile);
        _pos = _model.GetPos();
        transform.position = position;
    }

    public void SetDead()
    {
        _model.SetDead();
        gameObject.SetActive(false);
    }

    public IPieceModel GetModel()
    {
        return _model;
    }
}
