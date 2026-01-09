// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class BoardTilesPresenter : MonoBehaviour
// {
//     private IBoardModel _boardModel;
//     private Dictionary<int, Vector3> _tilePos;
//     private Dictionary<int, Vector3> _piecePos;


//     [Header("Tile Prefab:")]
//     [SerializeField] private GameObject _regularTile;
//     [SerializeField] private Transform _cam;
//     [SerializeField] private Transform _tileParent;

//     [Header("Pieces Prefab:")]
//     [SerializeField] private GameObject _blueBow;
//     [SerializeField] private GameObject _blueSword;
//     [SerializeField] private GameObject _bluePegasus;
//     [SerializeField] private GameObject _redBow;
//     [SerializeField] private GameObject _redSword;
//     [SerializeField] private GameObject _redPegasus;
//     [SerializeField] private Transform _pieceParent;

//     [Header("Buttons for Change")]
//     [SerializeField] private List<Button> _buttons;

//     public List<GameObject> _allPieceViews;
//     public List<GameObject> _allTileViews;
//     private List<Sprite> _sprites;

//     void Start()
//     {
//         _tilePos = new Dictionary<int, Vector3>();
//         _piecePos = new Dictionary<int, Vector3>();
//         _allPieceViews = new List<GameObject>();
//         _allTileViews = new List<GameObject>();

//         _sprites = new List<Sprite>
//         {
//             _blueBow.GetComponent<SpriteRenderer>().sprite,
//             _blueSword.GetComponent<SpriteRenderer>().sprite,
//             _bluePegasus.GetComponent<SpriteRenderer>().sprite,

//             _redBow.GetComponent<SpriteRenderer>().sprite,
//             _redSword.GetComponent<SpriteRenderer>().sprite,
//             _redPegasus.GetComponent<SpriteRenderer>().sprite,
//         };


//         _boardModel = new BoardModelImpl();
//         InitializeBoard();
//     }

//     private void InitializeBoard()
//     {
//         InitializeTiles();
//         InitializePieces();
//     }

//     private void InitializeTiles()
//     {
//         if (!_cam)
//         {
//             _cam = Camera.main.transform;
//             return;
//         }

//         int j = 0;
//         int y = 0;

//         //regular tiles
//         for (int x = 0; x < _boardModel.GetAllTiles().Count; x++)
//         {
//             if (x % 11 == 0 && x != 0)
//             {
//                 j = 0;
//                 y--;
//             }
//             var spawnedTile = Instantiate(_regularTile, new Vector3(j, y),
//             Quaternion.identity, _tileParent);
//             spawnedTile.name = $"Tile({j}, {y})";
//             spawnedTile.AddComponent<TileView>().Init(_boardModel.GetAllEnterableTiles()
//             .Find(t => t.GetID() == x), _boardModel);

//             _tilePos[x] = new Vector3(j, y);
//             j++;
//             _allTileViews.Add(spawnedTile);
//         }

//         _cam.transform.position = new Vector3((float)j / 2 - 0.5f, (float)y / 2, -10);
//         _boardModel.SetTileViewList(_allTileViews);
//     }

//     private void InitializePieces()
//     {

//         foreach (AAttackingPiece piece in _boardModel.GetAllPieces())
//         {
//             GameObject prefab;

//             if (piece is Bow)
//             {
//                 if (piece.GetPieceType() == 1)
//                 {
//                     Debug.Log("Instantiate Blue Bow");
//                     prefab = _blueBow;
//                 }
//                 else
//                 {
//                     Debug.Log("Instantiate Red Bow");
//                     prefab = _redBow;
//                 }
//             }

//             else if (piece is Sword)
//             {
//                 if (piece.GetPieceType() == 2)
//                 {
//                     Debug.Log("Instantiate Blue Sword");
//                     prefab = _blueSword;
//                 }
//                 else
//                 {
//                     Debug.Log("Instantiate Red Sword");
//                     prefab = _redSword;
//                 }
//             }

//             else if (piece is Pegasus)
//             {
//                 if (piece.GetPieceType() == 3)
//                 {
//                     Debug.Log("Instantiate Blue Pegasus");
//                     prefab = _bluePegasus;
//                 }
//                 else
//                 {
//                     Debug.Log("Instantiate Red Pegasus");
//                     prefab = _redPegasus;
//                 }
//             }
//             else
//             {
//                 prefab = null;
//                 Debug.Log("Incorrect Piece");
//             }

//             IEnterAndLeave tile = _boardModel.GetAllEnterableTiles()
//             .Find(tile => tile.GetID() == piece.GetPos());
//             tile.Enter(piece);


//             _allPieceViews.Add(InstaniatePiece(piece, prefab, _pieceParent));
//         }

//         _boardModel.SetPieceViewList(_allPieceViews);
//     }

//     private GameObject InstaniatePiece(AAttackingPiece piece, GameObject piecePrefab, Transform parent)
//     {
//         Vector3 position = _tilePos[piece.GetPos()];
//         position.z = -1;

//         var spawnedPiece = Instantiate(piecePrefab, position,
//         Quaternion.identity, parent);
//         spawnedPiece.AddComponent<PieceView>().Init(piece, _sprites);

//         _piecePos[piece.GetPos()] = position;

//         return spawnedPiece;
//     }

// }