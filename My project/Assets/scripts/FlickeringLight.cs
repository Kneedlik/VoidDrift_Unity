using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    Light2D light;
    [SerializeField] float range;
    [SerializeField] float speed;
    float t = 0;

     float intensity;
    void Start()
    {
        light = GetComponent<Light2D>();
        intensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Mathf.Lerp(range, intensity, t);
        t += speed * Time.deltaTime;

        if(t > 1)
        {
            float pom = intensity;
            intensity = range;
            range = pom;
            t = 0;
        }

        

      
        
    }
}
