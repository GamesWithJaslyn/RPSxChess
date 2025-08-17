using UnityEngine;

/// <summary>
/// Represents a Tile in the RPSxChess game.
/// This class is responsible for the visual representation of a tile in the game.
/// It handles highlighting and unhighlighting the tile.
/// </summary>
public class TileView : MonoBehaviour
{
    public Color defaultColor;
    public Color highlightColor;
    void Start()
    {
        // Initialize the tile view, if needed
        defaultColor = Color.white;
        highlightColor = Color.yellow;
        GetComponent<Renderer>().material.color = defaultColor; // Set the default color
    }

    /// <summary>
    /// Highlights the tile by changing its color.
    /// </summary>
    private void HightLight()
    {
        GetComponent<Renderer>().material.color = highlightColor; // Change to highlight color
    }

    /// <summary>
    /// Unhighlights the tile by resetting its color to default.
    /// </summary>
    private void UnHighlight()
    {
        GetComponent<Renderer>().material.color = defaultColor; // Reset to default color
    }
}
