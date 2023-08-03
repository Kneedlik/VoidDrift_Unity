using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;





public class HighLight : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    
   public Image image;
    [SerializeField] Image image2;
    public float delay;

    public void OnPointerEnter(PointerEventData eventData)
    {
       // StartCoroutine(wait());
        image.color = Color.white;
        image2.enabled = true;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(wait());
        image.color = Color.green;
        image2.enabled = false;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(delay);
        image.color = Color.white;
    }









}
    

   





