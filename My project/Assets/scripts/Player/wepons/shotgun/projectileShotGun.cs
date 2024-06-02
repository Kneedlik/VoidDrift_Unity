using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class projectileShotGun : weapeon
{
    public float range;
    public float spread;

    public GameObject cube;
    public GameObject pellet;

    bool shootCheck = false;
    float timeStamp;

    public int magSize;
    public float reloadSpeed;
    public float reloadSpeedMultiplier = 1;
    public int currentAmmo;
    float reloadTimeStamp;
    public float BaseOffset;
    public float BaseSideOffset;
    public float MaxSideScaling;
    public float OffsetScaling;
    public float SideOffsetScaling;

    //RingOfFire;
    [HideInInspector] public int RingOfFireCount;
    [HideInInspector] public bool RingOfFireActive;
    Transform Player;

    //Cluster
    public int ClusterAmount = 0;
    public int ClusterProjectiles;
    public float ClusterDamageMultiplier;
    public float ClusterAliveTime;

    [HideInInspector]public bool HomingForm = false;
    public GameObject HomingProjectileObj;

    public bool LaserForm = false;
    public GameObject LineObj;
    public float LineSpeed;
    public GameObject LaserDamageObj;
    public GameObject LaserImpactObj;
    public float LaserImpactDelay;
    public float LaserDamageDelay;
    public float LaserRayCastSize;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        SetUpWeapeon();
        timeStamp = 0;

        currentAmmo = magSize;
        setFirepoints();
        setSideFirepoints();
    }

    void Update()
    {
        if (currentAmmo <= 0)
        {
            reloadTimeStamp += Time.deltaTime;
            if (reloadTimeStamp >= (reloadSpeed / reloadSpeedMultiplier))
            {
                if(RingOfFireActive)
                {
                    RingOfFire();
                }
                currentAmmo = magSize;
                reloadTimeStamp = 0;
            }
        }

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

        if (shootCheck && timeStamp <= 0 && currentAmmo > 0)
        {
            if (LaserForm == false)
            {
                Shoot();
            }else
            {
                ShootHitScan();
            }
            shootCheck = false;
            timeStamp = CoolDown;
            currentAmmo--;
        }   
    }

    void Shoot()
    {
        int pom = extraDamage;
        if (eventManager.OnFire != null)
        {
            eventManager.OnFire(gameObject);
        }

        for (int i = 0; i < CubeList.Count; i++)
        {
            GameObject PelletTemp;
            if (HomingForm)
            {
                 PelletTemp = Instantiate(HomingProjectileObj, transform.position, Quaternion.identity);
            }
            else
            {
                 PelletTemp = Instantiate(pellet, transform.position, Quaternion.identity);
            }

            if (eventManager.OnFireAll != null)
            {
                eventManager.OnFireAll(gameObject, PelletTemp);
            }

            PelletTemp.transform.position = firePoint.position;
            Rigidbody2D rb = PelletTemp.GetComponent<Rigidbody2D>(); 
            PelletTemp.transform.rotation = CubeList[i].transform.rotation;

            if (HomingForm)
            {
                HomingProjectile Homing = PelletTemp.GetComponent<HomingProjectile>();
                SetUpProjectile(Homing);
                Homing.force = Homing.force * ForceMultiplier;
                Homing.MaxSpeed = Homing.MaxSpeed * ForceMultiplier;
                Homing.function += BugetCluster;
            }
            else
            {
                ShotGunPellet BulletDamage = PelletTemp.GetComponent<ShotGunPellet>();
                SetUpProjectile(BulletDamage);
                BulletDamage.ClusterAmount = ClusterAmount;
                rb.AddForce(PelletTemp.transform.up * Force, ForceMode2D.Impulse);
            } 
        }

        for (int i = 0; i < SideCubeList.Count; i++)
        {
            GameObject bullet;
            bullet = Instantiate(pellet, SideCubeList[i].transform.position, SideCubeList[i].transform.rotation);

            if (eventManager.OnFireAll != null)
            {
                eventManager.OnFireAll(gameObject, bullet);
            }

            ShotGunPellet BulletDamage = bullet.GetComponent<ShotGunPellet>();
            SetUpProjectile(BulletDamage);
            BulletDamage.ClusterAmount = ClusterAmount;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(SideCubeList[i].transform.up * Force, ForceMode2D.Impulse);
        }

        extraDamage = pom;
    }

    public override void setFirepoints()
    {
        ResetFirePoints();
        float offset = BaseOffset / projectileCount;
        offset = offset + projectileCount * OffsetScaling;
        float pom = offset;
        int Count = projectileCount;
        bool Switch = false;

        if(projectileCount % 2 == 1)
        {
            GameObject CubeTemp = Instantiate(cube);
            CubeTemp.transform.position = firePoint.position;
            CubeTemp.transform.rotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z);
            CubeTemp.transform.parent = gameObject.transform;
            Count -= 1;
            CubeList.Add(CubeTemp);
        }
        else
        {
            offset = offset / 2;
        }

        for (int i = 0; i < Count; i++)
        {
            GameObject CubeTemp = Instantiate(cube);
            CubeTemp.transform.position = firePoint.position;

            if (Switch)
            {
                CubeTemp.transform.rotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + offset);
                offset += pom;
                Switch = false;
            }else
            {
                CubeTemp.transform.rotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z - offset);
                Switch = true;
            }
            
            CubeTemp.transform.parent = gameObject.transform;
            
            CubeList.Add(CubeTemp);
        }
    }

    public override void setSideFirepoints()
    {
        ResetSideFirePoints();
        float offset = BaseSideOffset / sideProjectiles;
        offset = offset + (sideProjectiles * SideOffsetScaling);
        if (offset > MaxSideScaling && MaxSideScaling != 0)
        {
            offset = MaxSideScaling;
        }
        float offsetA = offset * 2;

        for (int i = 0; i < sideProjectiles * 2;)
        {
            GameObject CubeTemp;
            CubeTemp = Instantiate(cube, firePoint.position, Quaternion.Euler(0, 0, (offsetA * -1) + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
            i++;
            CubeTemp = Instantiate(cube, firePoint.position, Quaternion.Euler(0, 0, offsetA + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
            i++;
            offsetA += offset;
        }
    }

    public override GameObject GetProjectile()
    {
        return pellet;
    }

    public void RingOfFire()
    {
        float offset = Player.rotation.eulerAngles.z;
        float pom = 360 / RingOfFireCount;

        for(int i = 0;i < RingOfFireCount ;i++)
        {
            GameObject Pellet = Instantiate(pellet, Player.position, Quaternion.Euler(0, 0, offset));
            Rigidbody2D rigidbody2D = Pellet.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Pellet.transform.up * Force * 0.75f);
            offset += pom;
        }
    }

    public void BugetCluster(GameObject target, int damage, ref int scaledDamage)
    {
        scaledDamage = damage;
        float offset = 360f / ClusterProjectiles;
        float pom = offset;

        for (int i = 0; i < ClusterProjectiles; i++)
        {
            GameObject Pellet = Instantiate(pellet, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + offset));
            HomingProjectile HomingScr = Pellet.GetComponent<HomingProjectile>();
            SetUpProjectile(HomingScr);
            Pellet.transform.localScale = new Vector3(Pellet.transform.localScale.x * 0.5f, Pellet.transform.localScale.y * 0.5f, Pellet.transform.localScale.z * 0.5f);
            HomingScr.IgnoreTargets.Add(target);
            HomingScr.force = HomingScr.force * ForceMultiplier * 0.5f;
            HomingScr.MaxSpeed = HomingScr.MaxSpeed * ForceMultiplier * 0.5f;

            
            HomingScr.destroyTime = HomingScr.destroyTime * 0.5f;
            float damagePom = HomingScr.damage * ClusterDamageMultiplier;
            HomingScr.damage = (int)damagePom;

            offset += pom;
        }

    }

    public void ShootHitScan()
    {
        int pom = extraDamage;
        if (eventManager.OnFire != null)
        {
            eventManager.OnFire(gameObject);
        }

        GameObject ObjTemp;
        ObjTemp = Instantiate(LaserImpactObj);
        ObjTemp.GetComponent<ShotgunImapact>().Delay = LaserImpactDelay;
        ObjTemp.GetComponent<ShotgunImapact>().Size = LaserRayCastSize;

        ObjTemp = Instantiate(LaserDamageObj);
        ShotgunLaserDamage LaserDamage = ObjTemp.GetComponent<ShotgunLaserDamage>();
        LaserDamage.DelayBegin = LaserImpactDelay;
        LaserDamage.DelayDamage = LaserDamageDelay;
        LaserDamage.Size = LaserRayCastSize;
        LaserDamage.damage = damage + extraDamage;

        extraDamage = pom;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
