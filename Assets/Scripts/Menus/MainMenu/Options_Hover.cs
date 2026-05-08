using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class Options_Hover : AButtons
{
    [Header("Fonts")]
    [SerializeField] private TMP_FontAsset Options_Font;
    [SerializeField] private TMP_FontAsset Options_ShadowFont;

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
        Local = new Color32(155, 54, 54, 255);
        Online = new Color32(255, 170, 184, 255);
        Options = new Color32(255, 255, 255, 255);
        Quit = new Color32(255, 216, 223, 255);
        BaseTile = new Color32(240, 255, 223, 255);
        OffsetTile = new Color32(168, 223, 142, 255);
        BowColor = new Color32(211, 88, 115, 255);
        PegasusColor = new Color32(50, 115, 19, 255);

        Title = Options_Font;
        ShadowFont = Options_ShadowFont;
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
