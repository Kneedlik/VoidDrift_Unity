using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Cluster : upgrade
{
    public static SG_Cluster instance;
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
        if (level == 0)
        {
            ShotGun.ClusterAmount = 1;
            //ShotGun.ClusterProjectiles = 5;
            //ShotGun.ClusterDamageMultiplier = 0.35f;
            description = string.Format("Cluster amount + 1 Cluster damage multiplier + 15%");
        }else if (level <= 3)
        {
            ShotGun.ClusterProjectiles += 1;
            ShotGun.ClusterDamageMultiplier = 0.15f;
            description = string.Format(" Cluster damage multiplier + 15%");
        }else
        {
            ShotGun.ClusterDamageMultiplier += 0.15f;
        }
        level++;
    }
}
