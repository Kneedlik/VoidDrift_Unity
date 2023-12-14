using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShiv : upgrade
{
    public static StaticShiv Instance;
    int damage;

    private void Start()
    {
        Instance = this;
        Type = type.yellow;
        setColor();
    }

    //public override bool requirmentsMet()
   // {
    //    if(MoveSpeedIncreaseY.instance.level >= 1)
    //    {
    //        return true;
   //     }else return false;
   // }


    public override void function()
    {
        if(level == 0)
        {
            eventManager.OnFire += StaticShivSystem.instance.StaticShivTrigger;
            damage = StaticShivSystem.instance.Damage;
            description = string.Format("Static Shiv damage + 40%"); ;
        }else if(level == 1)
        {
            float pom = damage * 0.4f;
            StaticShivSystem.instance.Damage += (int)pom;
            description = string.Format("Static Shic damage + 60%");
        }else if(level == 2)
        {
            float pom = damage * 0.6f;
            StaticShivSystem.instance.Damage += (int)pom;
        }

        level++;
    }


}
