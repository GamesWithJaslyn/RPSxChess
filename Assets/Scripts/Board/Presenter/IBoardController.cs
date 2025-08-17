using UnityEngine;

/// <summary>
/// Interface for the Board Controller
/// </summary>
public interface IBoardController
{
    // Start is called once before the first execution of 
    // Update after the MonoBehaviour is created
    void Start();

    /// <summary>
    /// Called when a tile is clicked.
    /// This method is responsible for handling the logic when a tile is clicked.
    /// It should determine the tile's ID and perform the necessary
    /// actions based on the game rules
    /// </summary>
    void OnTileClicked(int tileID);
}
