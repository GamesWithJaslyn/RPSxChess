using UnityEngine;

/// <summary>
/// A class that allows IPieces to change f
public class ChangeIntoImpl : IChangeInto
{
    private bool _firstTimeChanging;
    private int _changingInto;
    private IPieceModel _currentPiece;

    /// <summary>
    /// The Constructor for the class that allows piece to change into other types.
    /// <summary>
    public ChangeIntoImpl(IPieceModel pieceType, int changingInto) 
    {
        _firstTimeChanging = true;
        _changingInto = changingInto;
    }


    public void ChangeInto() 
    {
        if (CanChange()) 
        {
            _currentPiece.SetPieceType(_changingInto);
        }
    }

    public bool CanChange()
    {
        return _firstTimeChanging;
    }
}
