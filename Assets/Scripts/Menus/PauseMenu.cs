using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _changeColors;
    [SerializeField] private ChangeColorPalette Color;

    void Start()
    {
        ShowPauseMenu(false);
        ShowColorOptions(false);
    }

    public void PressPauseButton()
    {
        ShowPauseMenu(true);
        GameState.SetState(GamePhase.PAUSE);
    }

    public void PressResume()
    {
        ShowPauseMenu(false);
        GameState.SetState(GamePhase.PLAYING);
    }

    public void PressReset()
    {
        GameObject.FindGameObjectWithTag("Board").GetComponent<BoardTilesView>()._boardModel.ResetBoard();
        ShowPauseMenu(false);
        GameState.Rematch();
    }

    public void PressQuitMatch()
    {
        SwitchScene.LoadSceneByName("Start Menu");
    }

    private void ShowPauseMenu(bool t)
    {
        _pauseMenu.SetActive(t);
        _pauseButton.SetActive(!t);
    }

    private void ShowColorOptions(bool t)
    {
        _changeColors.SetActive(t);
    }

    public void PressChangeColor()
    {
        ShowColorOptions(true);
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(false);
    }


    public void ClickedDefaultPalette()
    {
        Color.DefaultPalette();
        ShowPauseMenu(true);
        ShowColorOptions(false);
    }

    public void ClickedLocalPalette()
    {
        Color.LocalPalette();
        ShowPauseMenu(true);
        ShowColorOptions(false);
    }

    public void ClickedOnlinePalette()
    {
        Color.OnlinePalette();
        ShowPauseMenu(true);
        ShowColorOptions(false);
    }

    public void ClickedOptionsPalette()
    {
        Color.OptionsPalette();
        ShowPauseMenu(true);
        ShowColorOptions(false);
    }

    public void ClickedQuitPalette()
    {
        Color.QuitPalette();
        ShowPauseMenu(true);
        ShowColorOptions(false);
    }
}
