using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDamage : upgrade
{
    public float Amount;

    
    void Start()
    {
        Type = type.purple;
        setColor();
    }

    public override bool requirmentsMet()
    {
       if(levelingSystem.instance.purple >= 15)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        eventManager.PostImpact += globalDamage;

        level++;
    }

    public void globalDamage(GameObject target, int damage, ref int bonusDamage)
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Renderer[] renderers = new Renderer[Enemies.Length];

        for (int i = 0; i < Enemies.Length; i++)
        {
            //Transforms[i] = Enemies[i].GetComponent<Transform>();
            renderers[i] = Enemies[i].GetComponent<Renderer>();
        }

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (renderers[i].isVisible)
            {
                Health health = Enemies[i].GetComponent<Health>();
                if(health != null)
                {
                    float pom = damage * Amount;
                    health.TakeDamage((int)pom);
                }
            }
        }
    }
}
