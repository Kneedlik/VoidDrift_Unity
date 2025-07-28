using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_RingOfFire : upgrade
{
    public static SG_RingOfFire instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        if(level == 0)
        {
            ShotGun.RingOfFireCount = 10;
            ShotGun.RingOfFireActive = true;
            description = string.Format("Ring of fire projectiles + 6");
        }else
        {
            ShotGun.RingOfFireCount += 6;
        }

        level++;
    }
}
