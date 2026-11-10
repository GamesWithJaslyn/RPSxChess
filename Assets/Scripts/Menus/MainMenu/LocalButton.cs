using UnityEngine.UI;


public class LocalButton : AButtons
{
    public Color_DataBase Color;

    public void OnClick()
    {
        SwitchScene.LoadSceneByName("LocalGame");
    }

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
        Local = Color.Local_Hover;
        Online = Color.Local_Online;
        Options = Color.Local_Options;
        Quit = Color.Local_Quit;
        BaseTile = Color.Local_BaseTile;
        OffsetTile = Color.Local_OffesetTile;
        BowColor = Color.Local_BlueTeam;
        PegasusColor = Color.Local_RedTeam;

        Title = Color.Local_Font;
        ShadowFont = Color.Local_ShadowFont;
    }

    private void ButtonChange()
    {
        ColorBlock onCB = buttons[1].colors;
        onCB.normalColor = Online;
        buttons[1].colors = onCB;

        ColorBlock opCB = buttons[2].colors;
        opCB.normalColor = Options;
        buttons[2].colors = opCB;

        ColorBlock qCB = buttons[3].colors;
        qCB.normalColor = Quit;
        buttons[3].colors = qCB;
    }

}
