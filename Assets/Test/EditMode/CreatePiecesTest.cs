using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CreatePiecesTest
{
    private CreatePieces_V2 _createPieces;
    private IBoardModel_V2 _boardModel;
    [SetUp]
    public void SetUp()
    {
        _boardModel = new BoardModelImpl_V2();
        _createPieces = new CreatePieces_V2(_boardModel);
    }

    [Test]
    public void PiecesAmount_RegularCreation()
    {
        Assert.AreEqual(36, _createPieces.GetPieces().Count);
    }
}
