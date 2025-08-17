using UnityEngine;

public class BoardTilesPresenter : MonoBehaviour
{
    private IBoardModel _boardModel;
    [SerializeField] private GameObject _regularTile;
    [SerializeField] private Transform _cam;


    void Start()
    {
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

            //tiles[new Vector2(x, y)] = spawnedTile;
            j++;

        }

        _cam.transform.position = new Vector3((float)j / 2 - 0.5f, (float)y / 2, -10);
    }

    private void InitializePieces()
    {
        return;
    }

}