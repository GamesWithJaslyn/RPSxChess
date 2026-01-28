using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CreatePiecesTest
{
    private CreatePieces _createPieces;
    private IBoardModel _boardModel;
    [SetUp]
    public void SetUp()
    {
        _boardModel = new BoardModelImpl();
        _createPieces = new CreatePieces(_boardModel);
    }

    [Test]
    public void PiecesAmount_RegularCreation()
    {
        Assert.AreEqual(36, _createPieces.GetPieces().Count);
    }
}
