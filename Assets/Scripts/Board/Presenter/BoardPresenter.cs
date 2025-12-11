using UnityEngine;

public class BoardPresenter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Debug.Log("Mouse pos:" + mouseScreenPos);
    }
}
