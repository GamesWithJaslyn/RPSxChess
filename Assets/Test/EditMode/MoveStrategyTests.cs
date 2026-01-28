using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

public class MoveStrategyTests
{
    private IBoardModel _boardModel;
    private IPieceModel _bow;
    private IPieceModel _sword;
    private IPieceModel _pegasus;


    [SetUp]
    public void SetUp()
    {
        _boardModel = new BoardModelImpl();
        _bow = new AAttackingPiece(35, PieceType.Bow, Team.Blue, PieceType.Pegasus, _boardModel);
        _sword = new AAttackingPiece(24, PieceType.Sword, Team.Red, PieceType.Bow, _boardModel);
        _pegasus = new AAttackingPiece(36, PieceType.Pegasus, Team.Red,
        PieceType.Sword, _boardModel);

        _boardModel.AddPiece(_bow);
        _boardModel.AddPiece(_sword);
        _boardModel.AddPiece(_pegasus);
    }


    [Test]
    public void GetValidMovesTest_Capture()
    {
        Move move_down = new MoveBuilder(new Move(35, 46)).BuildMove();
        Move move_left = new MoveBuilder(new Move(35, 34)).BuildMove();
        Move move_right = new MoveBuilder(new Move(35, 36))
        .AddFlags(new List<MoveFlags> { MoveFlags.Capture }).BuildMove();

        List<Move> bowMoves = new List<Move> { move_down, move_left, move_right, };
        List<Move> actual = _boardModel.SelectPiece(35).GetValidMoves();

        Assert.AreEqual(_bow, _boardModel.SelectPiece(35));
        Assert.AreEqual(bowMoves.Count, actual.Count);
        CollectionAssert.AreEqual(bowMoves, actual);
    }

    [Test]
    public void GetValidMovesTest_Promotion()
    {
        Move move_up = new MoveBuilder(new Move(106, 95)).BuildMove();
        Move move_down = new MoveBuilder(new Move(106, 117))
                        .SetPromotionType(PieceType.None).BuildMove();
        Move move_right = new MoveBuilder(new Move(106, 107))
                        .AddFlags(new List<MoveFlags> { MoveFlags.Capture }).BuildMove();

        AAttackingPiece newBow = new AAttackingPiece(106, PieceType.Bow,
             Team.Blue, PieceType.Pegasus, _boardModel);
        _boardModel.AddPiece(newBow);

        List<Move> bowMoves = new List<Move> { move_up, move_down, move_right, };
        List<Move> actual = _boardModel.SelectPiece(106).GetValidMoves();

        Assert.AreEqual(newBow, _boardModel.SelectPiece(106));
        Assert.AreEqual(bowMoves.Count, actual.Count);
        CollectionAssert.AreEqual(bowMoves, actual);
    }
}
