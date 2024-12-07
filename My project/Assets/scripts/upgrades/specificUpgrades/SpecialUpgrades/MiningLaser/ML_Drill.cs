using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_Drill : upgrade
{
    public static ML_Drill instance; 
    [SerializeField] DrillSystem drillSystem;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {
            eventManager.ImpactGunOnlyHitScan += drillSystem.DrillProc;
            drillSystem.enabled = true;
            drillSystem.ExtraDamage = 4;
            description = string.Format("Every drill stack now causes enemies to take + 5% damage Drill extra damage + 4");

        }else if(level == 1)
        {
            drillSystem.ExtraDamage += 4;
            drillSystem.DamageMultiplier += 0.05f;
            description = string.Format("Drill extra damage + 3 Drill damage + 5%");
        
        }else
        {
            drillSystem.ExtraDamage += 4;
            drillSystem.DamageMultiplier += 0.05f;
        }
        level++;
    }
}
