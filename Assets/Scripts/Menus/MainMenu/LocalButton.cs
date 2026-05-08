using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;


public class LocalButton : AButtons
{
    [Header("Fonts")]
    [SerializeField] private TMP_FontAsset Local_Font;
    [SerializeField] private TMP_FontAsset Local_ShadowFont;

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
        Local = new Color32(225, 255, 187, 255);
        Online = new Color32(97, 175, 171, 255);
        Options = new Color32(6, 86, 94, 255);
        Quit = new Color32(0, 0, 0, 255);
        BaseTile = new Color32(7, 71, 153, 255);
        OffsetTile = new Color32(0, 26, 110, 255);
        BowColor = new Color32(176, 199, 121, 255);
        PegasusColor = new Color32(135, 175, 241, 255);

        Title = Local_Font;
        ShadowFont = Local_ShadowFont;
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
