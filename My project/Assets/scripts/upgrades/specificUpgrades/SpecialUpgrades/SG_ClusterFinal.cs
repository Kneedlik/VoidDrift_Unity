using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_ClusterFinal : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(levelingSystem.instance.FinallForm == false && SG_Cluster.instance.level >= 2)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        levelingSystem.instance.FinallForm = true;
        ShotGun.ClusterAmount = 2;
        
        level++;
    }
}
