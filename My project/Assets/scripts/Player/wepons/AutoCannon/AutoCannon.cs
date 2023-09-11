using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoCannon : weapeon
{
    public GameObject bulletPrefab;
    
    public float rapidFireDelay;
    public int bulletsInABurst;

    private float timeStamp;
    private bool shootCheck;
    Rigidbody2D rb;

    public float range;
    public float baseOffset;
    public float baseSideOffset;
    public GameObject[] cubes = new GameObject[100];
    GameObject[] sideCubes = new GameObject[100];
    public GameObject cubePrefab;

    public int Bounce;

    private void Start()
    {
        cubes = new GameObject[100];
        timeStamp = 0;
        shootCheck = false;
        Force = BaseForce;
        updateDamage(100);
        updateSize(100);
        updateAS(100);
        setFirepoints();
    }

    void Update()
    {
        

        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1"))
        {
            shootCheck = true;
        }
        else
        {
            shootCheck = false;
        }

        if (shootCheck)
        {
            if (timeStamp <= 0)
            {
                StartCoroutine(Shoot());
                timeStamp = bulletCoolDown;
            }
        }
    }

    IEnumerator Shoot()
    {
        
        for (int i = 0; i < bulletsInABurst; i++)
        {
            int pom = extraDamage;

            if(eventManager.OnFire != null)
            {
                eventManager.OnFire(gameObject);
            }
            
            for (int j = 0; j < projectileCount; j++)
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, cubes[j].transform.position, firePoint.rotation);

                if(eventManager.OnFireAll != null)
                {
                    eventManager.OnFireAll(gameObject,bullet);
                }

                bulletDamage = bullet.GetComponent<Projectile>();
                bulletDamage.setDamage(damage + extraDamage);
                bulletDamage.setArea(size);
                bulletDamage.setPierce(pierce);
                bulletDamage.setKnockBack(knockBack);
                bulletDamage.setBounce(Bounce);

                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * Force, ForceMode2D.Impulse);
            }

            for (int j = 0; j < sideProjectiles * 2; j++)
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, sideCubes[j].transform.position, sideCubes[j].transform.rotation);

                if (eventManager.OnFireAll != null)
                {
                    eventManager.OnFireAll(gameObject, bullet);
                }

                bulletDamage = bullet.GetComponent<Projectile>();
                bulletDamage.setDamage(damage + extraDamage);
                bulletDamage.setArea(size);
                bulletDamage.setPierce(pierce);
                bulletDamage.setKnockBack(knockBack);
                bulletDamage.setBounce(Bounce);

                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(sideCubes[j].transform.up * Force, ForceMode2D.Impulse);
            }

            extraDamage = pom;
            yield return new WaitForSeconds(rapidFireDelay);
        }
    }

    public override void setSideFirepoints()
    {
       // float mid = 45;
        float offset = baseSideOffset / sideProjectiles;
        float offsetA = offset;

        for (int i = 0; i < sideProjectiles * 2; i++)
        {
            Destroy(sideCubes[i]);
        }

        for (int i = 0; i < sideProjectiles * 2; )
        {
            sideCubes[i] = Instantiate(cubePrefab,SidefirePoint1.position,Quaternion.Euler(0,0,(offsetA * -1) + 270));
            sideCubes[i].transform.parent = gameObject.transform;
            i++;
            sideCubes[i] = Instantiate(cubePrefab, SidefirePoint2.position, Quaternion.Euler(0,0,offsetA + 270));
            sideCubes[i].transform.parent = gameObject.transform;
            i++;
            offsetA += offset;
        }
    }

    public override void setFirepoints()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Destroy(cubes[i]);
        }

        Vector2 points = new Vector2(firePoint.position.x, firePoint.position.y);
        float offset1 = 0;
        int j = 0;
        int a;
        float offset = baseOffset / projectileCount;


        if (projectileCount % 2 == 1)
        {
            points.x += range;

            cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
            cubes[j].transform.parent = gameObject.transform;
            j++;

        }
        else
        {
            points.x += range;
            offset1 += (offset / 2);
            points.y = firePoint.position.y + offset1;
            cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
            cubes[j].transform.parent = gameObject.transform;
            j++;
            points.y = firePoint.position.y - offset1;
            cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
            cubes[j].transform.parent = gameObject.transform;
            j++;
        }
        int count = j;
        for (int i = 0; i < (projectileCount - count); i++)
        {
            i++;
            offset1 += offset;

            points.y = firePoint.position.y + offset1;

            float distance = Vector2.Distance(firePoint.position, points);
            a = 0;

            while (distance > range && a < 1000)
            {
                a++;
                points.x -= 0.3f;
                distance = Vector2.Distance(firePoint.position, points);
            }
            cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
            cubes[j].transform.parent = gameObject.transform;
            j++;

            points.y = firePoint.position.y - offset1;

            cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
            cubes[j].transform.parent = gameObject.transform;
            j++;

        }
    }

    public override void ResetFirePoints()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Destroy(cubes[i]);
        }
        
    }

    public override GameObject GetProjectile()
    {
        return bulletPrefab;
    }
}
