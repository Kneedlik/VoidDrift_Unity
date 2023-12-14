using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioHammer : Summon
{
    float timeStamp;
    public float damageDelay;
 
    Transform target;
    public float healthScaling = 1;

    [SerializeField] GameObject DamageEffect;

    private void Start()
    {
        scaleSize();
        scaleSummonDamage();
        PlayerStats.OnLevel += scaleSummonDamage;
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
                StartCoroutine(Shoot(target));
            }else if(setRandomTarget(out target) == false)
             {
                target = null;
             }
            
        }
    }

    IEnumerator Shoot(Transform target)
    {
        Transform pom = target;
        GameObject obj = Instantiate(DamageEffect,pom.position,Quaternion.Euler(0,0,0));
        obj.transform.SetParent(pom);

        yield return new WaitForSeconds(damageDelay);

        if (pom != null)
        {
            Health health = pom.GetComponent<Health>();
            int plusDamage = damage;

            if (health != null)
            {
                if (eventManager.SummonOnImpact != null)
                {
                    eventManager.SummonOnImpact(target.gameObject, damage, ref plusDamage);
                }

                //  if (eventManager.PostImpact != null)
                // {
                //     eventManager.PostImpact(target.gameObject, plusDamage, ref plusDamage);
                // }
                health.TakeDamage(damage);
            }
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
