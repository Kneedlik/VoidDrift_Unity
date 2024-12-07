using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterOnDeath : upgrade
{
    GameObject BulletPrefab;
    public int amount;
    float multiplier;
    weapeon W;

    private void Start()
    {
        Type = type.red;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(levelingSystem.instance.red >= 5)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        if (level == 0)
        {
            multiplier = 0.4f;
            W = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();
            BulletPrefab = W.GetProjectile();
            eventManager.PostImpact += OnDeathScatter;
            description = string.Format("Scatter shot bullets now deal 60% damage");
        }else if (level == 1)
        {
            multiplier = 0.6f;
        }

        level++;
    }

    public void OnDeathScatter(GameObject Target,int damage, ref int Damage)
    {
        BulletPrefab = W.GetProjectile();
        if(BulletPrefab == null)
        {
            return;
        }

        if (Target != null)
        {
            Health health = Target.GetComponent<Health>();
            if((health.health + health.armor) <= damage)
            {
                float Angle = 360f / (float)amount;
                int rand = Random.Range(0, 90);
                float temp = rand;

                weapeon Weapeon = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();
                float Force = Weapeon.Force;

                for (int i = 0; i < amount; i++)
                {
                    GameObject Bullet = Instantiate(BulletPrefab, Target.transform.position, Quaternion.Euler(0, 0, 0));
                    Projectile p =  Bullet.GetComponent<Projectile>();
                    float pom = damage * multiplier;
                    p.damagePlus = (int)pom;


                    Bullet.transform.rotation = Quaternion.Euler(0, 0, temp);
                    temp += Angle;
                    Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Bullet.transform.up * Force, ForceMode2D.Impulse);
                }
            }
        }
    }
}
