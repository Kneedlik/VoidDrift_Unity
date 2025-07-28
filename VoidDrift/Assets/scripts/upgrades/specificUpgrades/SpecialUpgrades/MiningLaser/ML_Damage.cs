using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_Damage : upgrade
{
    public static ML_Damage instance;
    [SerializeField] float DamageMultiplier;
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
        Laser.damageMultiplier += DamageMultiplier;
        level++;
    }
}
