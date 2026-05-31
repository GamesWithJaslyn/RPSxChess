using System;
using System.Collections;
using System.Threading.Tasks;
using Mono.CSharp;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _image;
    public PieceView _pieceView;
    private AAttackingPiece _pieceModel;
    private float _speed = 7f;
    private bool _arrowShot;

    public event Action<bool> OnArrowReachedTarget;
    void Start()
    {
        TurnArrowOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pieceModel != null)
        {
            if (_pieceModel is AAttackingPiece attack && !_arrowShot)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector2 direction = (mousePos - transform.position);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }

    public void SetPieceViewAndModel(PieceView view, AAttackingPiece model)
    {
        _pieceView = view;
        _pieceModel = model;
        _pieceModel.OnShowArrow += TurnArrowOn;
        _pieceModel.OnHideArrow += TurnArrowOff;
        _pieceModel.OnArrowShot += ShootArrow;
    }


    private async Task ShootArrow(IPieceModel target)
    {
        _arrowShot = true;
        await FlyTowardTarget(target);
        transform.localPosition = new Vector3(0, 0, -1f);
        TurnArrowOff();
    }

    private async Task<bool> FlyTowardTarget(IPieceModel targetPiece)
    {
        Vector3 Pos = BoardTilesView.TilePos[targetPiece.Position];
        Vector3 targetPos = new Vector3(Pos.x, Pos.y, -1f);

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            await Task.Yield();
        }

        transform.position = targetPos;
        OnArrowReachedTarget?.Invoke(true);
        _pieceModel?.CallArrowHitTarget(targetPiece);

        return true;

    }


    public void TurnArrowOn()
    {
        _image.enabled = true;
        _arrowShot = false;
    }

    public void TurnArrowOff()
    {
        _image.enabled = false;
    }

    private void OnDisable()
    {
        _pieceModel.OnShowArrow -= TurnArrowOn;
        _pieceModel.OnHideArrow -= TurnArrowOff;
    }
}
