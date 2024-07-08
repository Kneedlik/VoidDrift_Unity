using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_Drones : upgrade
{
    public static ML_Drones instance;
    [SerializeField] int Amount;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        MiningLaser Laser = GameObject.FindWithTag("Weapeon").GetComponent<MiningLaser>();
        Laser.DroneAmount += Amount;
        Laser.SetUpDrones();

        if(level == 0)
        {
            description = string.Format("Drones + {0}",Amount);
        }

        level++;
    }
}
