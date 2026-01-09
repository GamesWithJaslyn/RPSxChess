// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;

// public class BoardTests
// {
//     private IBoardModel _boardModel;

//     [SetUp]
//     public void Setup()
//     {
//         _boardModel = new BoardModelImpl();
//     }

//     [Test]
//     public void NumberOfPiece()
//     {
//         Assert.AreEqual(36, _boardModel.GetAllPieces().Count);
//         Debug.Log("piece count: " + _boardModel.GetAllPieces().Count);

//     }

//     [Test]
//     public void NumberOfTiles()
//     {
//         Assert.AreEqual(121, _boardModel.GetAllTiles().Count);
//         Debug.Log("tile count: " + _boardModel.GetAllTiles().Count);
//     }


//     [Test]
//     public void MovePiece_TooFar_InvalidMove()
//     {
//         // BoardModelImpl._allPieces.Clear();
//         AAttackingPiece blueBow = new Bow(39, 1, -3, _boardModel);
//         _boardModel.AddPiece(blueBow);
//         _boardModel.SelectPiece(39);

//         Assert.AreNotEqual(null, _boardModel.SelectPiece(39));
//         Assert.Throws<System.ArgumentException>(() => _boardModel.MovePiece(10));
//     }

//     [Test]
//     public void MovePiece_SwitchingTurns_PiecesTakeTurnMoving()
//     {
//         Assert.AreEqual(true, _boardModel.IsBlueTurn());
//         _boardModel.AddPiece(new Sword(2, 2, -1, _boardModel));
//         _boardModel.SelectPiece(2);
//         _boardModel.MovePiece(3);

//         Assert.AreEqual(false, _boardModel.IsBlueTurn());
//         _boardModel.AddPiece(new Sword(10, -2, 1, _boardModel));
//         _boardModel.SelectPiece(10);
//         _boardModel.MovePiece(9);

//         Assert.AreEqual(true, _boardModel.IsBlueTurn());
//     }

//     [Test]
//     public void SwitchTurn_ChangesTurn()
//     {
//         Assert.AreEqual(true, _boardModel.IsBlueTurn());
//         _boardModel.SelectPiece(10);
//         Assert.AreEqual(true, _boardModel.IsBlueTurn());

//         _boardModel.MovePiece(9);
//         Assert.AreEqual(false, _boardModel.IsBlueTurn());
//     }

//     [Test]
//     public void ChangePieceType_FirstTime_ValidChange()
//     {
//         _boardModel.AddPiece(new Sword(14, -2, 1, _boardModel));
//         Assert.AreEqual(-2, _boardModel.SelectPiece(14).GetPieceType());

//         _boardModel.SelectPiece(14);
//         _boardModel.MovePiece(3);
//         //_boardModel.SelectPiece(1);
//         _boardModel.ChangePieceType(-1);

//         Assert.AreEqual(-1, _boardModel.SelectPiece(3).GetPieceType());
//     }

//     [Test]
//     public void ChangePieceType_MoreThanOnce_InvalidChange()
//     {

//         _boardModel.AddPiece(new Sword(14, -2, 1, _boardModel));
//         Assert.AreEqual(-2, _boardModel.SelectPiece(14).GetPieceType());

//         _boardModel.SelectPiece(14);
//         _boardModel.MovePiece(3);
//        // _boardModel.SelectPiece(1);
//         _boardModel.ChangePieceType(-3);

//         Assert.AreEqual(-3, _boardModel.SelectPiece(3).GetPieceType());

//         _boardModel.SelectPiece(3);
//         _boardModel.MovePiece(14);

//         _boardModel.SelectPiece(14);
//         _boardModel.MovePiece(3);

//         Assert.AreEqual(-3, _boardModel.SelectPiece(3).GetPieceType());

//        Assert.Throws<System.ArgumentException>(() => _boardModel.ChangePieceType(-1));

//         Assert.AreEqual(-3, _boardModel.SelectPiece(3).GetPieceType());
//     }

// }
