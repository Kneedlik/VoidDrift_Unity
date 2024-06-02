using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketLauncher : weapeon
{
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
    [SerializeField] float RangeX;
    [SerializeField] float RangeY;


    void Start()
    {
        SetUpWeapeon();
        setFirepoints();
        setSideFirepoints();
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
        if (projectileCount <= 0)
        {
            if (currentPoint)
            {
                currentPoint = false;
                SpawnRocket(firePoint1);
            }
            else
            {
                currentPoint = true;
                SpawnRocket(firePoint2);
            }
        }else
        {
            for (int i = 0; i < CubeList.Count; i++)
            {
                SpawnRocket(CubeList[i].transform);
            }
        }
            
        for (int i = 0; i < SideCubeList.Count; i++)
        {
            SpawnRocket(SideCubeList[i].transform);
        }  
    }

    public void SpawnRocket(Transform Pos)
    {
        GameObject Rocket = Instantiate(rocket, Pos.transform.position, Pos.transform.rotation);
        Rigidbody2D rb = Rocket.GetComponent<Rigidbody2D>();
        rb.AddForce(Pos.up * Force, ForceMode2D.Force);
    }

    public override void setFirepoints()
    {
        ResetFirePoints();

        int RealProjectileCount = projectileCount + 1;
        float offset = BaseOffset / RealProjectileCount;
        offset = offset + RealProjectileCount * OffsetScaling;
        float pom = offset;

        float RangeXTemp = RangeX / RealProjectileCount;
        RangeXTemp = RangeXTemp + RealProjectileCount * OffsetScaling;
        float RangeYTemp = RangeY / RealProjectileCount;
        RangeYTemp = RangeYTemp  + RealProjectileCount * OffsetScaling;

        if (projectileCount <= 0)
        {
            return;
        }

        GameObject CubeTemp;
        if (RealProjectileCount % 2 == 1)
        {
            CubeTemp = Instantiate(cube,firePoint);
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
            CubeTemp = Instantiate(cube, PreviousPoint1, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + offset + 270));
            CubeTemp.transform.SetParent(gameObject.transform);
            CubeList.Add(CubeTemp);

            PreviousPoint2.x = PreviousPoint2.x - RangeXTemp;
            PreviousPoint2.y = PreviousPoint2.y - RangeYTemp;
            CubeTemp = Instantiate(cube, PreviousPoint2, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z - offset + 270));
            CubeTemp.transform.SetParent(gameObject.transform);
            CubeList.Add(CubeTemp);
            offset += pom;
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

        for (int i = 0; i < sideProjectiles; i++)
        {
            GameObject CubeTemp;
            CubeTemp = Instantiate(cube, firePoint1.position, Quaternion.Euler(0, 0, (offsetA * -1) + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
      
            CubeTemp = Instantiate(cube, firePoint2.position, Quaternion.Euler(0, 0, offsetA + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
   
            offsetA += offset;
        }
    }
}
