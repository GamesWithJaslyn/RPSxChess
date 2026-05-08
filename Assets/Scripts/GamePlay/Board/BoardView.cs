using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    public IBoardModel Model { get; private set; }
    private SpriteRenderer _border;

    private Color32 _blueBorder;
    private Color32 _redBorder;

    public void Init(SpriteRenderer border, IBoardModel boardModel)
    {
        _border = border;
        Model = boardModel;

        _blueBorder = new Color32(0, 51, 143, 255);
        _redBorder = new Color32(90, 12, 12, 255);

        _border.color = _blueBorder;
        Model.OnChangeTurn += ChangeTurnColor;
        GameState.OnRematch += Reset;

    }

    public void ChangeTurnColor(bool isBlueTurn)
    {
        if (isBlueTurn)
        {
            _border.color = _blueBorder;
        }
        else
        {
            _border.color = _redBorder;
        }
    }

    public void Reset()
    {
        _border.color = _blueBorder;
    }

    public void OnDisable()
    {
        if (Model != null)
        {
            Model.OnChangeTurn -= ChangeTurnColor;
            GameState.OnRematch -= Reset;

        }
    }
}