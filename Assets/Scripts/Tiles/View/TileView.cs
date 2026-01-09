// using UnityEngine;

// /// <summary>
// /// Represents a Tile in the RPSxChess game.
// /// This class is responsible for the visual representation of a tile in the game.
// /// It handles highlighting and unhighlighting the tile.
// /// </summary>
// public class TileView : MonoBehaviour
// {
//     [Header("Tile Properties")]
//     public IEnterAndLeave _model { get; private set; }
//     [SerializeField] private int _id;
//     [SerializeField] private TileType _type;
//     [SerializeField] private IPieceModel_V2 _piece;
    
//     [Header("Tile Colors")]
//     [SerializeField] Color32 _baseColor = new Color32(152, 87, 95, 255);
//     [SerializeField] Color32 _offsetColor = new Color32(202, 154, 192, 255);
//     [SerializeField] Color32 _highlightColorBase = new Color32(217, 126, 41, 255);
//     [SerializeField] Color32 _highlightColorOffset = new Color32(237, 214, 123, 255);
//     [SerializeField] Color32 _currentColor;

//     private IBoardModel_V2 _boardModel;

//     // Assign a model instance to this view
//     public void Init(IEnterAndLeave model, IBoardModel_V2 boardModel)
//     {
//         _model = model;
//         _boardModel = boardModel;
//         _id = model.GetID();
//         _type = model.GetTileType();
//         _piece = null;

//         var isOffset = _id % 2 == 1;
//         SpriteRenderer ren = this.GetComponent<SpriteRenderer>();
//         ren.color = isOffset ? _offsetColor : _baseColor;
//         _currentColor = ren.color;
//     }

//     void OnMouseDown()
//     {
//         if(GameState.State().Equals(GamePhase.PLAYING))
//         {
//             Debug.Log("[Tile View] - OnMouseDown()");
//             HandleClick();
//         }
//         else
//         {
//             Debug.Log("[Tile View] - Can't interact with tiles right now.");
//         }

//     }

//     private void HandleClick()
//     {
//         Debug.Log("[Tile View] - Handle Click() -> Clicked on a tile: " + _id);
//         // CASE 1: Trying to move
//         if (_boardModel.TryMovePiece(_id)) return;

//         // CASE 2: Selecting a piece
//         var selectedPiece = _boardModel.GetSelectedPiece();

//         if(selectedPiece != null)
//         {
//             if(_piece != null && selectedPiece.IsSameTeam(_piece.GetTeam()))
//             {
//                 _boardModel.UnSelectPiece();
//                 _boardModel.SelectPiece(_id);
//             }
//             else
//             {
//                 _boardModel.UnSelectPiece();
//             }
            
//         }
//         else
//         {
//             _boardModel.UnSelectPiece();
//             _boardModel.SelectPiece(_id);
//         }

//         // if (_piece != null) //piece on this tile should get selected
//         // {
//         //     selectedPiece.SetSelected();
//         // }
//         // else if (selectedPiece == null) //no piece on this tile, should deselect any selected piece
//         // {
//         //     _boardModel.UnSelectPiece();
//         // }
//     }

//     /// <summary>
//     /// Highlights the tile by changing its color.
//     /// </summary>
//     public void HightLight()
//     {
//         var isOffset = _id % 2 == 1;
//         this.GetComponent<SpriteRenderer>().color = isOffset ? _highlightColorOffset : _highlightColorBase;
//     }

//     /// <summary>
//     /// Unhighlights the tile by resetting its color to default.
//     /// </summary>
//     public void UnHighlight()
//     {
//         GetComponent<SpriteRenderer>().color = _currentColor; // Reset to default color
//     }


//     public IEnterAndLeave GetTileModel()
//     {
//         return _model;
//     }
// }
