using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HighlightText : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text;
    public float delay;
    public Color32 highLightColor;
    public float highLightFont;
    Color32 baseColor;
    float baseFont;

    private void Start()
    {
        baseColor = text.color;
        baseFont = text.fontSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // StartCoroutine(wait());
        //Debug.Log("In");
        text.color = highLightColor;
        text.fontSize = highLightFont;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //StartCoroutine(wait());
        //Debug.Log("Out");
        text.color = baseColor;
        text.fontSize = baseFont;
    }
}
