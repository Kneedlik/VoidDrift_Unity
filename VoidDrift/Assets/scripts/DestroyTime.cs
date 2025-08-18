
using UnityEngine;
using TMPro;

public class DestroyTime : MonoBehaviour
{
    public float destroyTime;
    public float fadeTime;
    //private Color textOpacity;
    public TMP_Text textM;
    
    void Start()
    {
        Destroy(gameObject, destroyTime);
        //textM = this.GetComponent<TMP_Text>();
        //textOpacity = textM.color;
        
    }

    //private void Update()
    //{
    //    textOpacity.a -= fadeTime * Time.deltaTime;
    //    textM.color = textOpacity;
    //}


}
