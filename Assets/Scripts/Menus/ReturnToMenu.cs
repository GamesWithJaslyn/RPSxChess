using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    public void OnClick()
    {
        SwitchScene.LoadSceneByName("Start Menu");
    }
}
