using Mono.CSharp;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    public IBoardModel Model { get; private set; }
    private SpriteRenderer _border;
    private Color32 _blueBorder;
    private Color32 _redBorder;

    public void Init(SpriteRenderer border, IBoardModel boardModel, Color_DataBase Color)
    {
        _border = border;
        Model = boardModel;

        UpdateBorderColor(Color.Default_BlueTeam, Color.Default_RedTeam);
        ChangeColor(true);
        Model.OnChangeTurn += ChangeColor;
        GameState.OnRematch += Reset;

    }

    public void UpdateBorderColor(Color32 blueTeam, Color32 redTeam)
    {
        _blueBorder = blueTeam;
        _redBorder = redTeam;
    }

    public void ChangeColor(bool isBlueTurn)
    {
        _border.color = isBlueTurn ? _blueBorder : _redBorder;
    }

    public void Reset()
    {
        _border.color = _blueBorder;
    }

    public void OnDisable()
    {
        if (Model != null)
        {
            Model.OnChangeTurn -= ChangeColor;
            GameState.OnRematch -= Reset;

        }
    }
}