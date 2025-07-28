using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeLight : MonoBehaviour
{
    [SerializeField] float time;
   new Light2D light;
    float timeStamp;
    float intensity;

    void Start()
    {
        light = GetComponent<Light2D>();
        timeStamp = time;
        intensity = light.intensity;
    }

    
    void Update()
    {
       if(timeStamp > 0)
       {
            timeStamp -= Time.deltaTime;
       }

       if(light.intensity > 0)
        {
            float pom = timeStamp / intensity;
            light.intensity = intensity * pom;
        }else
        {
            this.enabled = false;
        }

       
    }
}
