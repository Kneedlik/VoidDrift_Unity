using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenMirror : upgrade
{
    [SerializeField] int amount;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.blue;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(ExpirienceB.instance.level == ExpirienceB.instance.maxLevel && levelingSystem.instance.level >= 25)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        PlayerStats.sharedInstance.IncreaseEXP(amount);
        level++;
    }
}
