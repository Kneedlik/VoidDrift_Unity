
using System;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : weapeon
{
    public List<Hovl_Laser> LasersList = new List<Hovl_Laser>();
    public List<Hovl_Laser> SideLasersList = new List<Hovl_Laser>();

    public Hovl_Laser MainLine;
    float timeStamp;
    public float MaxLength = 30;

    public float BaseSideOffset;
    public float MaxSideScaling;
    public float SideOffsetScaling;
    public GameObject cube;
    [SerializeField] GameObject FlashObj;

    public float FlashBaseSize;
    public float HitBaseSize;
    public float LineBaseSize;
    public float ProjectileCountMultiplier;
    public float WidthMultiplier;
    public float MaxRange;

    //Circle
    public bool Circle;

    //Inferno
    public bool Inferno;
    public Hovl_Laser InfernoLine;
    [SerializeField] GameObject InfernoFlashObj;

    //Drones
    public bool DronesFinal;
    public int DroneAmount;
    [SerializeField] GameObject DronePrefab;
    [SerializeField] DroneManager Dronemanager;
    [SerializeField] float DroneDistance;

    private void Start()
    {
        FlashBaseSize = FlashObj.transform.localScale.y;
        HitBaseSize = MainLine.HitEffect.transform.localScale.y;
        LineBaseSize = MainLine.Laser.widthMultiplier;

        SetUpWeapeon();
        MainLine.MaxLength = MaxLength;
        timeStamp = 0;
        setSideFirepoints();
        SetUpDrones();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Circle == false && DronesFinal == false && Inferno == false)
            {
                MainLine.EnablePrepare();

                for (int i = 0; i < SideLasersList.Count; i++)
                {
                    SideLasersList[i].EnablePrepare();
                }
            }
            else if (Circle)
            {
                for (int i = 0; i < LasersList.Count; i++)
                {
                    LasersList[i].EnablePrepare();
                }
            }else if (Inferno)
            {
                InfernoLine.EnablePrepare();
            }

            if (DronesFinal == false)
            {
                Shoot();
            }
        } else
        {
            MainLine.DisablePrepare();
            InfernoLine.DisablePrepare();
            if (Circle == false)
            { 
                for (int i = 0; i < SideLasersList.Count; i++)
                {
                    SideLasersList[i].DisablePrepare();
                }
            }
            else
            {
                for (int i = 0; i < LasersList.Count; i++)
                {
                    LasersList[i].DisablePrepare();
                }
            }

        }

        if (DronesFinal == false)
        {
            if (Input.GetButton("Fire1") && timeStamp <= 0)
            {
                timeStamp = CoolDown;
                DealDamage();
            }
        }

        if(timeStamp >= 0)
        {
            timeStamp -= Time.deltaTime;
        } 
    }

    void Shoot()
    {
        if (Circle == false)
        {
            int PierceTemp = pierce;
            bool Infinite = true;

            float SizeTrue = size + (size * projectileCount);
            SizeTrue = SizeTrue * WidthMultiplier;

            //RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, firePoint.up);
            RaycastHit2D[] hitInfo = Physics2D.CapsuleCastAll(firePoint.position, new Vector2(SizeTrue, SizeTrue), CapsuleDirection2D.Vertical, firePoint.rotation.eulerAngles.z, firePoint.up, MaxLength);

            for (int i = 0; i < hitInfo.Length; i++)
            {
                if (hitInfo[i].transform.GetComponent<Health>() != null)
                {
                    if (PierceTemp == 0)
                    {
                        Infinite = false;
                        break;
                    }
                    else
                    {
                        PierceTemp -= 1;
                    }
                }

            }

            PierceTemp = pierce;
            if (Infinite == false)
            {
                int Index = 0;
                for (int i = 0; i < hitInfo.Length; i++)
                {
                    if (hitInfo[i].transform.GetComponent<Health>() != null)
                    {
                        Index = i;
                        if (PierceTemp <= 0)
                        {
                            break;
                        }
                        else
                        {
                            PierceTemp -= 1;
                        }
                    }
                }

                if(Inferno == false)
                {
                    MainLine.SetUp(firePoint.position, hitInfo[Index].point, false);
                }
                else InfernoLine.SetUp(firePoint.position, hitInfo[Index].point, false);

            }
            else
            {
                if (Inferno == false)
                {
                    MainLine.SetUp(firePoint.position, firePoint.position + firePoint.up * MaxLength, true);
                }
                else InfernoLine.SetUp(firePoint.position, firePoint.position + firePoint.up * MaxLength, true);
            }

            for (int i = 0; i < SideCubeList.Count; i++)
            {
                ShootToPoint(SideCubeList[i].transform);
            }
        }else
        {
            for (int i = 0; i < CubeList.Count; i++)
            {
                ShootToPoint(CubeList[i].transform);
            }
        }

    }

    void ShootToPoint(Transform point)
    {
        int PierceTemp = pierce;
        bool Infinite = true;

        //RaycastHit2D[] hitInfo = Physics2D.RaycastAll(point.position, point.up);
        float SizeTrue = size * WidthMultiplier;
        RaycastHit2D[] hitInfo = Physics2D.CapsuleCastAll(point.position, new Vector2(SizeTrue, SizeTrue), CapsuleDirection2D.Vertical, point.rotation.eulerAngles.z, point.up, MaxLength);

        for (int j = 0; j < hitInfo.Length; j++)
        {
            if (hitInfo[j].transform.GetComponent<Health>() != null)
            {
                if (PierceTemp == 0)
                {
                    Infinite = false;
                    break;
                }
                else
                {
                    PierceTemp -= 1;
                }
            }
        }

        PierceTemp = pierce;
        if (Infinite == false)
        {
            int Index = 0;
            for (int j = 0; j < hitInfo.Length; j++)
            {
                if (hitInfo[j].transform.GetComponent<Health>() != null)
                {
                    Index = j;
                    if (PierceTemp <= 0)
                    {
                        break;
                    }
                    else
                    {
                        PierceTemp -= 1;
                    }
                }
            }

            Hovl_Laser LineTemp = point.GetComponent<Hovl_Laser>();
            if (LineTemp != null)
            {
                LineTemp.SetUp(point.position, hitInfo[Index].point, false);
            }
        }
        else
        {
            Hovl_Laser LineTemp = point.GetComponent<Hovl_Laser>();
            if (LineTemp != null)
            {
                LineTemp.SetUp(point.transform.position, point.transform.position + point.transform.up * MaxLength, false);
            }
        }
    }

    void DealDamage()
    {
        int TempExtra = extraDamage;

        if (Circle == false)
        {
            int PierceTemp = pierce;
            float SizeTrue;
            if (Inferno)
            {
                SizeTrue = size + (size * (projectileCount + sideProjectiles));
                SizeTrue = SizeTrue * 1.25f;
            }
            else if (Circle)
            {
                SizeTrue = size + (size * (projectileCount + sideProjectiles));
            }
            else
            {
                SizeTrue = size + (size * projectileCount);
            }
            SizeTrue = SizeTrue * WidthMultiplier;
            
            RaycastHit2D[] hitInfo = Physics2D.CapsuleCastAll(firePoint.position, new Vector2(SizeTrue, SizeTrue), CapsuleDirection2D.Vertical, firePoint.rotation.eulerAngles.z, firePoint.up, MaxLength);

            if (eventManager.OnFire != null)
            {
                eventManager.OnFire(gameObject);
            }

            for (int i = 0; i < hitInfo.Length; i++)
            {
                Health health = hitInfo[i].transform.GetComponent<Health>();
                if (health != null && Vector3.Distance(transform.position, hitInfo[i].transform.position) < MaxRange)
                {
                    if (eventManager.OnFireAll != null)
                    {
                        eventManager.OnFireAll(gameObject,null );
                    }

                    float pom = damage + (damage * projectileCount * ProjectileCountMultiplier);
                    int DamagePlus = (int)pom + extraDamage;
                    if(eventManager.ImpactGunOnlyHitScan != null)
                    {
                        eventManager.ImpactGunOnlyHitScan(hitInfo[i].transform.gameObject,DamagePlus, ref DamagePlus);
                    }

                    if(eventManager.OnImpact != null)
                    {
                        eventManager.OnImpact(hitInfo[i].transform.gameObject, damage, ref DamagePlus);
                    }

                    Color32 Color = new Color32(0,0,0,0);
                    if (eventManager.OnCrit != null)
                    {
                        Color32 TempColor = eventManager.OnCrit(hitInfo[i].transform.gameObject, DamagePlus, ref DamagePlus);
                        Color32 BaseColor = new Color32(0, 0, 0, 0);
                        if (!TempColor.Equals(BaseColor))
                        {
                            Color = TempColor;
                        }
                    }

                    if (eventManager.PostImpact != null)
                    {
                        eventManager.PostImpact(hitInfo[i].transform.gameObject, DamagePlus, ref DamagePlus);
                    }

                    if (Color.Equals(new Color32(0, 0, 0, 0)))
                    {
                        health.TakeDamage(DamagePlus);
                    }
                    else
                    {
                        //Debug.Log(Color);
                        health.TakeDamage(DamagePlus, Color);
                    }

                    if (PierceTemp <= 0)
                    {
                        break;
                    }
                    else
                    {
                        PierceTemp -= 1;
                    }
                }
            }

            for (int i = 0; i < SideCubeList.Count; i++)
            {
                DealDamageToPoint(SideCubeList[i].transform,damage);
            }
        }else if(Circle)
        {
            for (int i = 0;i < CubeList.Count;i++)
            {
                float pom = damage + (damage * projectileCount * ProjectileCountMultiplier);
                int DamagePlus = (int)pom;
                DealDamageToPoint(CubeList[i].transform,DamagePlus);
            }
        }

        extraDamage = TempExtra;
    }

    void DealDamageToPoint(Transform point,int Damage)
    {
        int PierceTemp = pierce;
        float SizeTrue = size * WidthMultiplier;
        RaycastHit2D[] hitInfo = Physics2D.CapsuleCastAll(point.position, new Vector2(SizeTrue, SizeTrue), CapsuleDirection2D.Vertical, point.rotation.eulerAngles.z, point.up, MaxLength);

        for (int j = 0; j < hitInfo.Length; j++)
        {
            Health health = hitInfo[j].transform.GetComponent<Health>();
            if (health != null && Vector3.Distance(transform.position, hitInfo[j].transform.position) < MaxRange)
            {

                if (eventManager.OnFireAll != null)
                {
                    eventManager.OnFireAll(gameObject, null);
                }

                int DamagePlus = Damage + extraDamage;
                if (eventManager.ImpactGunOnlyHitScan != null)
                {
                    eventManager.ImpactGunOnlyHitScan(hitInfo[j].transform.gameObject, DamagePlus, ref DamagePlus);
                }

                if (eventManager.OnImpact != null)
                {
                    eventManager.OnImpact(hitInfo[j].transform.gameObject, DamagePlus, ref DamagePlus);
                }

                Color32 Color = new Color32(0, 0, 0, 0);
                if (eventManager.OnCrit != null)
                {
                    Color32 TempColor = eventManager.OnCrit(hitInfo[j].transform.gameObject, DamagePlus, ref DamagePlus);
                    Color32 BaseColor = new Color32(0, 0, 0, 0);
                    if (!TempColor.Equals(BaseColor))
                    {
                        Color = TempColor;
                    }
                }

                if (eventManager.PostImpact != null)
                {
                    eventManager.PostImpact(hitInfo[j].transform.gameObject, DamagePlus, ref DamagePlus);
                }

           
                if (Color.Equals(new Color32(0, 0, 0, 0)))
                {
                    health.TakeDamage(DamagePlus);
                }
                else
                {
                        //Debug.Log(Color);
                    health.TakeDamage(DamagePlus, Color);
                }

                if (PierceTemp <= 0)
                {
                    break;
                }
                else
                {
                    PierceTemp -= 1;
                }
            }
        }
    }

    public override void updateSize(int multiplier)
    {
        size = ((float)multiplier / 100f) * baseSize * MasterManager.Instance.PlayerInformation.SizeMultiplier;
        float LineSize = ((float)multiplier / 100f) * baseSize * MasterManager.Instance.PlayerInformation.SizeMultiplier ;

        float Sizetemp = HitBaseSize * size;
        float FlashSizeTemp = FlashBaseSize * size;
        LineSize = LineBaseSize * LineSize;

        Hovl_Laser LaserTemp;

        for (int i = 0; i < SideCubeList.Count; i++)
        {
            LaserTemp = SideCubeList[i].GetComponent<Hovl_Laser>();
            if (LaserTemp != null)
            {
                LaserTemp.SetSize(LineSize, Sizetemp);
            }
        }

        float FlashSizeMultiplier = 0.28f;
        if (Inferno == false)
        {
            LineSize = size * (projectileCount + 1);
            Sizetemp = Sizetemp * (projectileCount + 1);
            FlashSizeTemp = FlashSizeTemp * (projectileCount + 1);
            MainLine.SetSize(LineSize, Sizetemp);

            FlashObj.transform.localScale = new Vector3(FlashSizeTemp * FlashSizeMultiplier, FlashSizeTemp * FlashSizeMultiplier, FlashSizeTemp * FlashSizeMultiplier);
        }else
        {
            LineSize = size * (projectileCount + sideProjectiles + 1) * 1.25f;
            Sizetemp = Sizetemp * (projectileCount + sideProjectiles + 1) * 1.25f;
            FlashSizeTemp = FlashSizeTemp * (projectileCount + sideProjectiles + 1) * 1.25f;
            InfernoLine.SetSize(LineSize, Sizetemp);

            InfernoFlashObj.transform.localScale = new Vector3(FlashSizeTemp * FlashSizeMultiplier, FlashSizeTemp * FlashSizeMultiplier, FlashSizeTemp * FlashSizeMultiplier);
        }

        if(Circle)
        {
            for (int i = 0; i < CubeList.Count; i++)
            {
                LaserTemp = CubeList[i].GetComponent<Hovl_Laser>();
                if(LaserTemp != null)
                {
                    LaserTemp.SetSize(LineSize,Sizetemp);
                }
            }
        }
    }

    public void ResetLasers()
    {
        LasersList.Clear();
    }

    public override void setFirepoints()
    {
        if(Circle)
        {
            ResetLasers();
            ResetFirePoints();

            //LineRenderer MainLineRend = MainLine.GetComponent<LineRenderer>();
            //MainLineRend.enabled = false; 
            //MainLine.enabled = false;

            int Amount = projectileCount + sideProjectiles + 1;
            float offset = 360f / Amount;
            float Temp = offset;

            for (int i = 0; i < Amount; i++)
            {
                GameObject CubeTemp;
                CubeTemp = Instantiate(cube,transform.position,Quaternion.Euler(0,0,offset));
                CubeTemp.transform.SetParent(transform);
                CubeList.Add(CubeTemp);
                LasersList.Add(CubeTemp.GetComponent<Hovl_Laser>());
                offset += Temp;
            }
        }
    }

    public void ResetSideLasers()
    {
        SideLasersList.Clear();
    }

    public override void setSideFirepoints()
    {
        ResetSideLasers();
        ResetSideFirePoints();

        if(Circle || Inferno || DronesFinal)
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

        for (int i = 0; i < sideProjectiles;i++)
        {
            GameObject CubeTemp;
            CubeTemp = Instantiate(cube, firePoint.position, Quaternion.Euler(0, 0, (offsetA * -1) + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
            SideLasersList.Add(CubeTemp.GetComponent<Hovl_Laser>());
            
            CubeTemp = Instantiate(cube, firePoint.position, Quaternion.Euler(0, 0, offsetA + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
            SideLasersList.Add(CubeTemp.GetComponent<Hovl_Laser>());
            offsetA += offset;
        }
    }

    public void SetUpDrones()
    {
        int Amount;
        if (DronesFinal == false)
        {
            Amount = DroneAmount;
        }else
        {
            Amount = DroneAmount + projectileCount + sideProjectiles ;
        }

        float Offset = 360f / Amount;
        float Temp = 0f;
        for (int i = 0; i < Amount; i++)
        {
            Vector3 Direction = new Vector3(Mathf.Sin(Temp * Mathf.Deg2Rad),Mathf.Cos(Temp * Mathf.Deg2Rad),0);
            Vector3 Point = Dronemanager.transform.position + (DroneDistance * Direction);
            GameObject Obj = Instantiate(DronePrefab, Point, Quaternion.Euler(0, 0, 0));
            Obj.transform.SetParent(Dronemanager.transform);
            Temp += Offset;
        }
        Dronemanager.ResetDrones(ASmultiplier); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MaxRange);
    }
}
