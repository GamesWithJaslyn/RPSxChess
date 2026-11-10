using UnityEngine.UI;

public class Options_Hover : AButtons
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

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        StartCoroutine(ScaleDown(gameObject));
    }

    private void ChangeColorFont()
    {
        Local = Color.Options_Local;
        Online = Color.Options_Online;
        Options = Color.Options_Hover;
        Quit = Color.Options_Quit;
        BaseTile = Color.Options_BaseTile;
        OffsetTile = Color.Options_OffesetTile;
        BowColor = Color.Options_BlueTeam;
        PegasusColor = Color.Options_RedTeam;

        Title = Color.Options_Font;
        ShadowFont = Color.Options_ShadowFont;
    }

    private void ButtonChange()
    {
        ColorBlock LoCB = buttons[0].colors;
        LoCB.normalColor = Local;
        buttons[0].colors = LoCB;

        ColorBlock onCB = buttons[1].colors;
        onCB.normalColor = Online;
        buttons[1].colors = onCB;

        ColorBlock qCB = buttons[3].colors;
        qCB.normalColor = Quit;
        buttons[3].colors = qCB;
    }

}
