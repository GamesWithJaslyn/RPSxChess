using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardTilesView : MonoBehaviour
{
    public IBoardModel _boardModel;
    public static Dictionary<int, Vector3> TilePos;
    private Dictionary<int, Vector3> _piecePos;

    [Header("Containers")]
    [SerializeField] private Transform TilesParent;
    [SerializeField] private Transform PiecesParent;


    [Header("Tile Prefab:")]
    [SerializeField] private GameObject _tile;


    [Header("Pieces Prefab:")]
    [SerializeField] private GameObject _piecePrefab;
    [SerializeField] private List<Sprite> _pieceTypes;

    [Header("Board")]
    [SerializeField] private SpriteRenderer Border;


    void Start()
    {
        TilePos = new Dictionary<int, Vector3>();
        _piecePos = new Dictionary<int, Vector3>();

        gameObject.AddComponent<BoardView>().Init(Border, new BoardModelImpl());

        _boardModel = gameObject.GetComponent<BoardView>().Model;
        _piecePrefab.transform.localScale = _tile.transform.localScale;

        InitializeTiles();
        InitializePieces();
    }

    private void InitializeTiles()
    {
        float j = 0;
        float y = 0;

        float change = _tile.transform.localScale.x;

        for (int x = 0; x < _boardModel.GetAllTiles().Count; x++)
        {
            if (x % 11 == 0 && x != 0)
            {
                j = 0;
                y -= change;
            }
            var spawnedTile = Instantiate(_tile, new Vector3(j, y),
            Quaternion.identity, TilesParent);
            spawnedTile.name = $"Tile({j}, {y})";
            spawnedTile.AddComponent<TileView>().Init(_boardModel.GetAllTiles()
            .Find(t => t.ID == x), _boardModel);

            TilePos[x] = new Vector3(j, y);
            j += change;
        }

        Camera.main.transform.position = new Vector3((float)j / 2 - 0.5f, (float)y / 2, -10);
    }

    private void InitializePieces()
    {
        Dictionary<IPieceModel, GameObject> pieceKey = new Dictionary<IPieceModel, GameObject>();

        foreach (IPieceModel piece in _boardModel.GetAllPieces())
        {
            ITileModel tile = _boardModel.GetAllTiles().Find(tile => tile.ID == piece.Position);
            tile.EnterPiece(piece);
            pieceKey.Add(piece, InstaniatePiece(piece, _piecePrefab));
        }

        _boardModel.SetPiecesDictionary(pieceKey);
    }

    private GameObject InstaniatePiece(IPieceModel piece, GameObject piecePrefab)
    {
        Vector3 position = TilePos[piece.Position];
        position.z = -1;

        var spawnedPiece = Instantiate(piecePrefab, position,
        Quaternion.identity, PiecesParent);
        spawnedPiece.AddComponent<PieceView>().Init(piece);

        _piecePos[piece.Position] = position;
        spawnedPiece.name = piece.Team.ToString() + "_" + piece.PieceType.ToString();

        return spawnedPiece;
    }

}