using System.Collections.Generic;
using UnityEngine;

public class PieceImageDataBase : MonoBehaviour
{
    [Header("Piece Type Images")]
    [SerializeField] private List<Sprite> PieceTypeImages;

    [Header("Piece Team Colors")]
    [SerializeField] private Color32 _blueTeam;
    [SerializeField] private Color32 _redTeam;

    public static List<Sprite> PieceSprites;
    public static Color32 BlueTeamColor;
    public static Color32 RedTeamColor;

    void Start()
    {
        PieceSprites = PieceTypeImages;
        BlueTeamColor = _blueTeam;
        RedTeamColor = _redTeam;
    }
}
