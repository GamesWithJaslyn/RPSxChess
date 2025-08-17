
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

    /// <summary>
    /// Gets the ID of this ITile.
    /// </summary>
    /// <returns> The int representing this ITile. </returns>
    int GetID();

    /// <summary>
    /// Gets the type of this ITile.
    /// </summary>
    /// <returns> The int representing the type of this ITile. </returns>
    int GetTileType();

    /// <summary>
    /// Gets this ITile.
    /// </summary>
    /// <returns> A TileType representing this ITile. </returns>
    ITileModel GetTile();

}