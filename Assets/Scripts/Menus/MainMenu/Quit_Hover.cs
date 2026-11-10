using UnityEngine;
using UnityEngine.UI;

public class Quit_Hover : AButtons
{
    public Color_DataBase Color;
    public void OnHoverEnter()
    {
        ChangeColorFont();
        Enter_ChangeFont();
        ButtonChange();
        Enter_TileChange();
        Enter_PiecesColorChange();
        Enter_IncreaseSize(gameObject);
    }

    private void ChangeColorFont()
    {
        Local = Color.Quit_Local;
        Online = Color.Quit_Online;
        Options = Color.Quit_Options;
        Quit = Color.Quit_Hover;
        BaseTile = Color.Quit_BaseTile;
        OffsetTile = Color.Quit_OffesetTile;
        BowColor = Color.Quit_BlueTeam;
        PegasusColor = Color.Quit_RedTeam;

        Title = Color.Quit_Font;
        ShadowFont = Color.Quit_ShadowFont;
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        StartCoroutine(ScaleDown(gameObject));
    }

    private void ButtonChange()
    {
        ColorBlock LoCB = buttons[0].colors;
        LoCB.normalColor = Local;
        buttons[0].colors = LoCB;

        ColorBlock onCB = buttons[1].colors;
        onCB.normalColor = Online;
        buttons[1].colors = onCB;

        ColorBlock OpCB = buttons[2].colors;
        OpCB.normalColor = Options;
        buttons[2].colors = OpCB;
    }



}
