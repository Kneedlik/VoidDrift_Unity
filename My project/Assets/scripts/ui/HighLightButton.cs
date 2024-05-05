using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighLightButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image image;
    public float delay;
    public Color32 highLightColor;
    Color32 baseColor;

    private void Start()
    {
        baseColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // StartCoroutine(wait());
        image.color = highLightColor;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(wait());
        image.color = baseColor;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(delay);
        image.color = baseColor;
    }

   // public void OnPointerClick(PointerEventData eventData)
   // {
   //     StartCoroutine(wait());
   //     image.color = baseColor;
   // }
}
