using UnityEngine.UI;

public class Online_Hover : AButtons
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
        Local = Color.Online_Local;
        Online = Color.Online_Hover;
        Options = Color.Online_Options;
        Quit = Color.Online_Quit;
        BaseTile = Color.Online_BaseTile;
        OffsetTile = Color.Online_OffesetTile;
        BowColor = Color.Online_BlueTeam;
        PegasusColor = Color.Online_RedTeam;

        Title = Color.Online_Font;
        ShadowFont = Color.Online_ShadowFont;
    }

    private void ButtonChange()
    {
        ColorBlock LoCB = buttons[0].colors;
        LoCB.normalColor = Local;
        buttons[0].colors = LoCB;

        ColorBlock opCB = buttons[2].colors;
        opCB.normalColor = Options;
        buttons[2].colors = opCB;

        ColorBlock qCB = buttons[3].colors;
        qCB.normalColor = Quit;
        buttons[3].colors = qCB;
    }

}
