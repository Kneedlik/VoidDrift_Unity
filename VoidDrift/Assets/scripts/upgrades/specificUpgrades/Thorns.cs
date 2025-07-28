using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : upgrade
{
   [SerializeField] int amount;
     int index;
    public int ticksNeeded = 5;
    public bool infiniteScaling = false;
    public bool onHit = false;

    void Start()
    {
        Type = type.green;
        setColor();
    }

    public override void function()
    {
        
        if(level == 0)
        {
            PlayerStats.sharedInstance.thorns += amount;
            eventManager.OnDamage += thonsActivate;
            PlayerStats.sharedInstance.thorns += amount;
        }else if(level == 1)
        {
            PlayerStats.sharedInstance.thorns += amount;
            description = string.Format("Thorns now activate on hit effects");
        }else if(level == 2)
        {
            onHit = true;
            description = string.Format("Thorns + {0}", amount);
        }
        else if(level == 3)
        {
            PlayerStats.sharedInstance.thorns += amount;
            description = string.Format("For every fifth enemy killed by thorns thorns + 1");
        }else  if(level == 4)
        {
            infiniteScaling = true;
            description = string.Format("Thorns + {0}",amount);
        }else
        {
            PlayerStats.sharedInstance.thorns += amount;
        }

        level++;
    }

    public void thonsActivate(int amount)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Transform target;

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // Transform[] Transforms = new Transform[Enemies.Length];
        Renderer[] renderers = new Renderer[Enemies.Length];

        for (int i = 0; i < Enemies.Length; i++)
        {
            //Transforms[i] = Enemies[i].GetComponent<Transform>();
            renderers[i] = Enemies[i].GetComponent<Renderer>();
        }


        target = Enemies[0].transform;

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (renderers[i].isVisible)
            {
                if (Vector3.Distance(Enemies[i].transform.position, player.position) < Vector3.Distance(target.position, player.position))
                {
                    target = Enemies[i].transform;

                }
            }
        }

        if (target != null)
        {
            Health healt = target.GetComponent<Health>();
            if (healt != null)
            {
                float pom = PlayerStats.sharedInstance.thorns * (PlayerStats.sharedInstance.damageMultiplier / 100f);
                int pom1 = (int)pom;

                if (onHit && eventManager.OnImpact != null)
                {
                    eventManager.OnImpact(target.gameObject, (int)pom, ref pom1);
                }
              
                if(infiniteScaling && healt.health + healt.armor <= pom1)
                {
                    PlayerStats.sharedInstance.thorns++;
                }
                healt.TakeDamage(pom1);

            }
        }

    }
}
