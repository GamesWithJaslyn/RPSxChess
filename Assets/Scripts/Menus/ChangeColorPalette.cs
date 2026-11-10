using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorPalette : MonoBehaviour
{
    public Color_DataBase Color;
    BoardTilesView _boardTiles;
    List<TileView> _tileViews;
    List<PieceView> _pieceViews;
    IEnumerator Start()
    {
        _boardTiles = GameObject.FindGameObjectWithTag("Board")
                                .GetComponent<BoardTilesView>();

        while (_boardTiles.GetTileViews() == null ||
               _boardTiles.GetTileViews().Count == 0)
        {
            yield return null;
        }

        Debug.Log("BOARD FOUND");
        _tileViews = new List<TileView>();
        _tileViews = _boardTiles.GetTileViews();
        Debug.Log($"Tiles count: {_tileViews.Count}");


        _pieceViews = new List<PieceView>();
        _pieceViews = _boardTiles.GetPieceViews();

    }

    public void DefaultPalette()
    {
        Debug.Log("Default color");
        TileView.UpdatePalette(Color.Default_BaseTile,
            Color.Default_OffesetTile);

        foreach (TileView tile in _tileViews)
        {
            tile.UpdateType();
        }

        Debug.Log("Default color checking if it run");

        PieceView.UpdateTeamColor(Color.Default_BlueTeam,
        Color.Default_RedTeam);

        foreach (PieceView piece in _pieceViews)
        {
            piece.UpdateColor();
        }

        _boardTiles._boardView.UpdateBorderColor(Color.Default_BlueTeam, Color.Default_RedTeam);
        _boardTiles._boardView.Model.ChangeBorder();
    }

    public void OnlinePalette()
    {
        Debug.Log("Online color");

        TileView.UpdatePalette(Color.Online_BaseTile,
            Color.Online_OffesetTile);

        foreach (TileView tile in _tileViews)
        {
            tile.UpdateType();
        }

        PieceView.UpdateTeamColor(Color.Online_BlueTeam,
        Color.Online_RedTeam);

        foreach (PieceView piece in _pieceViews)
        {
            piece.UpdateColor();
        }

        _boardTiles._boardView.UpdateBorderColor(Color.Online_BlueTeam, Color.Online_RedTeam);
        _boardTiles._boardView.Model.ChangeBorder();
    }

    public void LocalPalette()
    {
        Debug.Log("local color");

        TileView.UpdatePalette(Color.Local_BaseTile,
            Color.Local_OffesetTile);

        foreach (TileView tile in _tileViews)
        {
            tile.UpdateType();
        }

        PieceView.UpdateTeamColor(Color.Local_BlueTeam,
        Color.Local_RedTeam);

        foreach (PieceView piece in _pieceViews)
        {
            piece.UpdateColor();
        }

        _boardTiles._boardView.UpdateBorderColor(Color.Local_BlueTeam, Color.Local_RedTeam);
        _boardTiles._boardView.Model.ChangeBorder();
    }

    public void OptionsPalette()
    {
        Debug.Log("options color");

        TileView.UpdatePalette(Color.Options_BaseTile,
            Color.Options_OffesetTile);

        foreach (TileView tile in _tileViews)
        {
            tile.UpdateType();
        }

        PieceView.UpdateTeamColor(Color.Options_BlueTeam,
        Color.Options_RedTeam);

        foreach (PieceView piece in _pieceViews)
        {
            piece.UpdateColor();
        }
        _boardTiles._boardView.UpdateBorderColor(Color.Options_BlueTeam, Color.Options_RedTeam);
        _boardTiles._boardView.Model.ChangeBorder();
    }

    public void QuitPalette()
    {
        Debug.Log("quit color");

        TileView.UpdatePalette(Color.Quit_BaseTile,
            Color.Quit_OffesetTile);

        foreach (TileView tile in _tileViews)
        {
            tile.UpdateType();
        }

        PieceView.UpdateTeamColor(Color.Quit_BlueTeam,
        Color.Quit_RedTeam);

        foreach (PieceView piece in _pieceViews)
        {
            piece.UpdateColor();
        }

        _boardTiles._boardView.UpdateBorderColor(Color.Quit_BlueTeam, Color.Quit_RedTeam);
        _boardTiles._boardView.Model.ChangeBorder();
    }
}
