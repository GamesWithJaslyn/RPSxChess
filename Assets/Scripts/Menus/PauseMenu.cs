using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseButton;

    void Start()
    {
        ShowPauseMenu(false);
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
        SwitchScene.LoadSceneByName("MainMenu");
    }

    private void ShowPauseMenu(bool t)
    {
        _pauseMenu.SetActive(t);
        _pauseButton.SetActive(!t);
    }
}
