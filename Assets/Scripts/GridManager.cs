// using System.Collections.Generic;
// using UnityEngine;

// public class GridManager : MonoBehaviour
// {
//     public static GridManager gridManager;
//     [SerializeField] private int tileCountX, tileCountY;
//     [SerializeField] private ITileModel waterTile, regularTile;
//     [SerializeField] private Transform cam;
   

//     [Header("Prefabs")]
//     [SerializeField] private GameObject[] prefabs;


//     public RPSC_Piece[,] rpscPieces;
//     private Dictionary<Vector2, Tile> tiles;
//     private GameObject[,] positions;

//     void Awake()
//     {
//         gridManager = this;
//         positions = new GameObject[tileCountX, tileCountY];
//         SpawnAllPiece();
//         PositionAllPieces();
//     }

//     //Generates Grid
//     public void GenerateGrid()
//     {
//         if(!cam)
//         {
//             cam = Camera.main.transform;
//             return;
//         }
        
//         tiles = new Dictionary<Vector2, Tile>();

//         //regular tiles
//          for(int x = 0; x < tileCountX; x++) {
//             for (int y = 0; y < tileCountY; y++) {
//                 var spawnedTile = Instantiate(regularTile, new Vector3(x, y), 
//                 Quaternion.identity);
//                  spawnedTile.name = $"Tile({x}, {y})"; 
//                 //is x even AND y not even
//                 // OR
//                 //is x not even AND y is even
//                  var isOffset = (x % 2 == 0 && y % 2 != 0 
//                  || x % 2 != 0 && y % 2 == 0);
//                  spawnedTile.Init(x, y);

//                  tiles[new Vector2(x, y)] = spawnedTile;
//             }
//         }

//         if(tileCountX == 16)
//         {
//             //leftmost waterbed
//             for(int x = 1; x < 4; x++) {
//                 for (int y = 3; y < 6; y++) {
//                     GenerateRandWater(x, y);
//                 }
//             }


//             //left-middle waterbed
//             for(int x = 5; x < 8; x++) {
//                 for (int y = 3; y < 6; y++) {
//                     GenerateRandWater(x, y);
//                 }
//             }

//             //right-middle waterbed
//             for(int x = 8; x < 11; x++) {
//                 for (int y = 3; y < 6; y++) {
//                     GenerateRandWater(x, y);
//                 }
//             }

//             //rightmost waterbed
//             for(int x = 12; x < 15; x++) {
//                 for (int y = 3; y < 6; y++) {
//                     GenerateRandWater(x, y);
//                 }
//             }
//         }
//         else
//         {
//             //leftmost waterbed
//             for(int x = 0; x < 3; x++) {
//                 for (int y = 3; y < 6; y++) {
//                     GenerateRandWater(x, y);
//                 }
//             }


//             //middle waterbed
//             for(int x = 4; x < 8; x++) {
//                 for (int y = 3; y < 6; y++) {
//                     GenerateRandWater(x, y);
//                 }
//             }

//             //rightmost waterbed
//             for(int x = 9; x < 12; x++) {
//                 for (int y = 3; y < 6; y++) {
//                     GenerateRandWater(x, y);
//                 }
//             }
//         }

//             //setting camera position to be in the middle of the grid
//             cam.transform.position = 
//             new Vector3((float)tileCountX/2 - 0.5f, 
//             (float)tileCountY/2 - 0.5f, -10);
            
//             GameManager.gameManager.ChangeState(GameManager.GameState.SpawnPlayerOne);

//     }
//     public void GenerateRandWater(int x, int y) 
//     {
//         if (Random.Range(0, 6) < 3) 
//         {
//             var spawnedWaterTile = Instantiate(waterTile, new Vector3(x, y), Quaternion.identity);

//             spawnedWaterTile.name = $"Water Tile({x}, {y})"; 

//             spawnedWaterTile.Init(x,y);

//         }
//     }


//     //spawing all the pieces
//     private void SpawnAllPiece() 
//     {
//         int XThird = (int)tileCountX/3;
//         int XTTwoThirds = (int)(XThird + XThird);
//         int YMinusOne = tileCountY - 1;


//         rpscPieces = new RPSC_Piece[tileCountX, tileCountY];
//         int blueTeam = 0;
//         int redTeam = 1;

//         //blue team
//         for (int i = 0; i < XThird; i++)
//             for(int j = 0; j < 1; j++)
//                 rpscPieces[i,j] = SpawnSinglePiece(PieceType.BlueBow, blueTeam);

//          for (int i = XThird; i < XTTwoThirds; i++)
//             for(int j = 0; j < 1; j++)
//                 rpscPieces[i,j] = SpawnSinglePiece(PieceType.BlueSword, blueTeam);

//          for (int i = XTTwoThirds; i < tileCountX; i++)
//             for(int j = 0; j < 1; j++)
//                 rpscPieces[i,j] = SpawnSinglePiece(PieceType.BluePegasus, blueTeam);

//         //red team
//         for (int i = 0; i < XThird; i++)
//             for(int j = YMinusOne; j > YMinusOne - 1; j--)
//                 rpscPieces[i,j] = SpawnSinglePiece(PieceType.RedBow, redTeam);

//          for (int i = XThird; i < XTTwoThirds; i++)
//             for(int j = YMinusOne; j > YMinusOne - 1; j--)
//                 rpscPieces[i,j] = SpawnSinglePiece(PieceType.RedSword, redTeam);

//          for (int i = XTTwoThirds; i < tileCountX; i++)
//             for(int j = YMinusOne; j > YMinusOne - 1; j--)
//                 rpscPieces[i,j] = SpawnSinglePiece(PieceType.RedPegasus, redTeam);
//     }
//     private RPSC_Piece SpawnSinglePiece(PieceType type, int team)
//     {
//         RPSC_Piece piece = Instantiate
//         (prefabs[(int)type - 1], transform).GetComponent<RPSC_Piece>();

//         piece.type = type;
//         piece.team = team;

//         return piece;
//     }
      

//     //Positioning Pieces
//     private void PositionAllPieces()
//     {
//         for (int x = 0; x < tileCountX; x++)
//             for(int y = 0; y < tileCountY; y++) {
//                 if(rpscPieces[x, y] != null) {
//                     PositionSinglePiece(x, y, true);
//                 }
//             }
//     }

//     public void PositionSinglePiece(int x, int y, bool force = false)
//     {
//         rpscPieces[x, y].currentX = x;
//         rpscPieces[x, y].currentY = y;
//         rpscPieces[x, y].transform.position = new Vector3(x, y);
//     }

//      public void SetPosition(GameObject obj) {
//     RPSC_Piece rpsc = obj.GetComponent<RPSC_Piece>();

//     positions[rpsc.currentX, rpsc.currentY] = obj;
//    }

//    public void SetPositionEmpty(int x, int y)
//    {
//         positions[x, y] = null;
//    }

//    public GameObject GetPosition(int x, int y)
//    {
//         return positions[x, y];
//    }

//    public bool PositionOnBoard(int x, int y)
//    {
//         if(x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
//             return false;

//         return true;
//    }
// }
