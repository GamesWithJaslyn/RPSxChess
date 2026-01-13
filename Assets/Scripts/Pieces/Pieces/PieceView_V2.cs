using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a Piece in the RPSxChess game.
/// This class is responsible for the visual representation of a piece in the game.
/// It handles the movement of the piece in the game world.
/// </summary>
public class PieceView_V2 : MonoBehaviour
{
    public IPieceModel_V2 Model { get; private set; }
    [SerializeField] private Color _teamColor;

    private Transform _color;
    private Transform _pieceType;

    public void Init(IPieceModel_V2 model)
    {
        _color = gameObject.transform.Find("Color");
        _pieceType = gameObject.transform.Find("Type");

        Model = model;
        UpdateType();

        Model.OnMoved += UpdatePosition;
        Model.OnDeath += SetDead;
        Model.OnChangeInto += UpdateType;
        Model.OnReset += Reset;

        if (Model.Team == Team.Blue)
        {
            _color.GetComponent<SpriteRenderer>().color = new Color32(40, 96, 161, 255);
        }
        else
        {
            _color.GetComponent<SpriteRenderer>().color = new Color32(158, 21, 15, 255);
        }

    }

    public void SetDead() { gameObject.SetActive(false); }
    //private void OnDestroy() { RemoveEvents(); }
    private void OnDisable() { if (Model != null) RemoveEvents(); }

    public void UpdatePosition(int pos)
    {
        transform.position = new Vector3(pos % 11, -1 * (pos / 11), -1);
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
        gameObject.SetActive(true);
        UpdatePosition(Model.Position);
        UpdateType();
    }

    private void RemoveEvents()
    {
        Model.OnMoved -= UpdatePosition;
        Model.OnDeath -= SetDead;
        Model.OnChangeInto -= UpdateType;
        Model.OnReset -= Reset;
    }
}
