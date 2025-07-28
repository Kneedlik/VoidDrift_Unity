using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_ReloadSpeed : upgrade 
{
    public float ReloadSpeedIncrease;
    public float KnockBackIncrease;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        ShotGun.reloadSpeedMultiplier += ReloadSpeedIncrease;
        ShotGun.knockBackMultiplier += KnockBackIncrease;

    }
}
