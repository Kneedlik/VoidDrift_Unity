using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachneGun : Summon
{ 
    public float force;
    public int bulletsInBurst;
    public float burstDelay;
    public GameObject BulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] int Pierce;

    Transform target;
   
    float timeStamp;
  
    void Start()
    {
        scaleSummonDamage();
        scaleSize();
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
                    rb.velocity = direction * force;
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

    

   

}
