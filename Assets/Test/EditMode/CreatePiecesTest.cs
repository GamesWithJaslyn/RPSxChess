using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CreatePiecesTest
{
    private CreatePieces _createPieces;
    [SetUp]
    public void SetUp()
    {
        _createPieces = new CreatePieces();
    }

    [Test]
    public void PiecesAmount_RegularCreation()
    {
        Assert.AreEqual(36, _createPieces.GetPieces().Count);
    }
}
