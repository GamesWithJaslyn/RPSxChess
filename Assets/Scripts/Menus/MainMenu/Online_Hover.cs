using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class Online_Hover : AButtons
{
    [Header("Fonts")]
    [SerializeField] private TMP_FontAsset Online_Font;
    [SerializeField] private TMP_FontAsset Online_ShadowFont;

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
        Local = new Color32(245, 179, 72, 255);
        Online = new Color32(246, 243, 194, 255);
        Options = new Color32(227, 116, 52, 255);
        Quit = new Color32(81, 42, 0, 255);
        BaseTile = new Color32(145, 198, 188, 255);
        OffsetTile = new Color32(75, 157, 169, 255);
        BowColor = new Color32(245, 179, 72, 255);
        PegasusColor = new Color32(9, 72, 114, 255);

        Title = Online_Font;
        ShadowFont = Online_ShadowFont;
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
