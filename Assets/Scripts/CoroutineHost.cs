using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHost : MonoBehaviour
{
    [SerializeField] public ChangingInto _changeInto;
    private static CoroutineHost _instance;

    public static CoroutineHost Instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        if(_changeInto == null)
        {
            _changeInto = ChangingInto.Instance;
            if(_changeInto == null)
            {
                Debug.LogError("[Coroutine Host] - _changeInto is STILL null after assignment attempt");
            }
        }

        if (_instance == null)
        {
            GameObject host = new GameObject("CoroutineHost");
            _instance = host.AddComponent<CoroutineHost>();
            DontDestroyOnLoad(host);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("[Coroutine Host] - _instance is not null");
            //Destroy(this.gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // public void Starting_ButtonPress()
    // {
    //     Debug.Log("[Coroutine Host] - Started wait for buttons");
    //     StartCoroutine(_changeInto.WaitingForButtionPress());
    // }

    // public void Waiting_ButtonPress()
    // {
    //     Debug.Log("[Coroutine Host] - ButtonWasPressed");
    //     StopCoroutine(_changeInto.WaitingForButtionPress());
    // }
}
