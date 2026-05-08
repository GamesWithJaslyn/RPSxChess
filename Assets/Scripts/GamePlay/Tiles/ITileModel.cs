
using System;


/// <summary>
/// Represents a tile in the RPSxChess game.
/// This interface defines the basic structure and behavior of a tile.
/// Tiles can be used to represent different positions on the game board.
/// <br/>
/// It can be extended to include additional properties or methods specific to different types of tiles.
/// <br/>
/// Example implementations could include different types of tiles with unique characteristics or behaviors.
/// <br/>
/// </summary>
public interface ITileModel
{
    int ID { get; }
    TileType Type { get; }
    ITileRule Rule { get; }
    IPieceModel Occupant { get; }
    ITileModel GetTile();
    event Action<bool> OnTileValid;
    event Action<bool> OnTileSpecialAttackValid;
    event Action OnChangeType;
    event Action<IPieceModel> OnPieceEntered;

    public void EnterPiece(IPieceModel piece);
    public void SetValid(bool isvalid);
    public void SetSpecialAttackValid(bool isvalid);

    /// <summary>
    /// Changes the Tile Type of this Tile.
    /// </summary>
    /// <param name="type"> The Type this Tile will change into. </param>
    void ChangeType(TileType type);

}