using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_FireRate : upgrade
{
    public static SG_FireRate instance;
    public int AmmoAmount;
    public float ASAmount;
    
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
        ShotGun.magSize += AmmoAmount;
        ShotGun.ASmultiplier += ASAmount;

        //PlayerStats.sharedInstance.IncreaseAS(0);

        level++;
    }
}
