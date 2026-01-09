using System.Collections.Generic;
using UnityEngine;

public class PieceImageDataBase : MonoBehaviour
{
    [SerializeField] private List<Sprite> PieceTypeImages;
    public static List<Sprite> PieceSprites;

    void Start()
    {
        PieceSprites = PieceTypeImages;
    }
}
