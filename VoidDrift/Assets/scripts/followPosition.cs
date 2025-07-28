using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPosition : MonoBehaviour
{
    public GameObject obj;
    public float offsetX = 0;
    public float offsetY = 0;
    private Vector3 pos;


    // Update is called once per frame
    void Update()
    {
        if (obj != null)
        {
            pos = obj.transform.position;
            pos.x += offsetX;
            pos.y += offsetY;

            this.transform.position = pos;
        }
       // else Destroy(gameObject);

        
    }
}
