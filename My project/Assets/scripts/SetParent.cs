using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    Transform minimap;

    void Start()
    {
        minimap = GameObject.FindWithTag("MapIcons").GetComponent<Transform>();
        transform.parent = minimap;
    }

    
   
}
