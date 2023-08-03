using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioHammer : Summon
{
    float timeStamp;
    public float damageDelay;
 
    Transform target;
    public float healthScaling = 1;

    private void Start()
    {
        scaleSize();
        scaleSummonDamage();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeStamp >= 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0)
        {
            if(target != null)
            {
                timeStamp = fireRate;
                Invoke("shoot", damageDelay);
            }else if(setRandomTarget(out target) == false)
             {
                target = null;
             }
            
        }
    }

    void shoot()
    {
       Health health = target.GetComponent<Health>();
        int plusDamage = damage;

        if(health != null)
        {
           
            if (eventManager.OnImpact != null)
            {
                eventManager.OnImpact(target.gameObject, damage,ref plusDamage);
            }

            if(eventManager.PostImpact != null)
            {
                eventManager.PostImpact(target.gameObject, plusDamage,ref plusDamage);
            }
            health.TakeDamage(damage);
        }
    }

   public override void scaleSummonDamage()
    {
       plaerHealth health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
       int diff = health.health - health.baseHealth;
        float pom = diff * healthScaling;
        pom = baseDamage + pom;

         pom = pom * (PlayerStats.sharedInstance.SummonDamage / 100);
        pom = pom * (PlayerStats.sharedInstance.damageMultiplier / 100);
        damage = (int)pom;

    }
}
