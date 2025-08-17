using UnityEngine;
using UnityEngine.PlayerLoop;

public class TilePresenter : MonoBehaviour
{
    public ITileModel Model { get; private set; }
    [SerializeField] private int _id;
    [SerializeField] private int _type;
    [SerializeField] private AAttackingPiece _piece;
    [SerializeField] Color32 _baseColor = new Color32(152, 87, 95, 255);
    [SerializeField] Color32 _offsetColor = new Color32(202, 154, 192, 255);
    //[SerializeField] protected SpriteRenderer ren;



    // Assign a model instance to this view
    public void Init(ITileModel model)
    {
        Model = model;
        _id = model.GetID();
        _type = model.GetTileType();
        var isOffset = _id % 2 == 1;

        SpriteRenderer ren = this.GetComponent<SpriteRenderer>();
        ren.color = isOffset ? _offsetColor : _baseColor;
        // Example: you can also update visuals here based on Model
    }
}
