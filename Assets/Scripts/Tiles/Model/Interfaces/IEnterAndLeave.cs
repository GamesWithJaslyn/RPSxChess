
public interface IEnterAndLeave : ITileModel
{
      /// <summary>
    /// Sets the IPiece on this ITile.
    /// </summary>
    void Enter(IPieceModel pieceType);

    /// <summary>
    /// Removes the IPiece on this ITile.
    /// </summary>
    void Leave();

}
