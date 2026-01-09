using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingInto : MonoBehaviour
{
    [SerializeField] private GameObject _obj;
    [SerializeField] private List<Button> _buttons;
    private Canvas _canvas;
    private IPieceModel_V2 _model;
    private GameObject _currentPiece;
    private static ChangingInto _instance;

    public static ChangingInto Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _obj.SetActive(false);

        _buttons[0].onClick.AddListener(() => BowClicked());
        _buttons[1].onClick.AddListener(() => SwordClicked());
        _buttons[2].onClick.AddListener(() => PegasusClicked());
    }

    public void ShowChangeOptions(GameObject model)
    {
        _obj.SetActive(true);
        AreButtonsActive(true);
        _currentPiece = model;

        _model = _currentPiece.GetComponent<PieceView_V2>().Model;

        GameState.SetState(GamePhase.CHANGINGTYPE);
    }

    public void BowClicked()
    {
        Debug.Log("[Changing Into] - Bow Clicked");
        if (_model.PieceType == PieceType.Bow)
        {
            _currentPiece.GetComponent<SpriteRenderer>().sprite =
            PieceImageDataBase.PieceSprites[0];
            _model.ChangeInto(PieceType.Bow);
        }

        ResetButton();
    }

    public void SwordClicked()
    {
        Debug.Log("[Changing Into] - Bow Clicked");
        if (_model.PieceType == PieceType.Sword)
        {
            _currentPiece.GetComponent<SpriteRenderer>().sprite =
            PieceImageDataBase.PieceSprites[0];
            _model.ChangeInto(PieceType.Sword);
        }

        ResetButton();
    }

    public void PegasusClicked()
    {
        Debug.Log("[Changing Into] - Bow Clicked");
        if (_model.PieceType == PieceType.Pegasus)
        {
            _currentPiece.GetComponent<SpriteRenderer>().sprite =
            PieceImageDataBase.PieceSprites[0];
            _model.ChangeInto(PieceType.Pegasus);
        }

        ResetButton();
    }


    public void ResetButton()
    {
        AreButtonsActive(false);
        GameState.SetState(GamePhase.PLAYING);

    }

    public void AreButtonsActive(bool value)
    {
        foreach (Button but in _buttons)
        {
            but.gameObject.SetActive(value);
        }
    }
}
