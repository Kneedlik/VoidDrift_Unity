using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class showTime : MonoBehaviour
{
   TextMeshProUGUI textMeshProUGUI;
   

   [SerializeField] bool onEnable = false;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(onEnable == false)
        {
            setTime(timer.instance.gameTime);
        }
    }

    private void OnEnable()
    {
        if (onEnable)
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            setTime(timer.instance.gameTime);
        }
    }

    void setTime(float Time)
    {
        float minutes = Mathf.FloorToInt(Time / 60);
        float seconds = Mathf.FloorToInt(Time % 60);

        textMeshProUGUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
