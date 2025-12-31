using System;
using UnityEngine;
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

    public static Action<int> OnGameWon;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _isPlaying = true; //might change o starting later
        _blueWinScreen.SetActive(false);
        _redWinScreen.SetActive(false);
        _currentPhase = GamePhase.PLAYING;
        OnGameWon += Win;
    }

    public static void Win(int team)
    {
        if (team > 0)
        {
            Debug.Log("Blue Won!");
            Instance._isPlaying = false;
            Instance._blueWinScreen.SetActive(true);
            Instance._redWinScreen.SetActive(false);

        }

        else if (team < 0)
        {
            Debug.Log("Red Won!");
            Instance._isPlaying = false;
            Instance._blueWinScreen.SetActive(false);
            Instance._redWinScreen.SetActive(true);
        }

        _currentPhase = GamePhase.GAMEOVER;
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
}
