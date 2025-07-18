using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BowTests
{
    private AAttackingPiece _blueBow;
    private AAttackingPiece _redBow;
    private AAttackingPiece _blueTarget;
    private AAttackingPiece _redTarget;


    [SetUp]
    public void SetUp()
    {
        ABasicPiece._allPieces.Clear();

        _blueBow = new Bow(39, 1, -3);
        _redBow = new Bow(28, -1, 3);

        _blueTarget = new Pegasus(38, -3, 2);
        _redTarget = new Pegasus(27, 3, -2);
    }

    [Test]
    public void ExceptionsTest_WhenPieceAndTargetAreInSameTeam_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Bow(39, 1, 3));
        Assert.Throws<ArgumentException>(() => new Bow(39, -1, -3));
        Assert.Throws<ArgumentException>(() => new Bow(39, 1, -2));
        Assert.Throws<ArgumentException>(() => new Bow(39, -1, 1));
        Assert.Throws<ArgumentException>(() => new Bow(39, -2, 3));
        Assert.Throws<ArgumentException>(() => new Bow(39, 0, 3));
    }

    [Test]
    public void Movement_MovingOnRegularTiles_ChangesPosition()
    {
        Assert.AreEqual(39, _blueBow.GetPos());
        _blueBow.SetPos(40);
        Assert.AreEqual(40, _blueBow.GetPos());

        //A Piece can make invalid moves, it isn't its job to parse valid moves
        Assert.AreEqual(28, _redBow.GetPos());
        _redBow.SetPos(2);
        Assert.AreEqual(2, _redBow.GetPos());
    }

    [Test]
    public void PossibleMovesList_RegardlessOfTileType_NoChange()
    {
        List<int> possibleMoves = new List<int>();

        Debug.Log("Piece Pos: " + _blueBow.GetPos());

        int up = _blueBow.GetPos() - 11;
        int down = _blueBow.GetPos() + 11;
        int left = _blueBow.GetPos() - 1;
        int right = _blueBow.GetPos() + 1;

        possibleMoves.Add(up);
        possibleMoves.Add(down);
        possibleMoves.Add(left);
        possibleMoves.Add(right);

        Assert.AreEqual(39, _blueBow.GetPos());
        Assert.AreEqual(possibleMoves, _blueBow.GetMoveTiles());
        _blueBow.SetPos(40);
        Assert.AreEqual(40, _blueBow.GetPos());

        up = _blueBow.GetPos() - 11;
        down = _blueBow.GetPos() + 11;
        left = _blueBow.GetPos() - 1;
        right = _blueBow.GetPos() + 1;

        possibleMoves.Clear();

        possibleMoves.Add(up);
        possibleMoves.Add(down);
        possibleMoves.Add(left);
        possibleMoves.Add(right);

        Assert.AreEqual(possibleMoves, _blueBow.GetMoveTiles());
    }

    [Test]
    public void TargetClass_TargetsStillAlive_ReturnsITargetClass()
    {
        List<AAttackingPiece> pieces = new List<AAttackingPiece>();

        Assert.AreEqual(-3, _blueBow.GetTargetType());
        Assert.AreEqual(3, _redBow.GetTargetType());
    }

    [Test]
    public void TargetTiles_TargetsStillAlive_ReturnsTileNumbers()
    {
        ITargets blueBowTarget = new TargetsImpl(_blueBow, -3);
        ITargets redBowTarget = new TargetsImpl(_redBow, 3);

        List<int> blueTargetTiles = new List<int> { 38 };
        List<int> redTargetTiles = new List<int>{27};


        Assert.AreEqual(blueTargetTiles, blueBowTarget.GetTargetTiles());
        Assert.AreEqual(new List<int>{27}, redBowTarget.GetTargetTiles());
    }

    [Test]
    public void TargetType_ReturnsTargetType()
    {
        Assert.AreEqual(-3, _blueBow.GetTargetType());
        Assert.AreEqual(3, _redBow.GetTargetType());
    }

    [Test]
    public void Team_ReturnsTheTeam()
    {
        Assert.AreEqual(true, _blueBow.IsSameTeam("Blue"));
        Assert.AreNotEqual(true, _blueBow.IsSameTeam("Red"));

        Assert.AreEqual(true, _redBow.IsSameTeam("Red"));
        Assert.AreNotEqual(true, _redBow.IsSameTeam("Blue"));

        Assert.Throws<ArgumentException>(() => _redBow.IsSameTeam("Green"));
        Assert.Throws<ArgumentException>(() => _blueBow.IsSameTeam("Orange"));
    }

    [Test]
    public void ChangedInto_ReachesOtherSideForFirstTime_ChangeType()
    {
        Assert.AreEqual(1, _blueBow.GetPieceType());
        _blueBow.SetPos(15);
        _blueBow.ChangeInto(3);
        Assert.AreNotEqual(3, _blueBow.GetPieceType());

        _blueBow.SetPos(8);
        _blueBow.ChangeInto(3);
        Assert.AreEqual(3, _blueBow.GetPieceType());

        _blueBow.SetPos(9);
        _blueBow.ChangeInto(2);
        Assert.AreEqual(3, _blueBow.GetPieceType());

    }

    [Test]
    public void ChangedInto_ReachesOtherSideAfterFirstTime_NoChange()
    {
        Assert.AreEqual(1, _blueBow.GetPieceType());
        _blueBow.SetPos(15);
        _blueBow.ChangeInto(3);
        Assert.AreNotEqual(3, _blueBow.GetPieceType());

        _blueBow.SetPos(8);
        _blueBow.ChangeInto(3);
        Assert.AreEqual(3, _blueBow.GetPieceType());

        _blueBow.SetPos(9);
        _blueBow.ChangeInto(2);
        Assert.AreNotEqual(2, _blueBow.GetPieceType());

        _blueBow.SetPos(105);
        _blueBow.ChangeInto(2);
        Assert.AreNotEqual(2, _blueBow.GetPieceType());
        
        _blueBow.SetPos(119);
        _blueBow.ChangeInto(1);
        Assert.AreEqual(3, _blueBow.GetPieceType());
    }
}
