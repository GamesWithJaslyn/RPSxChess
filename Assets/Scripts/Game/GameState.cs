using System;
using UnityEngine;
using UnityEngine.UI;
public enum GamePhase
{
    START,
    PLAYING,
    CHANGINGTYPE,
    GAMEOVER
}

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    private bool _isPlaying;
    private static GamePhase _currentPhase;
    [SerializeField] private GameObject _blueWinScreen;
    [SerializeField] private GameObject _redWinScreen;
    [SerializeField] private Button _rematch;
    [SerializeField] private Button _quit;

    public static Action<Team> OnGameWon;
    public static Action OnRematch;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _isPlaying = true; //might change o starting later

        _rematch.onClick.AddListener(() => Rematch());

        HideUI();

        _currentPhase = GamePhase.PLAYING;
        OnGameWon += Win;
    }

    public static void Win(Team team)
    {
        if (team.Equals(Team.Blue))
        {
            Instance._isPlaying = false;
            Instance._blueWinScreen.SetActive(true);
            Instance._redWinScreen.SetActive(false);

        }
        else
        {
            Instance._isPlaying = false;
            Instance._blueWinScreen.SetActive(false);
            Instance._redWinScreen.SetActive(true);
        }

        _currentPhase = GamePhase.GAMEOVER;
        Instance._rematch.gameObject.SetActive(true);
        Instance._quit.gameObject.SetActive(true);
    }

    public static bool StillPlaying()
    {
        return Instance._isPlaying;
    }

    public static Enum State()
    {
        return _currentPhase;
    }

    public static void SetState(GamePhase newPhase)
    {
        _currentPhase = newPhase;
    }

    public static void Rematch()
    {
        OnRematch?.Invoke();
        Instance.HideUI();
        SetState(GamePhase.PLAYING);
    }

    private void HideUI()
    {
        _blueWinScreen.SetActive(false);
        _redWinScreen.SetActive(false);
        _rematch.gameObject.SetActive(false);
        _quit.gameObject.SetActive(false);
    }


}
