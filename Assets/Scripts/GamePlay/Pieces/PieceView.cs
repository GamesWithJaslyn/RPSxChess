using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a Piece in the RPSxChess game.
/// This class is responsible for the visual representation of a piece in the game.
/// It handles the movement of the piece in the game world.
/// </summary>
public class PieceView : MonoBehaviour
{
    public IPieceModel Model { get; private set; }
    [SerializeField] private Color _teamColor;
    private Transform _color;
    private Transform _pieceType;

    public void Init(IPieceModel model)
    {
        _color = gameObject.transform.Find("Color");
        _pieceType = gameObject.transform.Find("Type");

        Model = model;
        UpdateType();
        AddEvents();

        if (Model.Team == Team.Blue)
        {
            _color.GetComponent<SpriteRenderer>().color = PieceImageDataBase.BlueTeamColor;
        }
        else
        {
            _color.GetComponent<SpriteRenderer>().color = PieceImageDataBase.RedTeamColor;
        }

    }

    public void SetDead()
    {
        SettingActive(false);
    }
    //private void OnDestroy() { RemoveEvents(); }
    private void OnDisable()
    {
        if (Model != null)
        {
            RemoveEvents();
            SettingActive(false);
        }

    }

    public void UpdatePosition(int pos)
    {
        transform.position = BoardTilesView.TilePos[Model.Position];

    }

    public void UpdateType()
    {
        switch (Model.PieceType)
        {
            case PieceType.Bow:
                _pieceType.GetComponent<SpriteRenderer>().sprite =
                PieceImageDataBase.PieceSprites[0];
                break;
            case PieceType.Sword:
                _pieceType.GetComponent<SpriteRenderer>().sprite =
                PieceImageDataBase.PieceSprites[1];
                break;
            case PieceType.Pegasus:
                _pieceType.GetComponent<SpriteRenderer>().sprite =
                PieceImageDataBase.PieceSprites[2];
                break;
        }
    }

    public void Reset()
    {
        SettingActive(true);
        AddEvents();
        UpdatePosition(Model.Position);
        UpdateType();
    }

    private void SettingActive(bool isItTrue)
    {
        _color.gameObject.SetActive(isItTrue);
        _pieceType.gameObject.SetActive(isItTrue);
        //this.enabled = isItTrue;
    }

    private void AddEvents()
    {
        Model.OnMoved += UpdatePosition;
        Model.OnDeath += SetDead;
        Model.OnChangeInto += UpdateType;
        Model.OnReset += Reset;
    }

    private void RemoveEvents()
    {
        Model.OnMoved -= UpdatePosition;
        Model.OnDeath -= SetDead;
        Model.OnChangeInto -= UpdateType;
        Model.OnReset -= Reset;
    }
}
