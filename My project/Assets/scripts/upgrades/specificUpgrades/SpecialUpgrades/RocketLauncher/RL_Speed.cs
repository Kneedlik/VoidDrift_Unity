using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_Speed : upgrade
{
    [SerializeField] float SpeedMultiplier;
    [SerializeField] int ImpactDamageAmount;
    [SerializeField] int ImpactDamagePlus;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        rocketLauncher RocketLauncher = GameObject.FindWithTag("Weapeon").GetComponent<rocketLauncher>();
        RocketLauncher.SpeedMultiplier += SpeedMultiplier;

        if (level == 0)
        {
            RocketLauncher.ImpactDamage += ImpactDamageAmount;
            description = string.Format("Rockets impact damage + {0} on Impact Rockets Speed + {0}%",ImpactDamagePlus,SpeedMultiplier * 100);
        }else
        {
            RocketLauncher.ImpactDamage += ImpactDamagePlus;
        }
        level++;
    }
}
