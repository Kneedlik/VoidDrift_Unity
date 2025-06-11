using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerGun : weapeon
{
    private float timeStamp;
    private bool shootCheck;
    Rigidbody2D rb;
    float TimeStampSum;

    public GameObject bulletPrefab;
    HomingProjectile BulletDamage;

    GameObject[] sideCubes = new GameObject[100];
    public GameObject cubePrefab;
    [SerializeField] int RealProjectiles;
    public float sideScaling = 1;
    public float MaxSideScaling;
    public float BaseOffset;
    public float BaseSideOffset;
    public float OffsetScaling;

    public float RotationMultiplier = 1f;

    //Upgrades
    public bool Double;
    public bool PierceFinal;
    public bool SummonFinal;
    public int SummonCount;
    [SerializeField] GameObject SummonPrefab;
    [SerializeField] float SummonCooldown;
    [SerializeField] float SummonDelay;
    [SerializeField] GameObject DroneManager;
    [SerializeField] float SummonPointDistance;
    [SerializeField] List<GameObject> SummonList = new List<GameObject>();
    [SerializeField] List<GameObject> SummonList2 = new List<GameObject>();
    [SerializeField] Transform Firepoint1;
    [SerializeField] Transform Firepoint2;

    // Start is called before the first frame update
    void Start()
    {
        SetUpWeapeon();
        setFirepoints();

        PlayerStats.OnLevel += SetAS;
        PlayerStats.OnLevel += SetForce;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("0");

        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (TimeStampSum > 0)
        {
            TimeStampSum -= Time.deltaTime;
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
                if (SummonFinal == false)
                {
                    Shoot();
                }else
                {
                    SpawnSummons(true);
                }
                timeStamp = CoolDown;
            }
        }

        if(SummonCount > 0)
        {
            if(TimeStampSum <= 0)
            {
                SpawnSummons(false);
                TimeStampSum = SummonCooldown;
            }
        }
    }

    void SetUpBullet(GameObject bullet)
    {
        BulletDamage = bullet.GetComponent<HomingProjectile>();
        if (PierceFinal == false)
        {
            BulletDamage.setDamage(damage + extraDamage);
        }else
        {
            int DamageTemp = damage;
            DamageTemp = DamageTemp * RealProjectiles;
            BulletDamage.setDamage(DamageTemp + extraDamage);
        }
        BulletDamage.setArea(size);
        BulletDamage.setPierce(pierce);
        BulletDamage.setKnockBack(knockBack);
        BulletDamage.force = BulletDamage.force * ForceMultiplier;
        BulletDamage.MaxSpeed = BulletDamage.MaxSpeed * ForceMultiplier;

        float IncreaseTemp;

        float MultiplierTemp = PlayerStats.sharedInstance.ProjectileForce;

        if (PierceFinal == false)
        {
            IncreaseTemp = BulletDamage.rotSpeed * (MultiplierTemp);
            IncreaseTemp = IncreaseTemp - BulletDamage.rotSpeed;
            if(IncreaseTemp > 0)
            {
                IncreaseTemp = IncreaseTemp * 0.65f;
                BulletDamage.rotSpeed = IncreaseTemp + BulletDamage.rotSpeed;
            }
        }else
        {
            BulletDamage.rotSpeed = BulletDamage.rotSpeed * (MultiplierTemp);
        }
        BulletDamage.rotSpeed *= RotationMultiplier;

        IncreaseTemp = BulletDamage.Delay / (MultiplierTemp);
        IncreaseTemp = BulletDamage.Delay - IncreaseTemp;
        if(IncreaseTemp > 0)
        {
            IncreaseTemp = IncreaseTemp * 0.45f;
            BulletDamage.Delay = BulletDamage.Delay - IncreaseTemp;
        }

        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.up * Force, ForceMode2D.Impulse);
    }

    void Shoot()
    {
        int pom = extraDamage;

        if (PierceFinal)
        {
            GameObject bullet;
            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            if (eventManager.OnFireAll != null)
            {
                eventManager.OnFireAll(gameObject, bullet);
            }
            SetUpBullet(bullet);
        }
        else
        {
            for (int i = 0; i < CubeList.Count; i++)
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, CubeList[i].transform.position, CubeList[i].transform.rotation);

                if (eventManager.OnFireAll != null)
                {
                    eventManager.OnFireAll(gameObject, bullet);
                }

                SetUpBullet(bullet);
            }

            for (int i = 0; i < SideCubeList.Count; i++)
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, sideCubes[i].transform.position, sideCubes[i].transform.rotation);

                if (eventManager.OnFireAll != null)
                {
                    eventManager.OnFireAll(gameObject, bullet);
                }

                SetUpBullet(bullet);
            }

        }
        extraDamage = pom;
    }

    public override void setFirepoints()
    {
        ResetFirePoints();

        if (Double)
        {
            GameObject TempG;
            RealProjectiles = projectileCount + sideProjectiles + 1;
            float offset = BaseOffset / RealProjectiles;
            offset = offset + RealProjectiles * (OffsetScaling * 0.4f);
            float pom = 0;

            if(RealProjectiles % 2 == 1)
            {
                RealProjectiles = RealProjectiles - 1;
                TempG = Instantiate(cubePrefab, Firepoint1.position, Firepoint1.rotation);
                TempG.transform.parent = gameObject.transform;
                CubeList.Add(TempG);

                TempG = Instantiate(cubePrefab, Firepoint1.position, Firepoint1.rotation);
                TempG.transform.parent = gameObject.transform;
                CubeList.Add(TempG);

                pom = offset;
            }else
            {
                pom = offset / 2;
            }

            bool Flip = false;
            for (int i = 0; i < RealProjectiles; i++)
            {
                if(Flip)
                {
                    TempG = Instantiate(cubePrefab, Firepoint1.position, Quaternion.Euler(0, 0, Firepoint1.rotation.eulerAngles.z + pom));
                    TempG.transform.parent = gameObject.transform;
                    CubeList.Add(TempG);

                    TempG = Instantiate(cubePrefab, Firepoint1.position, Quaternion.Euler(0, 0, Firepoint1.rotation.eulerAngles.z - pom));
                    TempG.transform.parent = gameObject.transform;
                    CubeList.Add(TempG);

                    Flip = false;
                }
                else
                {
                    TempG = Instantiate(cubePrefab, Firepoint2.position, Quaternion.Euler(0, 0, Firepoint2.rotation.eulerAngles.z + pom));
                    TempG.transform.parent = gameObject.transform;
                    CubeList.Add(TempG);

                    TempG = Instantiate(cubePrefab, Firepoint2.position, Quaternion.Euler(0, 0, Firepoint2.rotation.eulerAngles.z - pom));
                    TempG.transform.parent = gameObject.transform;
                    CubeList.Add(TempG);

                    Flip = true;
                }
                pom += offset;
            }
        }
        else if (SummonFinal == false)
        {
            RealProjectiles = projectileCount + 1;

            float offset = BaseOffset / RealProjectiles;
            offset = offset + RealProjectiles * OffsetScaling;
            float pom = 0;
            int Count = RealProjectiles;

            GameObject TempG;

            if (Count % 2 == 1)
            {
                Count = Count - 1;
                TempG = Instantiate(cubePrefab, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z));
                TempG.transform.parent = gameObject.transform;
                CubeList.Add(TempG);
                pom = offset;
            }
            else pom = offset / 2;

            for (int i = 0; i < (Count / 2); i++)
            {
                TempG = Instantiate(cubePrefab, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + pom));
                TempG.transform.parent = gameObject.transform;
                CubeList.Add(TempG);

                TempG = Instantiate(cubePrefab, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z - pom));
                TempG.transform.parent = gameObject.transform;
                CubeList.Add(TempG);

                pom += offset;
            }
        }else
        {
            RealProjectiles = RealProjectiles + sideProjectiles + 1;
        }

        SetUpSummonPoints();
    }

    public override void setSideFirepoints()
    {
        ResetSideFirePoints();

        if (Double == false && SummonFinal == false)
        {
            float offset = BaseSideOffset / sideProjectiles;
            offset = offset + (sideProjectiles * sideScaling) / sideProjectiles;
            if (offset > MaxSideScaling && MaxSideScaling != 0)
            {
                offset = MaxSideScaling;
            }

            float offsetA = offset * 2;

            for (int i = 0; i < sideProjectiles * 2; i++)
            {
                Destroy(sideCubes[i]);
            }

            for (int i = 0; i < sideProjectiles * 2;)
            {
                sideCubes[i] = Instantiate(cubePrefab, firePoint.position, Quaternion.Euler(0, 0, (offsetA * -1) + 270));
                sideCubes[i].transform.parent = gameObject.transform;
                i++;
                sideCubes[i] = Instantiate(cubePrefab, firePoint.position, Quaternion.Euler(0, 0, offsetA + 270));
                sideCubes[i].transform.parent = gameObject.transform;
                i++;
                offsetA += offset;
            }
        }
    }

    void SpawnSummons(bool Fire)
    {
        if (Fire == false)
        {
            StartCoroutine(SpawnSummsRutine());
        }else
        {
            for (int i = 0; i < SummonList2.Count; i++)
            {
                GameObject Obj = Instantiate(SummonPrefab, SummonList2[i].transform.position, Quaternion.Euler(0, 0, 0));
                Obj.transform.SetParent(SummonList2[i].transform);
            }
        }
    }

    IEnumerator SpawnSummsRutine()
    {
        for (int i = 0; i < SummonList.Count; i++)
        {
            GameObject Obj = Instantiate(SummonPrefab, SummonList[i].transform.position, Quaternion.Euler(0, 0, 0));
            Obj.transform.SetParent(SummonList[i].transform);
            yield return new WaitForSeconds(SummonDelay);
        }
    }

    public void SetUpSummonPoints()
    {
        for (int i = 0; i < SummonList.Count ; i++)
        {
            if (SummonList[i] != null)
            {
                Destroy(SummonList[i]);
            }
        }
        SummonList.Clear();

        int PointAmount;
        PointAmount = SummonCount;

        float Offset = 360f / PointAmount;
        float OffsetTemp = 0;
        for (int i = 0; i < PointAmount;i++)
        {
            Vector3 Direction = new Vector3(Mathf.Sin(OffsetTemp * Mathf.Deg2Rad), Mathf.Cos(OffsetTemp * Mathf.Deg2Rad), 0);
            Vector3 Point = DroneManager.transform.position + (SummonPointDistance * Direction);
            GameObject Obj = Instantiate(cubePrefab, Point, Quaternion.Euler(0, 0, 0));
            Obj.transform.SetParent(DroneManager.transform);
            OffsetTemp += Offset;
            SummonList.Add(Obj);
        }

        if (SummonFinal)
        {
            PointAmount = projectileCount + sideProjectiles + 1;

            Offset = 360f / PointAmount;
            OffsetTemp = 0;
            for (int i = 0; i < PointAmount; i++)
            {
                Vector3 Direction = new Vector3(Mathf.Sin(OffsetTemp * Mathf.Deg2Rad), Mathf.Cos(OffsetTemp * Mathf.Deg2Rad), 0);
                Vector3 Point = DroneManager.transform.position + (SummonPointDistance * Direction * 1.25f);
                GameObject Obj = Instantiate(cubePrefab, Point, Quaternion.Euler(0, 0, 0));
                Obj.transform.SetParent(DroneManager.transform);
                OffsetTemp += Offset;
                SummonList2.Add(Obj);
            }
        }
    }
}
