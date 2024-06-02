
using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MiningLaser : weapeon
{
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

    private void Start()
    {
        FlashBaseSize = FlashObj.transform.localScale.y;
        HitBaseSize = MainLine.HitEffect.transform.localScale.y;
        LineBaseSize = MainLine.Laser.widthMultiplier;

        SetUpWeapeon();
        MainLine.MaxLength = MaxLength;
        timeStamp = 0;
        setSideFirepoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            MainLine.EnablePrepare();
            for (int i = 0; i < SideCubeList.Count; i++)
            {
                Hovl_Laser LineTemp = SideCubeList[i].GetComponent<Hovl_Laser>();
                if(LineTemp != null)
                {
                    LineTemp.EnablePrepare();
                }
            }
            Shoot();
        }else
        {
            for (int i = 0; i < SideCubeList.Count; i++)
            {
                Hovl_Laser LineTemp = SideCubeList[i].GetComponent<Hovl_Laser>();
                if (LineTemp != null)
                {
                    LineTemp.DisablePrepare();
                }
            }
            MainLine.DisablePrepare();
        }

        if (Input.GetButton("Fire1") && timeStamp <= 0)
        {
            timeStamp = CoolDown;
            DealDamage();
        }

        if(timeStamp >= 0)
        {
            timeStamp -= Time.deltaTime;
        } 
    }

    void Shoot()
    {
        int PierceTemp = pierce;
        bool Infinite = true;
        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, firePoint.up);

        for (int i = 0; i < hitInfo.Length; i++)
        {
            if (hitInfo[i].transform.GetComponent<Health>() != null)
            {
                if (PierceTemp == 0)
                {
                    Infinite = false;
                    break;
                } else
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
                    } else
                    {
                        PierceTemp -= 1;
                    }
                }
            }

            MainLine.SetUp(firePoint.position, hitInfo[Index].point, false);
        }
        else
        { 
            MainLine.SetUp(firePoint.position, firePoint.position + firePoint.up * MaxLength,true);
        }

        for (int i = 0;i < SideCubeList.Count;i++)
        {
            PierceTemp = pierce;
            Infinite = true;

            hitInfo = Physics2D.RaycastAll(SideCubeList[i].transform.position, SideCubeList[i].transform.up);

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

                Hovl_Laser LineTemp = SideCubeList[i].GetComponent<Hovl_Laser>();
                if(LineTemp != null)
                {
                    LineTemp.SetUp(SideCubeList[i].transform.position, hitInfo[Index].point, false);
                }
            }
            else
            {
                Hovl_Laser LineTemp = SideCubeList[i].GetComponent<Hovl_Laser>();
                if (LineTemp != null)
                {
                    LineTemp.SetUp(SideCubeList[i].transform.position, SideCubeList[i].transform.position + SideCubeList[i].transform.up * MaxLength, false);
                } 
            }
        }

    }

    void DealDamage()
    {
        int PierceTemp = pierce;
        //RaycastHit2D[] hitInfo = Physics2D.RaycastAll(FirePoint.position, FirePoint.up);
        RaycastHit2D[] hitInfo = Physics2D.CapsuleCastAll(firePoint.position, new Vector2(size, size),CapsuleDirection2D.Vertical, firePoint.rotation.eulerAngles.z, firePoint.up, MaxLength); 

        for (int i = 0;i < hitInfo.Length;i++)
        {
            Health health = hitInfo[i].transform.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                if(PierceTemp <= 0)
                {
                    break;
                }else
                {
                    PierceTemp -= 1;
                }
            }
        }

        for (int i = 0; i < SideCubeList.Count;i++)
        {
            PierceTemp = pierce;
            hitInfo = Physics2D.CapsuleCastAll(SideCubeList[i].transform.position, new Vector2(size, size), CapsuleDirection2D.Vertical, SideCubeList[i].transform.rotation.eulerAngles.z, SideCubeList[i].transform.up, MaxLength);

            for (int j = 0; j < hitInfo.Length; j++)
            {
                Health health = hitInfo[j].transform.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
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
    }

    public override void updateSize(int multiplier)
    {
        size = (float)multiplier / 100f * baseSize;
        float LineSize = (float)multiplier / 100f * baseSize;

        float Sizetemp = HitBaseSize * size;
        float FlashSizeTemp = FlashBaseSize * size;
        LineSize = LineBaseSize * LineSize;

        Hovl_Laser LaserTemp;

        for (int i = 0; i < SideCubeList.Count; i++)
        {
            LaserTemp = SideCubeList[i].GetComponent<Hovl_Laser>();
            if(LaserTemp != null)
            {
                LaserTemp.SetSize(LineSize,Sizetemp);
            }
        }

        LineSize = size * (projectileCount + 1);
        Sizetemp = Sizetemp * (projectileCount + 1);
        FlashSizeTemp = FlashSizeTemp * (projectileCount + 1);
        MainLine.SetSize(LineSize,Sizetemp);
        FlashObj.transform.localScale = new Vector3(FlashSizeTemp,FlashSizeTemp,FlashSizeTemp);
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

        for (int i = 0; i < sideProjectiles;i++)
        {
            GameObject CubeTemp;
            CubeTemp = Instantiate(cube, firePoint.position, Quaternion.Euler(0, 0, (offsetA * -1) + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
            
            CubeTemp = Instantiate(cube, firePoint.position, Quaternion.Euler(0, 0, offsetA + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
            offsetA += offset;
        }
    }
}
