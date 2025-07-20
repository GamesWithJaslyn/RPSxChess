using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RegularTileTests
{
    private IPieceModel _blueBow;
    private IPieceModel _redBow;
    private IPieceModel _bluePegasus;

    private IEnterAndLeave _tileOne;
    private IEnterAndLeave _tileTwo;
    private IEnterAndLeave _tileThree;

    [SetUp]
    public void SetUp()
    {
        _blueBow = new Bow(39, 1, -3);
        _redBow = new Bow(28, -1, 3);
        _bluePegasus = new Pegasus(38, -3, 2);

        _tileOne = new RegularTile(1, 0, _redBow);
        _tileTwo = new RegularTile(2, 0, _blueBow);
        _tileThree = new RegularTile(3, 0, null);
    }

    [Test]
    public void ExceptionsTest_WhenPieceAndTargetAreInSameTeam_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new RegularTile(2, 1, null));
        Assert.Throws<ArgumentException>(() => new RegularTile(2, -1, null));
        Assert.Throws<ArgumentException>(() => new RegularTile(-3, 0, null));
    }

    [Test]
    public void GetPiece_NoChange()
    {
        Assert.AreEqual(_redBow, _tileOne.GetPiece());

        _tileOne.Leave();
        Assert.AreEqual(null, _tileOne.GetPiece());

        _tileOne.Enter(_bluePegasus);
        Assert.AreEqual(_bluePegasus, _tileOne.GetPiece());

        _tileOne.Leave();
        Assert.AreEqual(null, _tileOne.GetPiece());
    }

    [Test]
    public void GetTileID_NoChange()
    {
        Assert.AreEqual(1, _tileOne.GetID());
        Assert.AreEqual(2, _tileTwo.GetID());
        Assert.AreNotEqual(1, _tileThree.GetID());
        Assert.AreEqual(3, _tileThree.GetID());
    }


    [Test]
    public void GetTileType_NoChange()
    {
        Assert.AreEqual(0, _tileOne.GetTileType());
        Assert.AreEqual(0, _tileTwo.GetTileType());
        Assert.AreNotEqual(1, _tileThree.GetTileType());
        Assert.AreEqual(0, _tileThree.GetTileType());
    }


    [Test]
    public void GetTile_NoChange()
    {
        Assert.AreEqual(_tileOne, _tileOne.GetTile());
        Assert.AreEqual(_tileTwo, _tileTwo.GetTile());
        Assert.AreNotEqual(_tileTwo, _tileThree.GetTile());
        Assert.AreEqual(_tileThree, _tileThree.GetTile());
    }

    [Test]
    public void IsTileOccupied_NoChange()
    {
        Assert.AreEqual(true, _tileOne.IsOccupied());
        _tileOne.Leave();
        Assert.AreEqual(false, _tileOne.IsOccupied());

        Assert.AreEqual(true, _tileTwo.IsOccupied());

        Assert.AreNotEqual(true, _tileThree.IsOccupied());
        Assert.AreEqual(false, _tileThree.IsOccupied());
        _tileThree.Enter(_bluePegasus);
        Assert.AreEqual(true, _tileThree.IsOccupied());
    }

}
