using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardTilesPresenter_V2 : MonoBehaviour
{
    private IBoardModel_V2 _boardModel;
    private Dictionary<int, Vector3> _tilePos;
    private Dictionary<int, Vector3> _piecePos;

    [Header("Containers")]
    [SerializeField] private Transform TilesParent;
    [SerializeField] private Transform PiecesParent;



    [Header("Tile Prefab:")]
    [SerializeField] private GameObject _tile;

    [Header("Pieces Prefab:")]
    [SerializeField] private GameObject _piecePrefab;
    [SerializeField] private List<Sprite> _pieceTypes;


    void Start()
    {
        _tilePos = new Dictionary<int, Vector3>();
        _piecePos = new Dictionary<int, Vector3>();

        _boardModel = new BoardModelImpl_V2();
        InitializeTiles();
        InitializePieces();
    }

    private void InitializeTiles()
    {
        int j = 0;
        int y = 0;

        for (int x = 0; x < _boardModel.GetAllTiles().Count; x++)
        {
            if (x % 11 == 0 && x != 0)
            {
                j = 0;
                y--;
            }
            var spawnedTile = Instantiate(_tile, new Vector3(j, y),
            Quaternion.identity, TilesParent);
            spawnedTile.name = $"Tile({j}, {y})";
            spawnedTile.AddComponent<TileView_V2>().Init(_boardModel.GetAllTiles()
            .Find(t => t.ID == x), _boardModel);

            _tilePos[x] = new Vector3(j, y);
            j++;
        }

        Camera.main.transform.position = new Vector3((float)j / 2 - 0.5f, (float)y / 2, -10);
    }

    private void InitializePieces()
    {
        Dictionary<IPieceModel_V2, GameObject> pieceKey =
        new Dictionary<IPieceModel_V2, GameObject>();

        foreach (IPieceModel_V2 piece in _boardModel.GetAllPieces())
        {
            ITileModel tile = _boardModel.GetAllTiles().Find(tile => tile.ID == piece.Position);
            tile.Occupant = piece;
            pieceKey.Add(piece, InstaniatePiece(piece, _piecePrefab));
        }
        _boardModel.SetPiecesDictionary(pieceKey);
    }

    private GameObject InstaniatePiece(IPieceModel_V2 piece, GameObject piecePrefab)
    {
        Vector3 position = _tilePos[piece.Position];
        position.z = -1;

        var spawnedPiece = Instantiate(piecePrefab, position,
        Quaternion.identity, PiecesParent);
        spawnedPiece.AddComponent<PieceView_V2>().Init(piece);

        _piecePos[piece.Position] = position;
        spawnedPiece.name = piece.Team.ToString() + "_" + piece.PieceType.ToString();

        return spawnedPiece;
    }

}