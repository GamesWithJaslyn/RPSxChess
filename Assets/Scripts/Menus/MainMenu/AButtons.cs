using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AButtons : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] protected GameObject TitleObj;
    [SerializeField] protected GameObject ShadowFontObj;
    [SerializeField] protected List<GameObject> baseTiles;
    [SerializeField] protected List<GameObject> offsetTiles;
    [SerializeField] protected List<Button> buttons;
    [SerializeField] protected Image BowImage;
    [SerializeField] protected Image PegasusImage;

    protected Color32 Local;
    protected Color32 Online;
    protected Color32 Options;
    protected Color32 Quit;
    protected Color32 BaseTile;
    protected Color32 OffsetTile;
    protected TMP_FontAsset Title;
    protected TMP_FontAsset ShadowFont;
    protected Color32 BowColor;
    protected Color32 PegasusColor;


    [Header("Default")]
    [SerializeField] private TMP_FontAsset Default_Title;
    [SerializeField] private TMP_FontAsset Default_ShadowFont;
    private Color32 Default_Local;
    private Color32 Default_Online;
    private Color32 Default_Options;
    private Color32 Default_Quit;
    private Color32 Default_BaseTile;
    private Color32 Default_OffsetTile;
    private Color32 Default_BowColor;
    private Color32 Default_PegasusColor;

    Vector3 scaleUp;
    Vector3 scaleDown;

    void Start()
    {
        Default_Local = new Color32(5, 74, 145, 255);
        Default_Online = new Color32(226, 126, 0, 255);
        Default_Options = new Color32(51, 151, 95, 255);
        Default_Quit = new Color32(90, 12, 12, 255);
        Default_BaseTile = new Color32(223, 146, 113, 255);
        Default_OffsetTile = new Color32(183, 72, 72, 255);
        Default_BowColor = new Color32(22, 76, 136, 255);
        Default_PegasusColor = new Color32(116, 0, 0, 255);
        scaleUp = new Vector3(1.25f, 1.5f, 1);
        scaleDown = new Vector3(1, 1, 1);
    }

    protected void Enter_PiecesColorChange()
    {
        BowImage.color = BowColor;
        PegasusImage.color = PegasusColor;
    }

    protected void Enter_ChangeFont()
    {
        TitleObj.GetComponent<TextMeshProUGUI>().font = Title;
        ShadowFontObj.GetComponent<TextMeshProUGUI>().font = ShadowFont;
    }

    protected void Enter_TileChange()
    {
        foreach (GameObject tile in baseTiles)
        {
            tile.GetComponent<Image>().color = BaseTile;
        }

        foreach (GameObject tile in offsetTiles)
        {
            tile.GetComponent<Image>().color = OffsetTile;
        }
    }

    protected void Enter_IncreaseSize(GameObject obj)
    {
        StartCoroutine(ScaleUp(obj));
    }

    private IEnumerator ScaleUp(GameObject obj)
    {
        Vector3 startScale = obj.transform.localScale;
        float time = 0f;
        float duration = 0.2f;

        while (time < duration)
        {
            time += Time.deltaTime;
            obj.transform.localScale = Vector3.Lerp(startScale, scaleUp, time / duration);
            yield return null;
        }

        obj.transform.localScale = scaleUp;
    }


    public virtual void OnHoverExit()
    {
        ChangeFont();
        ButtonChange();
        Exit_TileChange();
        PiecesColorChange();
    }

    private void ChangeFont()
    {
        TitleObj.GetComponent<TextMeshProUGUI>().font = Default_Title;
        ShadowFontObj.GetComponent<TextMeshProUGUI>().font = Default_ShadowFont;
    }

    private void ButtonChange()
    {
        ColorBlock LoCB = buttons[0].colors;
        LoCB.normalColor = Default_Local;
        buttons[0].colors = LoCB;

        ColorBlock onCB = buttons[1].colors;
        onCB.normalColor = Default_Online;
        buttons[1].colors = onCB;

        ColorBlock opCB = buttons[2].colors;
        opCB.normalColor = Default_Options;
        buttons[2].colors = opCB;

        ColorBlock qCB = buttons[3].colors;
        qCB.normalColor = Default_Quit;
        buttons[3].colors = qCB;
    }

    private void Exit_TileChange()
    {
        foreach (GameObject tile in baseTiles)
        {
            tile.GetComponent<Image>().color = Default_BaseTile;
        }

        foreach (GameObject tile in offsetTiles)
        {
            tile.GetComponent<Image>().color = Default_OffsetTile;
        }
    }

    private void PiecesColorChange()
    {
        BowImage.color = Default_BowColor;
        PegasusImage.color = Default_PegasusColor;
    }

    protected IEnumerator ScaleDown(GameObject obj)
    {
        Vector3 startScale = obj.transform.localScale;
        float time = 0f;
        float duration = 0.2f;

        while (time < duration)
        {
            time += Time.deltaTime;
            obj.transform.localScale = Vector3.Lerp(startScale, scaleDown, time / duration);
            yield return null;
        }

        obj.transform.localScale = scaleDown;
    }
}