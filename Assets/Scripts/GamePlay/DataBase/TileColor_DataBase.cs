using UnityEngine;

public class TileColor_DataBase : MonoBehaviour
{
    [Header("Regular Tile Colors")]
    [SerializeField] Color32 _baseColor = new Color32(152, 87, 95, 255);
    [SerializeField] Color32 _offsetColor = new Color32(202, 154, 192, 255);

    [Header("Water Tile Colors")]
    [SerializeField] Color32 _waterColor = new Color32(45, 193, 207, 255);

    [Header("Teleport Tile Colors")]
    [SerializeField] Color32 _teleport = new Color32(134, 36, 191, 255);

    [Header("Highlight Colors")]
    [SerializeField] Color32 _highlightColorBase = new Color32(217, 126, 41, 200);
    [SerializeField] Color32 _highlightColorOffset = new Color32(237, 214, 123, 200);

    [Header("Special Attack Colors")]
    [SerializeField] Color32 _specialAttackColor = new Color32(255, 0, 0, 100);

    public static Color32 Regular_BaseColor;
    public static Color32 Regular_OffsetColor;
    public static Color32 WaterColor;
    public static Color32 TeleportColor;

    public static Color32 Regular_HighlightColorBase;
    public static Color32 Regular_HighlightColorOffset;
    public static Color32 SpecialAttackColor;


    void Start()
    {
        Regular_BaseColor = _baseColor;
        Regular_OffsetColor = _offsetColor;
        WaterColor = _waterColor;
        TeleportColor = _teleport;
        Regular_HighlightColorBase = _highlightColorBase;
        Regular_HighlightColorOffset = _highlightColorOffset;
        SpecialAttackColor = _specialAttackColor;
    }
}
