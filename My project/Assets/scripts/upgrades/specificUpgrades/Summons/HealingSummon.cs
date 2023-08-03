using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSummon : Summon
{
    public float regenTime = 5;
   float timeStamp;
    public bool transferDamage;
   [SerializeField] Transform target;
    public int damageMultiplier = 1;

    plaerHealth health;


    void Start()
    {
        health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
    }

    
    void Update()
    {
        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0)
        {
            Regenarete();
            timeStamp = regenTime;
        }
    }

    void Regenarete()
    {
        if(health.health < health.maxHealth)
        {
            health.health += damage;
            health.healthBar.SetHealth(health.health);
            Debug.Log("1");
        }else
        {
            if(transferDamage)
            {
                if (setRandomTarget(out target))
                {             
                    Health healthE = target.GetComponent<Health>();
                    healthE.TakeDamage(damage);

                } else
                {
                    target = null;
                }
            }
        }
    }

    public float CalculateHealing()
    {
        float healPerSec = damage / regenTime;

        return healPerSec;
    }

    public override void scaleSummonDamage()
    {

       float pom =  (baseDamage * damageMultiplier)* (PlayerStats.sharedInstance.SummonDamage / 100);
        pom = pom * (PlayerStats.sharedInstance.damageMultiplier / 100);
        damage = (int)pom;
    }


}
