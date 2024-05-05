using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

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
    public float OffsetScaling = 1;
    public float RangeScaling;
    public float MaxOffsetScaling;
    public float FirePointRotation;
    public float MaxFirePointRotation;
    [SerializeField] float offset;
    [SerializeField] float Range1;

    public float baseSideOffset;
    public float sideScaling = 1;
    public float MaxSideScaling;

    public bool Laser;
    public bool Raiden;
   [SerializeField] int RealProjectiles;
    BulletScript BulletDamage;
   

    public GameObject[] cubes = new GameObject[100];
    GameObject[] sideCubes = new GameObject[100];
    public GameObject cubePrefab;

    public int Bounce;
    public float BounceDistance;
    public bool BounceDistanceOff = false;

    private void Start()
    {
        startingDamage = baseDamage;
        StartingForce = BaseForce;
        StartingBulletCooldown = baseBulletCoolDown;
        StartingSize = baseSize;

        PlayerStats.OnLevel += SetAS;
        PlayerStats.OnLevel += SetForce;

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

            AudioManager.instance.PlayName("Autocannon4");
            if(eventManager.OnFire != null)
            {
                eventManager.OnFire(gameObject);
            }
            
            for (int j = 0; j < RealProjectiles; j++)
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, cubes[j].transform.position, cubes[j].transform.rotation);

                if(eventManager.OnFireAll != null)
                {
                    eventManager.OnFireAll(gameObject,bullet);
                }

                BulletDamage = bullet.GetComponent<BulletScript>();
                BulletDamage.setDamage(damage + extraDamage);
                BulletDamage.setArea(size);
                BulletDamage.setPierce(pierce);
                BulletDamage.setKnockBack(knockBack);
                BulletDamage.setBounce(Bounce);
                BulletDamage.MaxBounceDistance = BounceDistance;

                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bullet.transform.up * Force, ForceMode2D.Impulse);
            }

            for (int j = 0; j < sideProjectiles * 2; j++)
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, sideCubes[j].transform.position, sideCubes[j].transform.rotation);

                if (eventManager.OnFireAll != null)
                {
                    eventManager.OnFireAll(gameObject, bullet);
                }

                BulletDamage = bullet.GetComponent<BulletScript>();
                BulletDamage.setDamage(damage + extraDamage);
                BulletDamage.setArea(size);
                BulletDamage.setPierce(pierce);
                BulletDamage.setKnockBack(knockBack);
                BulletDamage.setBounce(Bounce);
                BulletDamage.MaxBounceDistance = BounceDistance;               

                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(sideCubes[j].transform.up * Force, ForceMode2D.Impulse);
            }

            extraDamage = pom;
            yield return new WaitForSeconds(rapidFireDelay);
        }
    }

    public override void setSideFirepoints()
    {
       
        float offset = baseSideOffset / sideProjectiles;
        offset = offset + (sideProjectiles * sideScaling) / sideProjectiles;
        if(offset > MaxSideScaling && MaxSideScaling != 0)
        {
            offset = MaxSideScaling;
        }
        float offsetA = offset * 2;

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
        Transform Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
         Range1 = range;

        RealProjectiles = projectileCount;
        if (RealProjectiles < 1)
        {
            RealProjectiles = 1;
        }

        offset = baseOffset / RealProjectiles;
        float pom = offset;

        if (RealProjectiles > 3)
        {
            offset = offset + RealProjectiles * OffsetScaling;
            Range1 = Range1 + RealProjectiles * RangeScaling;
        }

        if (Range1 > range * MaxOffsetScaling)
        {
            Range1 = range * MaxOffsetScaling;
        }

        if (offset > pom * MaxOffsetScaling)
        {
            offset = offset + pom * OffsetScaling;
        }

        bool Finished = false;
        int b = 0;       

        if (Raiden == false)
        {
            if (Laser)
            {
                offset = 0.05f;
                Range1 = 2;
            }

            while (Finished == false && b < 100)
            {
                firePoint.position = Player.position;
                firePoint.position = new Vector3(Player.position.x - 2 + (Range1 * -0.15f), Player.position.y, 0);

                float rot = 0;
                float offset1 = 0;
                int a;
                int j = 0;
                Vector2 points = new Vector2(firePoint.position.x, firePoint.position.y);
                Finished = true;
                b++;

                for (int i = 0; i < RealProjectiles; i++)
                {
                    if (cubes[i] != null)
                    {
                        Destroy(cubes[i]);
                    }
                }

                if (RealProjectiles % 2 == 1)
                {
                    points.x += Range1;

                    cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
                    cubes[j].transform.parent = gameObject.transform;
                    j++;

                }
                else
                {
                    points.x += Range1;
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
                for (int i = 0; i < (RealProjectiles - count); i++)
                {
                    i++;
                    offset1 += offset;

                    float distance;
                    a = 0;

                    points.y = firePoint.position.y + offset1;
                    distance = Vector2.Distance(firePoint.position, points);

                    while (distance > Range1 && a < 250)
                    {
                        a++;
                        points.x -= 0.3f;
                        distance = Vector2.Distance(firePoint.position, points);
                    }

                    if (a >= 249)
                    {
                        offset -= 0.05f;
                        Range1 += 0.2f;
                        Finished = false;
                    }

                    rot += FirePointRotation;
                    if (rot > MaxFirePointRotation)
                    {
                        rot = MaxFirePointRotation;
                    }  
                    
                    if(Laser)
                    {
                        rot = 0;
                    }

                    cubes[j] = Instantiate(cubePrefab, points, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + rot));
                    cubes[j].transform.parent = gameObject.transform;
                    j++;

                    points.y = firePoint.position.y - offset1;

                    cubes[j] = Instantiate(cubePrefab, points, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z - rot));
                    cubes[j].transform.parent = gameObject.transform;
                    j++;


                }

            }

        }else
        {
            int Set;
            int SetAmount;            

            if(RealProjectiles <= 12 )
            {
                Set = 2;
                if(RealProjectiles % 2 == 1)
                {
                    RealProjectiles -= 1;                   
                }
                SetAmount = RealProjectiles / 2;
            }else if(RealProjectiles <= 18 )
            {
                Set = 3;
                if(RealProjectiles % 3 == 1)
                {
                    RealProjectiles -= 1;
                }
                SetAmount = RealProjectiles / 3;
            }
            else
            {
                Set = 4;
                if (RealProjectiles % 2 == 1)
                {
                    RealProjectiles -= 1;
                }
                SetAmount = RealProjectiles / 4;
                bulletsInABurst = 4;
            }

            while (Finished == false && b < 100)
            {
                firePoint.position = Player.position;
                firePoint.position = new Vector3(Player.position.x - 2 + (Range1 * -0.15f), Player.position.y, 0);

                float rot = 0;
                float offset1 = 0;
                float OffsetBig;

                if (Set == 2)
                {
                    OffsetBig = offset * 1.2f;
                }else if (Set == 3) 
                {
                    OffsetBig = offset * 1.6f;
                }else
                {
                    OffsetBig = offset * 2f;
                }
                
                float OffsetSmall = offset * 0.8f;

                int a;
                int j = 0;
                Vector2 points = new Vector2(firePoint.position.x, firePoint.position.y);
                Finished = true;
                b++;

                for (int i = 0; i < RealProjectiles; i++)
                {
                    if (cubes[i] != null)
                    {
                        Destroy(cubes[i]);
                    }
                }

                int count = 0;
                if (SetAmount % 2 == 1)
                {
                    points.x += Range1;                  

                    //cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
                    //cubes[j].transform.parent = gameObject.transform;
                    //j++;

                    if (Set % 2 == 1)
                    {
                        count++;
                        points.y = firePoint.position.y + offset1;
                        cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
                        cubes[j].transform.parent = gameObject.transform;
                        j++;
                        offset1 += OffsetSmall;
                        
                    }else
                    {
                        offset1 += OffsetSmall / 2;   
                    }

                    for (int i = 0; i < Set - count; i++)
                    {
                        points.y = firePoint.position.y + offset1;
                        cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
                        cubes[j].transform.parent = gameObject.transform;
                        j++;
                        i++;

                        points.y = firePoint.position.y - offset1;
                        cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
                        cubes[j].transform.parent = gameObject.transform;
                        offset1 += OffsetSmall;
                        j++;
                    }
                }
                else
                {
                    points.x += Range1;
                    offset1 += OffsetBig / 2;

                    for (int i = 0; i < Set; i++)
                    {
                        points.y = firePoint.position.y + offset1;
                        cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
                        cubes[j].transform.parent = gameObject.transform;
                        j++;

                        points.y = firePoint.position.y - offset1;
                        cubes[j] = Instantiate(cubePrefab, points, firePoint.rotation);
                        cubes[j].transform.parent = gameObject.transform;
                        j++;
                        offset1 += OffsetSmall;
                    }
                }

                count = j;
                int SetIndex = 0;
                rot += FirePointRotation * 6;

                for (int i = 0; i < (RealProjectiles - count); i++)
                {
                    i++;

                    if(SetIndex >= Set)
                    {
                        offset1 += OffsetBig;
                        rot += FirePointRotation * 6;
                        if (rot > MaxFirePointRotation)
                        {
                            rot = MaxFirePointRotation;
                        }
                        SetIndex = 0;
                    }else
                    {
                        offset1 += OffsetSmall;
                        SetIndex++;
                    }
                    
                    float distance;
                    a = 0;

                    points.y = firePoint.position.y + offset1;
                    distance = Vector2.Distance(firePoint.position, points);

                    while (distance > Range1 && a < 250)
                    {
                        a++;
                        points.x -= 0.3f;
                        distance = Vector2.Distance(firePoint.position, points);
                    }

                    if (a >= 249)
                    {
                        offset -= 0.05f;
                        Range1 += 0.2f;
                        Finished = false;
                    }

                    cubes[j] = Instantiate(cubePrefab, points, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + rot));
                    cubes[j].transform.parent = gameObject.transform;
                    j++;

                    points.y = firePoint.position.y - offset1;

                    cubes[j] = Instantiate(cubePrefab, points, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z - rot));
                    cubes[j].transform.parent = gameObject.transform;
                    j++;
                }

            }

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
