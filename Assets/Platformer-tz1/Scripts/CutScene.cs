using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogTetx;
    [SerializeField] private TextMeshProUGUI escTetx;

    [SerializeField] private Image fadeBG;
    [SerializeField] private RectTransform knight;
    [SerializeField] private RectTransform blackLineTop;
    [SerializeField] private RectTransform blackLineBottom;
    [SerializeField] private int blackLineHeight;
    [SerializeField] private float fadeBgAlpha;
    [SerializeField] private float knightPosition;
    private string[] text;
    private bool isSkip;

    private void Awake()
    {
        fadeBG.color = new Color(0,0,0,0);
        knight.anchoredPosition += Vector2.left*200;
        blackLineTop.sizeDelta = blackLineBottom.sizeDelta = Vector2.zero;
        text = dialogTetx.text.Split("##");
        dialogTetx.text = "";
        escTetx.gameObject.SetActive(false);
    }

    private void Start()
    {
        Player.ST._inputs.isActive = false;
        Invoke("Starter", 1.0f);
    }

    private void Starter()
    {
        StartCoroutine(DialogStart());
    }
    private void DialogStarter()
    {
        StartCoroutine(Dialog());
    }

    private void Update()
    {
        if(!isSkip)
            isSkip = Input.GetButtonDown(GlobalParams.JUMP);
    }
    private IEnumerator DialogStart()
    {
        Player.ST.gameObject.SetActive(false);
        bool isBg = false;
        float i = 0;
        float j;
        float k = knight.anchoredPosition.x;
        while (!isBg)
        {
            yield return new WaitForSeconds(0.05f);
            
            if(i < fadeBgAlpha)
                fadeBG.color = new Color(0,0,0,i);
            
            j = i * (blackLineHeight/fadeBgAlpha);
            
            if(j < blackLineHeight)
                blackLineTop.sizeDelta = blackLineBottom.sizeDelta = new Vector2(0, j);

            k = i * (knightPosition/fadeBgAlpha);
            if (k < knightPosition)
                knight.anchoredPosition += Vector2.right*k;
            
            if (i < fadeBgAlpha && j < blackLineHeight && k < knightPosition)
                i += 0.1f;
            else
                isBg = true;
        }
        knight.anchoredPosition = new Vector2(knightPosition,knight.anchoredPosition.y);
        fadeBG.color = new Color(0,0,0,fadeBgAlpha);
        blackLineTop.sizeDelta = blackLineBottom.sizeDelta = new Vector2(0, blackLineHeight);
        escTetx.gameObject.SetActive(true);
        DialogStarter();
        yield return null;
    }
    private IEnumerator Dialog()
    {
        foreach (var str in text)
        {
            if(isSkip) break;
            dialogTetx.text = "";
            char[] ch = str.ToCharArray();
            
            foreach (var simbol in ch)
            {
                if(isSkip) break;
                yield return new WaitForSeconds(0.05f);
                dialogTetx.text += simbol;
            }
            yield return new WaitForSeconds(1f);
        }
        gameObject.SetActive(false);
        Player.ST.gameObject.SetActive(true);
        Player.ST._inputs.isActive = true;
        yield return null;
    }
}
