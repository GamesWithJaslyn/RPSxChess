using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    private bool _isPlaying;
    [SerializeField] private GameObject _blueWinScreen;
    [SerializeField] private GameObject _redWinScreen;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _isPlaying = true;
        _blueWinScreen.SetActive(false);
        _redWinScreen.SetActive(false);
    }

    public static void Win(int team)
    {
        if (team > 0)
        {
            Debug.Log("Blue Won!");
            Instance._isPlaying = false;
            Instance._blueWinScreen.SetActive(true);
            
        }

        else if (team < 0)
        {
            Debug.Log("Red Won!");
            Instance._isPlaying = false;
            Instance._redWinScreen.SetActive(false);
        }
    }

    public static bool StillPlaying()
    {
        return Instance._isPlaying;
    }
}
