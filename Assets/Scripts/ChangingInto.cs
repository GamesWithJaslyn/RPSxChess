using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingInto : MonoBehaviour
{
    [SerializeField] private GameObject _obj;
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private List<Sprite> _images;
    private Canvas _canvas;
    private IPieceModel _model;
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

        _model = _currentPiece.GetComponent<PieceView>().GetModel();

        GameState.SetState(GamePhase.CHANGINGTYPE);
    }

    public void BowClicked()
    {
        Debug.Log("[Changing Into] - Bow Clicked");
        if(_model.GetPieceType() != 1 && _model.GetPieceType() > 0)
        {
            _currentPiece.GetComponent<SpriteRenderer>().sprite = _images[0];
            _model.ChangeInto(1);
        }
        else if (_model.GetPieceType() != -1 && _model.GetPieceType() < 0)
        {
            _currentPiece.GetComponent<SpriteRenderer>().sprite = _images[3];
            _model.ChangeInto(-1);
        }

        ResetButton();
    }

    public void SwordClicked()
    {
        Debug.Log("[Changing Into] - Sword Clicked");
        if(_model.GetPieceType() != 2 && _model.GetPieceType() > 0)
        {
            _currentPiece.GetComponent<SpriteRenderer>().sprite = _images[1];
            _model.ChangeInto(2);
        }
        else if (_model.GetPieceType() != -2 && _model.GetPieceType() < 0)
        {
            _currentPiece.GetComponent<SpriteRenderer>().sprite = _images[4];
            _model.ChangeInto(-2);
        }

        ResetButton();
    }

    public void PegasusClicked()
    {
        Debug.Log("[Changing Into] - Pegasus Clicked");
        if(_model.GetPieceType() != 3 && _model.GetPieceType() > 0)
        {
            _currentPiece.GetComponent<SpriteRenderer>().sprite = _images[2];
            _model.ChangeInto(3);
        }
        else if (_model.GetPieceType() != -3 && _model.GetPieceType() < 0)
        {
            _currentPiece.GetComponent<SpriteRenderer>().sprite = _images[5];
            _model.ChangeInto(-3);
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
        foreach(Button but in _buttons)
        {
            but.gameObject.SetActive(value);
        }
    }
}
