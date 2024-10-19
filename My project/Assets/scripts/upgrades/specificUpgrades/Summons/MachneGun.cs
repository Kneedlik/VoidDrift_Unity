using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MachneGun : Summon
{ 
    public float force;
    public float BaseForce;
    public int bulletsInBurst;
    public float burstDelay;
    public GameObject BulletPrefab;
    [SerializeField] Transform firePoint;
    public int Pierce;

    Transform target;
   
    float timeStamp;
  
    void Start()
    {
        ScaleSummonStats();
        PlayerStats.OnLevel += ScaleSummonStats;
    }

    void Update()
    {
        if(timeStamp >= 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0)
        {
            
           if(setClosestTarget(out target))
            {
                timeStamp = fireRate;
                StartCoroutine(shoot());
            }
            
        }

        if(target != null)
        {
            faceEnemy(target);
        }

    }

    IEnumerator shoot()
    {
        for (int i = 0; i < bulletsInBurst; i++)
        {
            if (target != null)
            {
                Rigidbody2D rb2 = target.gameObject.GetComponent<Rigidbody2D>();
                GameObject Bullet = Instantiate(BulletPrefab, firePoint.position, Quaternion.identity);
                Bullet.transform.localScale = new Vector3(size,size,0);

                Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();

                if (InterceptionPoint(target.position, transform.position, rb2.velocity, force, out var direction))
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                    rb.rotation = angle;
                    rb.velocity = direction.normalized * force;
                }
                else
                {
                    rb.velocity = (target.transform.position - transform.position).normalized * force;
                }

                Projectile projectile = Bullet.GetComponent<Projectile>();
                projectile.pierce = Pierce;
                projectile.setDamage(damage);

                yield return new WaitForSeconds(burstDelay);
            }
        }
    }

    public override int PrintPowerLevel()
    {
        float PowerLevel = 0;
        float TimeSpend = 0;

        for (int i = 0; i < bulletsInBurst - 1; i++)
        {
            PowerLevel += baseDamage;
            TimeSpend += burstDelay;
        }
        PowerLevel += baseDamage;
        TimeSpend += baseFireRate;
        PowerLevel = PowerLevel / TimeSpend;
        Debug.Log(string.Format("Power level: {0}", (int)PowerLevel));
        return (int)PowerLevel;
    }

    public override void scaleForce()
    {
        force = BaseForce * PlayerStats.sharedInstance.ProjectileForce;
    }



}
