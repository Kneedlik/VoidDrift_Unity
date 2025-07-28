using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateIcons : MonoBehaviour
{
    SpriteRenderer[] icons;

    void Start()
    {
        GameObject[] pom = GameObject.FindGameObjectsWithTag("MapIcon");
        icons = new SpriteRenderer[pom.Length];

        for (int i = 0; i < pom.Length; i++)
        {
            icons[i] = pom[i].GetComponent<SpriteRenderer>();
            icons[i].enabled = true;
           
        }
    }

    
    void Update()
    {
        
    }
}
