
public interface IEnterAndLeave : ITileModel
{
  /// <summary>
  /// Gets the IPiece on this ITile.
  /// <br/>
  /// If there is no piece on this ITile, it will return null.
  /// <br/>
  /// </summary>
  /// <returns> An IPieceModel representing the piece on this ITile. </returns>
  IPieceModel GetPiece();

  /// <summary>
  /// Sets the IPiece on this ITile.
  /// </summary>
  void Enter(IPieceModel pieceType);

  /// <summary>
  /// Removes the IPiece on this ITile.
  /// </summary>
  void Leave();
    
  /// <summary>
  /// Checks if this ITile is occupied by an IPiece.
  /// If this Tile has 0 as its piece type, it is considered unoccupied.
  /// </summary>
  /// <returns> A boolean indicating whether this ITile is occupied. </returns>
  bool IsOccupied();

  /// <summary>
  /// Marks whether this tile is a valid move tile for a piece.
  /// </summary>
  /// <param name="isValid"></param>
  public void IsValidTile_CanMoveHere(bool isValid);

  /// <summary>
  /// Checks if a piece can move to this tile.
  /// </summary>
  /// <returns> A boolean indicating whether a piece can move to this tile. </returns
  public bool CanPieceMoveHere();


}
