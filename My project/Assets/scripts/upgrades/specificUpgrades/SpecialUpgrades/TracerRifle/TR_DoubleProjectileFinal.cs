using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR_DoubleProjectileFinal : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    //public override bool requirmentsMet()
    //{
    //    if (FinalConditionsMet() && TR_Haste.Instance.level >= 1 && TR_Projectiles.Instance.level >= 1)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    public override void function()
    {
        TracerGun gun = GameObject.FindWithTag("Weapeon").GetComponent<TracerGun>();

        if (gun != null)
        {
            gun.Double = true;
            gun.setFirepoints();
            gun.setSideFirepoints();
        }
    }
}
