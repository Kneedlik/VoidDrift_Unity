using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioHammer : Summon
{
    float timeStamp;
    public float damageDelay;
 
    Transform target;
    public float healthScaling = 1;
    public float TrueDamage;
    public bool Aoe;
    public GameObject ExploObj;

    [SerializeField] GameObject DamageEffect;

    private void Start()
    {
        ScaleSummonStats();
        PlayerStats.OnLevel += ScaleSummonStats;
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
                if (Aoe)
                {
                    GameObject Obj = Instantiate(ExploObj, target.position, Quaternion.Euler(0, 0, 0));
                    explosion Explo = Obj.GetComponent<explosion>();
                    Explo.damage = plusDamage;
                    Explo.TrueDamage = TrueDamage;
                }
                else
                {
                    if (eventManager.SummonOnImpact != null)
                    {
                        eventManager.SummonOnImpact(target.gameObject, damage, ref plusDamage);
                    }

                    float Temp = health.maxHealth * TrueDamage;
                    health.TakeDamage(plusDamage + (int)Temp);
                }
            }
        }
    }

   public override void scaleSummonDamage()
   {
       plaerHealth health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
       int diff = health.maxHealth - health.baseHealth;
        float pom = diff * healthScaling;
        pom = baseDamage + pom;

        pom = pom * (PlayerStats.sharedInstance.SummonDamage / 100) * MasterManager.Instance.PlayerInformation.SummonDamageMultiplier;
        pom = pom * (PlayerStats.sharedInstance.damageMultiplier / 100)  * MasterManager.Instance.PlayerInformation.DamageMultiplier;
        damage = (int)pom;

   }
}
