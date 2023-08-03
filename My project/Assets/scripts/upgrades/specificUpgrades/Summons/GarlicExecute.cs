using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicExecute : upgrade
{
    public static GarlicExecute instance;

    void Awake()
    {
        instance = this;
        Type = type.yellow;
        setColor();
        //yellow red
    }

    public override bool requirmentsMet()
    {
        GarlicYellow[] pom = FindObjectsOfType<GarlicYellow>();
        bool temp = false;

        for (int i = 0; i < pom.Length; i++)
        {
            if (pom[i].level >= pom[i].maxLevel)
            {
                temp = true;
            }
        }

        if (levelingSystem.instance.red >= 10 && temp)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        GarlicMain[] main = FindObjectsOfType<GarlicMain>();

        for (int i = 0; i < main.Length; i++)
        {
            main[i].execute = true;
        }


        level++;
    }
}
