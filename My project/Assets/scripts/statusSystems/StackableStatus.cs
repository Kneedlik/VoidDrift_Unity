using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StackableStatus 
{
    public int Amount;
    public float TimeLeft;
    public GameObject Vfx;

    public StackableStatus(int amount,float timeleft, GameObject vfx = null)
    {
        Amount = amount;
        TimeLeft = timeleft;
        Vfx = vfx;
    }
}
