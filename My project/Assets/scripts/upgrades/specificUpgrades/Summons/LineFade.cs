using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFade : MonoBehaviour
{
    LineRenderer line;
    public float duration;
    public float Delay;
    float pom;
    float percent;

    Color32 color;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        color = new Color32(255, 255, 255, 255);
        pom = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if(Delay > 0)
        {
            Delay -= Time.deltaTime;
        }

        if(Delay <= 0 && pom > 0)
        {
            pom -= Time.deltaTime;
            if(pom < 0)
            {
                pom = 0;
            }
        }

        percent = (pom / duration) * 255f;
        int temp = (int)percent;
        color = new Color32(255,255,255,(byte)temp);

        line.endColor = color;
        line.startColor = color;
    }
}
