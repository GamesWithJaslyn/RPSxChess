using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Game/Color DataBase")]
public class Color_DataBase : ScriptableObject
{
    [Header("Default")]
    public Color32 Default_Local;
    public Color32 Default_Online;
    public Color32 Default_Options;
    public Color32 Default_Quit;

    public Color32 Default_BaseTile;
    public Color32 Default_OffesetTile;
    public TMP_FontAsset Default_Font;
    public TMP_FontAsset Default_ShadowFont;

    public Color32 Default_BlueTeam;
    public Color32 Default_RedTeam;


    [Header("Local")]
    public Color32 Local_Hover;
    public Color32 Local_Online;
    public Color32 Local_Options;
    public Color32 Local_Quit;

    public Color32 Local_BaseTile;
    public Color32 Local_OffesetTile;
    public TMP_FontAsset Local_Font;
    public TMP_FontAsset Local_ShadowFont;

    public Color32 Local_BlueTeam;
    public Color32 Local_RedTeam;

    [Header("Online")]
    public Color32 Online_Local;
    public Color32 Online_Hover;
    public Color32 Online_Options;
    public Color32 Online_Quit;

    public Color32 Online_BaseTile;
    public Color32 Online_OffesetTile;
    public TMP_FontAsset Online_Font;
    public TMP_FontAsset Online_ShadowFont;

    public Color32 Online_BlueTeam;
    public Color32 Online_RedTeam;

    [Header("Options")]
    public Color32 Options_Local;
    public Color32 Options_Online;
    public Color32 Options_Hover;
    public Color32 Options_Quit;

    public Color32 Options_BaseTile;
    public Color32 Options_OffesetTile;
    public TMP_FontAsset Options_Font;
    public TMP_FontAsset Options_ShadowFont;

    public Color32 Options_BlueTeam;
    public Color32 Options_RedTeam;

    [Header("Quit")]
    public Color32 Quit_Local;
    public Color32 Quit_Online;
    public Color32 Quit_Options;
    public Color32 Quit_Hover;

    public Color32 Quit_BaseTile;
    public Color32 Quit_OffesetTile;
    public TMP_FontAsset Quit_Font;
    public TMP_FontAsset Quit_ShadowFont;

    public Color32 Quit_BlueTeam;
    public Color32 Quit_RedTeam;

}
