using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoardTests_V2
{
    private IBoardModel_V2 _boardModel;
    private IPieceModel_V2 _bow;

    [SetUp]
    public void Setup()
    {
        _boardModel = new BoardModelImpl_V2();
        _bow = new PieceModelImpl(22, PieceType.Bow, Team.Blue, _boardModel);
        _boardModel.AddPiece(_bow);
    }

    [Test]
    public void NumberOfPiece()
    {
        Assert.AreEqual(37, _boardModel.GetAllPieces().Count);
        _boardModel.AddPiece(_bow);
        Assert.AreEqual(38, _boardModel.GetAllPieces().Count);
        _boardModel.RemovePiece(_bow);
        _boardModel.RemovePiece(_bow);

        Assert.AreEqual(36, _boardModel.GetAllPieces().Count);

    }

    [Test]
    public void NumberOfTiles()
    {
        Assert.AreEqual(121, _boardModel.GetAllTiles().Count);
    }

    [Test]
    public void FindingTile()
    {
        Assert.AreEqual(true, _boardModel.IsInside(35));
    }

    [Test]
    public void SamePiece()
    {
        Assert.AreEqual(true, _boardModel.SelectPiece(22).Equals(_bow));
        Assert.AreEqual(_bow, _boardModel.SelectPiece(22));
    }

    [Test]
    public void MovePiece_TooFar_InvalidMove()
    {
        IPieceModel_V2 blueBow = new PieceModelImpl(39, PieceType.Bow, Team.Blue, _boardModel);
        _boardModel.AddPiece(blueBow);

        Assert.AreNotEqual(null, _boardModel.SelectPiece(39));
        Assert.AreEqual(false, _boardModel.MovePiece(32));
        Assert.AreEqual(null, _boardModel.SelectPiece(32));
        Assert.AreNotEqual(32, blueBow.Position);
    }

    [Test]
    public void MovePiece_ValidMove()
    {
        IPieceModel_V2 blueBow = new PieceModelImpl(39, PieceType.Bow, Team.Blue, _boardModel);
        _boardModel.AddPiece(blueBow);

        Assert.AreNotEqual(null, _boardModel.SelectPiece(39));
        Assert.AreEqual(false, _boardModel.MovePiece(32));
        Assert.AreEqual(null, _boardModel.SelectPiece(32));
        Assert.AreNotEqual(32, blueBow.Position);

        _boardModel.SelectPiece(39);
        _boardModel.MovePiece(40);

        _boardModel.SwitchTurn(); //switching turn back to blue

        Assert.AreEqual(null, _boardModel.SelectPiece(39));
        Assert.AreEqual(blueBow, _boardModel.SelectPiece(40));
        Assert.AreNotEqual(39, blueBow.Position);
        Assert.AreEqual(40, blueBow.Position);
    }

}
