using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSummon : Summon
{
   [SerializeField] GameObject bullet;
    float timeStamp;
    public int projectileAmount;
    public float bulletForce;
    public float BaseForce;
    public int bursts;
    int burstIndex;
    bool flip;
    public float burstDelay;
   public int pierce;

    void Start()
    {
        burstIndex = 0;
        flip = false;
        ScaleSummonStats();
        PlayerStats.OnLevel += ScaleSummonStats;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeStamp > 0 )
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0)
        {
            shoot();
        }
    }

   void shoot()
    {
        float angle = 360 / projectileAmount;
        float pom = angle;

        if (flip)
        {
            angle += 45f;
        }

        for (int i = 0; i < projectileAmount; i++)
        {
            GameObject B = Instantiate(bullet,transform.position,Quaternion.Euler(0,0,angle));
            B.transform.localScale = new Vector3(size, size, 1);

            Projectile projectile = B.GetComponent<Projectile>();
            projectile.pierce = pierce;
            projectile.setDamage(damage);
            projectile.setArea(size);


            Rigidbody2D rb = B.GetComponent<Rigidbody2D>();
            rb.velocity = B.transform.up * bulletForce;

            angle += pom;
        }
        burstIndex++;

        if (burstIndex == bursts)
        {
            burstIndex = 0;
            flip = false;
            timeStamp = fireRate;
        } else
        {
            if(flip)
            {
                flip = false;
            }else
            {
                flip = true;
            }

            timeStamp = burstDelay;
        } 
   }
    public override void scaleForce()
    {
        bulletForce = BaseForce * PlayerStats.sharedInstance.ProjectileForce;
    }

    public override int PrintPowerLevel()
    {
        float PowerLevel = 0;
        float TimeSpend = 0;

        for (int i = 0; i < bursts - 1; i++)
        {
            PowerLevel += baseDamage;
            TimeSpend += burstDelay;
        }
        PowerLevel += baseDamage;
        PowerLevel = PowerLevel * projectileAmount;
        TimeSpend += baseFireRate;
        PowerLevel = PowerLevel / TimeSpend;
        Debug.Log(string.Format("Power level: {0}", (int)PowerLevel));
        return (int)PowerLevel;
    }


}
