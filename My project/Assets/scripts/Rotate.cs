using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Rot;
    public float Speed;
 

    void Update()
    {
        Rot += Time.deltaTime * Speed;
        if(Rot > 360)
        {
            Rot = 0;
        }
        transform.rotation = Quaternion.Euler(0,0,Rot);
    }
}
