using System.Collections.Generic;
using UnityEngine;

public class BoardTilesPresenter : MonoBehaviour
{
    private IBoardModel _boardModel;
    private Dictionary<int, Vector3> _tilePos;
    private Dictionary<int, ITileModel> _tileList;

    [SerializeField] private GameObject _regularTile;
    [SerializeField] private Transform _cam;

    [Header("Pieces Prefab:")]
    [SerializeField] private GameObject _blueBow;
    [SerializeField] private GameObject _blueSword;
    [SerializeField] private GameObject _bluePegasus;
    [SerializeField] private GameObject _redBow;
    [SerializeField] private GameObject _redSword;
    [SerializeField] private GameObject _redPegasus;

    void Start()
    {
        _tilePos = new Dictionary<int, Vector3>();
        _boardModel = new BoardModelImpl();
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        InitializeTiles();
        InitializePieces();
    }

    private void InitializeTiles()
    {
        if (!_cam)
        {
            _cam = Camera.main.transform;
            return;
        }

        int j = 0;
        int y = 0;
        //regular tiles
        for (int x = 0; x < _boardModel.GetAllTiles().Count; x++)
        {
            if (x % 11 == 0 && x != 0)
            {
                j = 0;
                y--;
            }
            var spawnedTile = Instantiate(_regularTile, new Vector3(j, y),
            Quaternion.identity);
            spawnedTile.name = $"Tile({j}, {y})";
            spawnedTile.AddComponent<TilePresenter>().Init(_boardModel.GetAllTiles()
            .Find(tile => tile.GetID() == x));
            //is x even AND y not even
            // OR
            //is x not even AND y is even
            //  var isOffset = (x % 2 == 0 && y % 2 != 0 
            //  || x % 2 != 0 && y % 2 == 0);
            //  spawnedTile.Init(x, y);

            _tilePos[x] = new Vector3(j, y);
            j++;

        }

        _cam.transform.position = new Vector3((float)j / 2 - 0.5f, (float)y / 2, -10);
    }

    private void InitializePieces()
    {

        foreach (AAttackingPiece piece in _boardModel.GetAllPieces())
        {
            GameObject prefab;

            if (piece is Bow)
            {
                if (piece.GetPieceType() == 1)
                {
                    Debug.Log("Instantiate Blue Bow");
                    prefab = _blueBow;
                }
                else
                {
                    Debug.Log("Instantiate Red Bow");
                    prefab = _redBow;
                }
            }

            else if (piece is Sword)
            {
                if (piece.GetPieceType() == 2)
                {
                    Debug.Log("Instantiate Blue Sword");
                    prefab = _blueSword;
                }
                else
                {
                    Debug.Log("Instantiate Red Sword");
                    prefab = _redSword;
                }
            }

            else if (piece is Pegasus)
            {
                if (piece.GetPieceType() == 3)
                {
                    Debug.Log("Instantiate Blue Pegasus");
                    prefab = _bluePegasus;
                }
                else
                {
                    Debug.Log("Instantiate Red Pegasus");
                    prefab = _redPegasus;
                }
            }
            else
            {
                prefab = null;
                Debug.Log("Incorrect Piece");
            }

            IEnterAndLeave tile = _boardModel.GetAllEnterableTiles()
            .Find(tile => tile.GetID() == piece.GetPos());
            tile.Enter(piece);

            InstaniatePiece(piece, prefab);
        }

    }

    private GameObject InstaniatePiece(AAttackingPiece piece, GameObject piecePrefab)
    {
        Vector3 position = _tilePos[piece.GetPos()];

        var spawnedPiece = Instantiate(piecePrefab, position,
        Quaternion.identity);
        spawnedPiece.AddComponent<PiecePresenter>().Init(_boardModel.GetAllPieces()
            .Find(findingPiece => findingPiece.GetPos() == piece.GetPos()));
        return spawnedPiece;
    }

}