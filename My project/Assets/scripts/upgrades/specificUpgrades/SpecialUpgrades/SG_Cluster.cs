using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Cluster : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        if (level == 0)
        {
            ShotGun.ClusterAmount = 1;
            ShotGun.ClusterProjectiles = 5;
            ShotGun.ClusterDamageMultiplier = 0.35f;
        }else if (level == 1)
        {
            ShotGun.ClusterProjectiles = 6;
            ShotGun.ClusterDamageMultiplier = 0.5f;
        }else
        {
            ShotGun.ClusterDamageMultiplier += 0.15f;
        }
        level++;
    }
}
