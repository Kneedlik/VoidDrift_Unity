using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTransferManager : MonoBehaviour
{
    public static DamageTransferManager instance;
    int i;

    void Start()
    {
        instance = this;
    }

    public void TransferDamage(GameObject target, int damage, ref int BonusDamage)
    {
        Health health = target.GetComponent<Health>();
        Transform newTarget;

        if (health != null)
        {
            if(damage > health.health)
            {
                damage -= health.health;
                
                if(KnedlikLib.FindClosestEnemy(gameObject.transform,out newTarget) == false)
                {
                    newTarget = null;
                }else
                {
                    health = newTarget.GetComponent<Health>();
                    if (health != null)
                    {
                        int pom = health.health;
                        health.TakeDamage(damage);
                        damage -= pom;
                    }
                }

                i = 0;
                while(damage > 0 && i < 100)
                {
                    if(KnedlikLib.FindClosestEnemy(gameObject.transform,out newTarget))
                    {
                        health = newTarget.GetComponent<Health>();
                        if (health != null)
                        {
                            int pom = health.health;
                            health.TakeDamage(damage);
                            damage -= pom;
                        }
                        else break;
                    }else
                    {
                        newTarget = null;
                        break;
                    }
                    i++;
                }
            }
        }
    }

    public bool setClosestTarget(out Transform target)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // Transform[] Transforms = new Transform[Enemies.Length];
        Renderer[] renderers = new Renderer[Enemies.Length];

        for (int i = 0; i < Enemies.Length; i++)
        {
            //Transforms[i] = Enemies[i].GetComponent<Transform>();
            renderers[i] = Enemies[i].GetComponent<Renderer>();
        }

        target = Enemies[0].transform;
        int j = 0;


        for (int i = 0; i < Enemies.Length; i++)
        {
            if (renderers[i].isVisible)
            {
                if (Vector3.Distance(Enemies[i].transform.position, player.position) < Vector3.Distance(target.position, player.position))
                {
                    target = Enemies[i].transform;
                    j = i;
                }
            }
        }

        if (renderers[j].isVisible)
        {
            return true;
        }
        else return false;
    }

}
