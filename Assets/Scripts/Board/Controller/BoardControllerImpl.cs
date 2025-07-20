using UnityEngine;

public class BoardControllerImpl : MonoBehaviour
{
    public IBoardModel boardModel;

    // public void Start()
    // {
    //     // Initialize the board model and view
    //     boardModel = new IBoardModel();
    //     boardModel.SetupInitialBoard();

    //     boardView.Initialize(boardModel, this)
    //     // Set up the board view with the model
    //     boardView.Setup(boardModel);

    //     // Optionally, you can add event listeners for tile clicks
    //     boardView.OnTileClicked += OnTileClicked;

    // }

    public void OnTileClicked(int tileID)
    {

    }
}
