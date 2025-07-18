using UnityEngine;

/// <summary>
/// Represents a Piece in the RPSxChess game.
/// This class is responsible for the visual representation of a piece in the game.
/// It handles the movement of the piece in the game world.
/// </summary>
public class PieceView : MonoBehaviour
{
    /// <summary>
    /// Moves the piece to the specified position.
    /// </summary>
    void MoveTo(Vector3 position)
    {
        transform.position = position; // Update the piece's position in the game world
    }
}
