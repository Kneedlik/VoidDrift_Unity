using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR_SummonFinal : upgrade
{
    public int SummonAmount;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(FinalConditionsMet() && TR_Summon.instance.level >= 2)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        TracerGun gun = GameObject.FindWithTag("Weapeon").GetComponent<TracerGun>();
        gun.SummonFinal = true;
        gun.SummonCount += SummonAmount;
        gun.setFirepoints();
        gun.setSideFirepoints();
    }
}
