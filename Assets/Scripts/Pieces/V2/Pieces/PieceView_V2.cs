// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UI;

// /// <summary>
// /// Represents a Piece in the RPSxChess game.
// /// This class is responsible for the visual representation of a piece in the game.
// /// It handles the movement of the piece in the game world.
// /// </summary>
// public class PieceView_V2 : MonoBehaviour
// {
//     public IPieceModel_V2 _model { get; private set; }
//     [SerializeField] private int _pos;
//     [SerializeField] private PieceType _type;
//     [SerializeField] private int _targetType;
//     [SerializeField] private List<Sprite> _images;

//       public void Init(IPieceModel_V2 model, List<Sprite> images)
//     {
//         _model = model;
//         _images = images;
//         _pos = model.GetPos();
//         _type = model.GetPieceType();
//         _model.OnMoved += UpdatePosition;
//         _model.OnDeath += SetDead;
//         _model.OnChangeInto += ChangeType;

//         if(model is AAttackingPiece attack)
//         {
//             _targetType = attack.GetTargetType();
//         }

//     }

//     public void UpdatePosition(int pos)
//     {
//         Vector3 position = new Vector3(pos % 11, -1 * (pos / 11), -1);
//         _pos = _model.GetPos();
//         transform.position = position;
//     }

//     public void ChangeType(PieceType changeInto)
//     {
//         Debug.Log("[Piece View] - Changing Type to: " + changeInto);
//         ChangeSprite(changeInto);

//         _model.GetBoardModel().RemovePiece(_model); //removing old piece from board
//         _model = ChangePieceClass(changeInto); //changing the piece class
//         _model.GetBoardModel().AddPiece(_model); //adding new piece to board
//         UpdatingModel();
//     }

//     private void UpdatingModel()
//     {
//         _model.PromotionMade();
//         _type = _model.GetPieceType();
//         _targetType = (_model as AAttackingPiece).GetTargetType();

//         _model.OnMoved += UpdatePosition;
//         _model.OnDeath += SetDead;
//         _model.OnChangeInto += ChangeType;
//     }

//     private void ChangeSprite(PieceType changeInto)
//     {
//         // if(_type > 0)
//         // {
//         //     gameObject.GetComponent<SpriteRenderer>().sprite = _images[changeInto - 1];
//         // }
//         // else if (_type < 0)
//         // {
//         //     gameObject.GetComponent<SpriteRenderer>().sprite = _images[(-1 * changeInto) + 2];
//         // }
//     }

//     private IPieceModel_V2 ChangePieceClass(int changeInto)
//     {
//         RemoveEvents();

//         switch(changeInto)
//         {
//             default:
//                 Debug.Log("[PieceView] - No valid change type found, staying the same.");
//                 return _model;
//         }
//     }

//     public void SetDead()
//     {
//         gameObject.SetActive(false);
//     }

//     public IPieceModel_V2 GetModel()
//     {
//         return _model;
//     }

//     private void OnDestroy()
//     {
//         RemoveEvents();
//     }

//     private void RemoveEvents()
//     {
//         _model.OnMoved -= UpdatePosition;
//         _model.OnDeath -= SetDead;
//         _model.OnChangeInto -= ChangeType;
//     }
// }
