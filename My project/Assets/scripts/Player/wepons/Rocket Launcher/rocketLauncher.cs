using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class rocketLauncher : weapeon
{
    //Homing
    public int HomingAmount;
    public bool HomingFinal;
    [SerializeField] GameObject HomingPrefab;
    List<GameObject> HomingCubeList = new List<GameObject>();
    //[SerializeField] float HomingPointDistance;

    //Atomic
    public bool Atomic;
    [SerializeField] GameObject AtomicPrefab;
    public float AtomicForce;

    //Plasma
    public bool Plasma;
    [SerializeField] GameObject PlasmaPrefab;

    bool shootCheck;
    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject rocket;
    bool currentPoint;

    float timeStamp;
    [SerializeField] GameObject cube;
    public float BaseOffset;
    public float BaseSideOffset;
    public float MaxSideScaling;
    public float OffsetScaling;
    public float SideOffsetScaling;
    [SerializeField] float RangeOffsetScaling;
    [SerializeField] float RangeX;
    [SerializeField] float RangeY;
    public float SpeedMultiplier = 1f;
    public int ImpactDamage;

    void Start()
    {
        SetUpWeapeon();
        setFirepoints();
        setSideFirepoints();
        setHomingFirePoints();
        shootCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            shootCheck = true;
        }

        if(Input.GetButtonUp("Fire1"))
        {
            shootCheck = false;
        }

        if (shootCheck)
        {
            if (timeStamp <= 0)
            {
                Shoot();
                timeStamp = CoolDown;
            }
        }
    }

    void Shoot()
    {
        int pom = extraDamage;
        if (eventManager.OnFire != null)
        {
            eventManager.OnFire(gameObject);
        }

        FireHoming();
        if(HomingFinal)
        {
            extraDamage = pom;
            return;
        }

        if (Atomic == false)
        {
            if (projectileCount <= 0)
            {
                if (currentPoint)
                {
                    currentPoint = false;
                    if (Plasma == false)
                    {
                        SpawnRocket(firePoint1, rocket);
                    }
                    else SpawnRocket(firePoint1, PlasmaPrefab);
                    
                }
                else
                {
                    currentPoint = true;
                    if (Plasma == false)
                    {
                        SpawnRocket(firePoint1, rocket);
                    }
                    else SpawnRocket(firePoint1, PlasmaPrefab); 
                }
            }
            else
            {
                for (int i = 0; i < CubeList.Count; i++)
                {
                    if (Plasma == false)
                    {
                        SpawnRocket(CubeList[i].transform, rocket);
                    }
                    else SpawnRocket(CubeList[i].transform, PlasmaPrefab);
                }
            }

            for (int i = 0; i < SideCubeList.Count; i++)
            {
                if (Plasma == false)
                {
                    SpawnRocket(SideCubeList[i].transform, rocket);
                }
                else SpawnRocket(SideCubeList[i].transform, PlasmaPrefab);
            }
        }else
        {
            SpawnRocket(firePoint,AtomicPrefab);
        }

        extraDamage = pom;
    }

    public GameObject SpawnRocket(Transform Pos,GameObject Obj)
    {
        GameObject Rocket = Instantiate(Obj, Pos.transform.position, Pos.transform.rotation);
        if (eventManager.OnFireAll != null)
        {
            eventManager.OnFireAll(gameObject, Rocket);
        }
        rocket Bullet = Rocket.GetComponent<rocket>();
        SetUpProjectile(Bullet);
        Rigidbody2D rb = Rocket.GetComponent<Rigidbody2D>();
        Bullet.SetSpeed(SpeedMultiplier);
        float ImpactDamageTemp = ImpactDamage * damageMultiplier;
        Bullet.ImpactDamage = KnedlikLib.ScaleDamage((int)ImpactDamageTemp, true, true);

        if(Atomic)
        {
            rb.AddForce(Pos.up * AtomicForce, ForceMode2D.Force);
            float TempDamage = Bullet.damagePlus + ((projectileCount + sideProjectiles) * (damage * 0.6f));
            Bullet.setDamage((int)TempDamage);
        }else rb.AddForce(Pos.up * Force, ForceMode2D.Force);
        return Rocket;
    }

    public void FireHoming()
    {
        for (int i = 0; i < HomingCubeList.Count; i++)
        {
            GameObject Obj = SpawnRocket(HomingCubeList[i].transform, HomingPrefab);
            BulletScript Bullet = Obj.GetComponent<BulletScript>();
            Bullet.setDamage(Bullet.damagePlus * 2);
        }
    }

    public override void setFirepoints()
    {
        ResetFirePoints();

        setHomingFirePoints();
        if (HomingFinal)
        {
            return;
        }

        int RealProjectileCount = projectileCount + 1;
        if(RealProjectileCount <= 0)
        {
            RealProjectileCount = 1;
        }
        float offset = BaseOffset / RealProjectileCount;
        offset = offset + RealProjectileCount * OffsetScaling;
        float pom = offset;

        float RangeXTemp = RangeX / RealProjectileCount;
        RangeXTemp = RangeXTemp + (RangeOffsetScaling * RealProjectileCount);
        float RangeYTemp = RangeY / RealProjectileCount;
        RangeYTemp = RangeYTemp + (RangeOffsetScaling * RealProjectileCount);

        GameObject CubeTemp;
        if (RealProjectileCount % 2 == 1)
        {
            CubeTemp = Instantiate(cube,firePoint.position, Quaternion.Euler(0,0, firePoint.rotation.z));
            CubeTemp.transform.SetParent(gameObject.transform);
            CubeList.Add(CubeTemp);
            RealProjectileCount -= 1;
        }

        Vector3 PreviousPoint1 = firePoint1.position;
        Vector3 PreviousPoint2 = firePoint2.position;

        for (int i = 0; i < RealProjectileCount / 2; i++)
        {
            PreviousPoint1.x = PreviousPoint1.x - RangeXTemp;
            PreviousPoint1.y = PreviousPoint1.y + RangeYTemp;
            CubeTemp = Instantiate(cube, PreviousPoint1, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + offset));
            CubeTemp.transform.SetParent(gameObject.transform);
            CubeList.Add(CubeTemp);

            PreviousPoint2.x = PreviousPoint2.x - RangeXTemp;
            PreviousPoint2.y = PreviousPoint2.y - RangeYTemp;
            CubeTemp = Instantiate(cube, PreviousPoint2, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z - offset));
            CubeTemp.transform.SetParent(gameObject.transform);
            CubeList.Add(CubeTemp);
            offset += pom;
        }
    }

    public override void setSideFirepoints()
    {
        ResetSideFirePoints();
        if(HomingFinal)
        {
            return;
        }

        float offset = BaseSideOffset / sideProjectiles;
        offset = offset + (sideProjectiles * SideOffsetScaling);
        if (offset > MaxSideScaling && MaxSideScaling != 0)
        {
            offset = MaxSideScaling;
        }
        float offsetA = offset * 2;

        for (int i = 0; i < sideProjectiles; i++)
        {
            GameObject CubeTemp;
            CubeTemp = Instantiate(cube, firePoint1.position, Quaternion.Euler(0, 0, offsetA + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
      
            CubeTemp = Instantiate(cube, firePoint2.position, Quaternion.Euler(0, 0, (offsetA * -1) + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
   
            offsetA += offset;
        }
    }

    public void setHomingFirePoints()
    {
        ResetHomingFirePoints();

        float Offset;
        float pom;
        int Amount;

        if (HomingFinal)
        {
            Amount = HomingAmount + projectileCount + sideProjectiles + 1;
        }else
        {
            Amount = HomingAmount;
        }

        Offset = 360f / Amount;
        pom = Offset;
        Offset = 0;
        GameObject ObjTemp;
    
        for (int i = 0;i < Amount;i++)
        {
            ObjTemp = Instantiate(cube,gameObject.transform.position,Quaternion.Euler(0,0,Offset + 270));
            //ObjTemp.transform.Translate(ObjTemp.transform.forward * HomingPointDistance);
            ObjTemp.transform.parent = gameObject.transform;
            HomingCubeList.Add(ObjTemp);
            Offset += pom;
        }     
    }

    public void ResetHomingFirePoints()
    {
        for (int i = 0; i < HomingCubeList.Count; i++)
        {
            Destroy(HomingCubeList[i]);
        }
        HomingCubeList.Clear();
    }
}
