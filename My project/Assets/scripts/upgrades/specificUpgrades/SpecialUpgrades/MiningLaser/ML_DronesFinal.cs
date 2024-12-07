using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_DronesFinal : upgrade
{
    [SerializeField] int DroneAmount;
    // Start is called before the first frame update

    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (FinalConditionsMet() && ML_Drones.instance.level >= 2)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        MiningLaser Laser = GameObject.FindWithTag("Weapeon").GetComponent<MiningLaser>();
        Laser.DronesFinal = true;
        Laser.DroneAmount += DroneAmount;
        Laser.SetUpDrones();
        Laser.setSideFirepoints();
        level++;
    }
}
