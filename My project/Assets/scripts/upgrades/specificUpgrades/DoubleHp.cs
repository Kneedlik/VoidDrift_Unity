using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleHp : upgrade
{
    
    void Start()
    {
        Type = type.green;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (levelingSystem.instance.green >= 15 && levelingSystem.instance.level >= 25)
        {
            return true;
        }
        else return false;
    }


    public override void function()
    {
        if(plaerHealth.Instance.flash != null)
        {
            plaerHealth.Instance.StopCoroutine(plaerHealth.Instance.flash);
        }
        plaerHealth.Instance.fill.color = new Color32(0, 255, 45, 255);

        plaerHealth.Instance.half = true;


        level++;
    }
}
