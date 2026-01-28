using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PieceTests
{
    private IBoardModel _boardModel;
    private IPieceModel _blueBow;
    private IPieceModel _redBow;
    private IPieceModel _blueTarget;
    private IPieceModel _redTarget;


    [SetUp]
    public void SetUp()
    {
        _boardModel = new BoardModelImpl();

        _blueBow = new AAttackingPiece(39, PieceType.Bow, Team.Blue,
                    PieceType.Pegasus, _boardModel);
        _redBow = new AAttackingPiece(28, PieceType.Bow, Team.Red,
                    PieceType.Pegasus, _boardModel);

        _blueTarget = new AAttackingPiece(38, PieceType.Pegasus, Team.Red,
                    PieceType.Sword, _boardModel);
        _redTarget = new AAttackingPiece(27, PieceType.Pegasus, Team.Blue,
                    PieceType.Sword, _boardModel);

        _boardModel.AddPiece(_blueBow);
        _boardModel.AddPiece(_redBow);
        _boardModel.AddPiece(_blueTarget);
        _boardModel.AddPiece(_redTarget);
    }

    [Test]
    public void CorrectTarget()
    {
        if (_blueBow is AAttackingPiece aAttacking)
        {
            Assert.AreEqual(_blueTarget.PieceType, aAttacking.TargetType);
            Assert.AreNotEqual(_redBow.PieceType, aAttacking.TargetType);
        }
    }

    [Test]
    public void Movement_MovingOnRegularTiles_SetPos()
    {
        Assert.AreEqual(39, _blueBow.Position);
        _blueBow.MakeMove(40);
        Assert.AreEqual(40, _blueBow.Position);

        //A Piece cannot make invalid moves
        Assert.AreEqual(28, _redBow.Position);
        _redBow.MakeMove(2);
        Assert.AreNotEqual(2, _redBow.Position);
        Assert.AreEqual(28, _redBow.Position);
    }

    [Test]
    public void Movement_MovingOnRegularTiles_GetValidMoves()
    {
        foreach (PieceModelImpl piece in _boardModel.GetAllPieces())
        {
            ITileModel tile = _boardModel.GetAllTiles().Find(tile => tile.ID == piece.Position);
            tile.EnterPiece(piece);
        }

        Move move = new MoveBuilder(new Move(11, 22)).BuildMove();
        List<Move> moves = new List<Move> { move };
        Assert.AreNotEqual(null, _boardModel.SelectPiece(11));
        CollectionAssert.AreEquivalent(moves, _boardModel.SelectPiece(11).GetValidMoves());

        Assert.AreEqual(39, _blueBow.Position);

        List<Move> validMoves = _blueBow.GetValidMoves();
        _blueBow.MakeMove(validMoves[0].To);
        Assert.AreEqual(50, _blueBow.Position);
    }


}
