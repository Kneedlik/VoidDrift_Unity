using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberMaster : MonoBehaviour
{
    public int NumberCap;
    public int Count;
    public bool Full;

    // Update is called once per frame
    void Update()
    {
        Count = transform.childCount;
        if(Count >= NumberCap)
        {
            Full = true;
        }else Full = false;
    }
}
