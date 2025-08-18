using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberMaster : MonoBehaviour
{
    public int NumberCap;
    public int Count;
    public bool Full;

    public void UpdateCounter()
    {
        Count = transform.childCount;
        if (Count >= NumberCap)
        {
            Full = true;
        }
        else Full = false;
    }
}
