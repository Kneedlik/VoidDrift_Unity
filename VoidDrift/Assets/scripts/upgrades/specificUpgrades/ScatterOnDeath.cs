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
            multiplier = 0.25f;
            W = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();
            BulletPrefab = W.GetProjectile();
            eventManager.PostImpact += OnDeathScatter;
            description = string.Format("Scatter shot bullets now deal 40% damage");
        }else if (level == 1)
        {
            multiplier = 0.4f;
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
            if((health.health + health.armor) <= damage * health.multiplier)
            {
                float Angle = 360f / (float)amount;
                int rand = Random.Range(0, 90);
                float temp = rand;

                float Force = W.Force;

                for (int i = 0; i < amount; i++)
                {
                    GameObject Bullet = Instantiate(BulletPrefab, Target.transform.position, Quaternion.Euler(0, 0, 0));
                    BulletScript p =  Bullet.GetComponent<BulletScript>();
                    float pom = W.damage * multiplier;
                    p.damagePlus = (int)pom;
                    p.PostImpact = false;
                    Debug.Log((int)pom);

                    Bullet.transform.rotation = Quaternion.Euler(0, 0, temp);
                    temp += Angle;
                    Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Bullet.transform.up * Force, ForceMode2D.Impulse);
                }
            }
        }
    }
}
