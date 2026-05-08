using UnityEngine;
using TMPro;

public class MainMenu_DataBase : MonoBehaviour
{
    [Header("Default")]
    [SerializeField] private Color32 Default_Local;
    [SerializeField] private Color32 Default_Online;
    [SerializeField] private Color32 Default_Options;
    [SerializeField] private Color32 Default_Quit;

    [SerializeField] private Color32 Default_BaseTile;
    [SerializeField] private Color32 Default_OffesetTile;
    [SerializeField] private TMP_FontAsset Default_Font;
    [SerializeField] private TMP_FontAsset Default_ShadowFont;

    [SerializeField] private Color32 Default_BowColor;
    [SerializeField] private Color32 Default_PegasusColor;


    [Header("Local")]
    public Color32 Local_Hover;
    public Color32 Local_Online;
    public Color32 Local_Options;
    public Color32 Local_Quit;

    public Color32 Local_BaseTile;
    public Color32 Local_OffesetTile;
    public TMP_FontAsset Local_Font;
    public TMP_FontAsset Local_ShadowFont;

    public Color32 Local_BowColor;
    public Color32 Local_PegasusColor;

    [Header("Online")]
    [SerializeField] private Color32 Online_Local;
    [SerializeField] private Color32 Online_Hover;
    [SerializeField] private Color32 Online_Options;
    [SerializeField] private Color32 Online_Quit;

    [SerializeField] private Color32 Online_BaseTile;
    [SerializeField] private Color32 Online_OffesetTile;
    [SerializeField] private TMP_FontAsset Online_Font;
    [SerializeField] private TMP_FontAsset Online_ShadowFont;

    [SerializeField] private Color32 Online_BowColor;
    [SerializeField] private Color32 Online_PegasusColor;

    [Header("Options")]
    [SerializeField] private Color32 Options_Local;
    [SerializeField] private Color32 Options_Online;
    [SerializeField] private Color32 Options_Hover;
    [SerializeField] private Color32 Options_Quit;

    [SerializeField] private Color32 Options_BaseTile;
    [SerializeField] private Color32 Options_OffesetTile;
    [SerializeField] private TMP_FontAsset Options_Font;
    [SerializeField] private TMP_FontAsset Options_ShadowFont;

    [SerializeField] private Color32 Options_BowColor;
    [SerializeField] private Color32 Options_PegasusColor;

    [Header("Quit")]
    [SerializeField] private Color32 Quit_Local;
    [SerializeField] private Color32 Quit_Online;
    [SerializeField] private Color32 Quit_Options;
    [SerializeField] private Color32 Quit_Hover;

    [SerializeField] private Color32 Quit_BaseTile;
    [SerializeField] private Color32 Quit_OffesetTile;
    [SerializeField] private TMP_FontAsset Quit_Font;
    [SerializeField] private TMP_FontAsset Quit_ShadowFont;

    [SerializeField] private Color32 Quit_BowColor;
    [SerializeField] private Color32 Quit_PegasusColor;




    public static Color32 S_Default_Local;
    public static Color32 S_Default_Online;
    public static Color32 S_Default_Options;
    public static Color32 S_Default_Quit;

    public static Color32 S_Default_BaseTile;
    public static Color32 S_Default_OffesetTile;
    public static TMP_FontAsset S_Default_Font;
    public static TMP_FontAsset S_Default_ShadowFont;

    public static Color32 S_Default_BowColor;
    public static Color32 S_Default_PegasusColor;


    
    public static Color32 S_Local_Hover;
    public static Color32 S_Local_Online;
    public static Color32 S_Local_Options;
    public static Color32 S_Local_Quit;

    public static Color32 S_Local_BaseTile;
    public static Color32 S_Local_OffesetTile;
    public static TMP_FontAsset S_Local_Font;
    public static TMP_FontAsset S_Local_ShadowFont;

    public static Color32 S_Local_BowColor;
    public static Color32 S_Local_PegasusColor;


    public static Color32 S_Online_Local;
    public static Color32 S_Online_Hover;
    public static Color32 S_Online_Options;
    public static Color32 S_Online_Quit;

    public static Color32 S_Online_BaseTile;
    public static Color32 S_Online_OffesetTile;
    public static TMP_FontAsset S_Online_Font;
    public static TMP_FontAsset S_Online_ShadowFont;

    public static Color32 S_Online_BowColor;
    public static Color32 S_Online_PegasusColor;

    public static Color32 S_Options_Local;
    public static Color32 S_Options_Online;
    public static Color32 S_Options_Hover;
    public static Color32 S_Options_Quit;

    public static Color32 S_Options_BaseTile;
    public static Color32 S_Options_OffesetTile;
    public static TMP_FontAsset S_Options_Font;
    public static TMP_FontAsset S_Options_ShadowFont;

    public static Color32 S_Options_BowColor;
    public static Color32 S_Options_PegasusColor;


    public static Color32 S_Quit_Local;
    public static Color32 S_Quit_Online;
    public static Color32 S_Quit_Options;
    public static Color32 S_Quit_Hover;

    public static Color32 S_Quit_BaseTile;
    public static Color32 S_Quit_OffesetTile;
    public static TMP_FontAsset S_Quit_Font;
    public static TMP_FontAsset S_Quit_ShadowFont;

    public static Color32 S_Quit_BowColor;
    public static Color32 S_Quit_PegasusColor;

    void Start()
    {
        S_Default_Local = Default_Local;
        S_Default_Online = Default_Online;
        S_Default_Options = Default_Options;
        S_Default_Quit = Default_Quit;
        S_Default_BaseTile = Default_BaseTile;
        S_Default_OffesetTile = Default_OffesetTile;
        S_Default_Font = Default_Font;
        S_Default_ShadowFont = Default_ShadowFont;
        S_Default_BowColor = Default_BowColor;
        S_Default_PegasusColor = Default_PegasusColor;


        
        S_Local_Hover = Local_Hover;
        S_Local_Online = Local_Online;
        S_Local_Options = Local_Options;
        S_Local_Quit = Local_Quit;
        S_Local_BaseTile = Local_BaseTile;
        S_Local_OffesetTile = Local_OffesetTile;
        S_Local_Font = Local_Font;
        S_Local_ShadowFont = Local_ShadowFont;
        S_Local_BowColor = Local_BowColor;
        S_Local_PegasusColor = Local_PegasusColor;



        // S_Online_Local;
        // S_Online_Hover;
        // S_Online_Options;
        // S_Online_Quit;
        // S_Online_BaseTile;
        // S_Online_OffesetTile;
        // S_Online_Font;
        // S_Online_ShadowFont;
        // S_Online_BowColor;
        // S_Online_PegasusColor;



        // S_Options_Local;
        // S_Options_Online;
        // S_Options_Hover;
        // S_Options_Quit;
        // S_Options_BaseTile;
        // S_Options_OffesetTile;
        // S_Options_Font;
        // S_Options_ShadowFont;
        // S_Options_BowColor;
        // S_Options_PegasusColor;



        // S_Quit_Local;
        // S_Quit_Online;
        // S_Quit_Options;
        // S_Quit_Hover;
        // S_Quit_BaseTile;
        // S_Quit_OffesetTile;
        // S_Quit_Font;
        // S_Quit_ShadowFont;
        // S_Quit_BowColor;
        // S_Quit_PegasusColor;
    }
}
