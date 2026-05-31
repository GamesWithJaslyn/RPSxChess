using System.Collections;
using Mono.CSharp;
using UnityEngine;

public class ArrowTest : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _image;
    private float _speed = 10f;
    private bool _on;
    private bool _shot;
    void Start()
    {
        _on = false;
        _shot = false;
        _image.enabled = _on;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _on = !_on;
            Debug.Log($"[ArowTest] - _on is {_on}");
        }

        _image.enabled = _on;

        if (_on)
        {
            if (!_shot)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector2 direction = (mousePos - transform.position);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }



            if (Input.GetMouseButtonDown(0))
            {
                ShootArrow(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            }
        }



    }

    private void ShootArrow(Vector3 target)
    {
        StartCoroutine(FlyTowardTarget(target));
    }

    private void HitTarget(Vector3 target)
    {
        StopCoroutine(FlyTowardTarget(target));
        ReturnToPiecePos();
    }

    public IEnumerator FlyTowardTarget(Vector3 mouse)
    {
        Vector3 target = new Vector3(mouse.x, mouse.y, -1f);
        _shot = true;
        while (transform.position != target)
        {
            // Move towards the target's position
            float step = _speed * Time.deltaTime; // Calculate distance to move this frame

            transform.position = Vector3.MoveTowards(transform.position, target, step);
            // Wait until the next frame before running the loop again
            yield return new WaitForSeconds(0.001f);
        }

        transform.localPosition = new Vector3(0, 0, -1f);
        _on = false;
        _shot = false;

    }

    private void ReturnToPiecePos()
    {
        transform.localPosition = new Vector3(0, 0, -1f);
    }

}
