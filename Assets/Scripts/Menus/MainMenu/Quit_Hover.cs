using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class Quit_Hover : AButtons
{
    [Header("Fonts")]
    [SerializeField] private TMP_FontAsset Quit_Font;
    [SerializeField] private TMP_FontAsset Quit_ShadowFont;

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
        Local = new Color32(36, 14, 8, 255);
        Online = new Color32(77, 43, 34, 255);
        Options = new Color32(135, 78, 56, 255);
        Quit = new Color32(234, 196, 154, 255);
        BaseTile = new Color32(84, 35, 26, 255);
        OffsetTile = new Color32(49, 20, 15, 255);
        BowColor = new Color32(238, 203, 165, 255);
        PegasusColor = new Color32(163, 81, 0, 255);

        Title = Quit_Font;
        ShadowFont = Quit_ShadowFont;
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
