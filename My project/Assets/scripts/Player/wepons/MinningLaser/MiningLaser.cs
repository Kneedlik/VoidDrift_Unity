
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MiningLaser : weapeon
{
    public Hovl_Laser MainLine;
    float timeStamp;
    public float MaxLength = 30;
    public float AreaMultiplier;
    [HideInInspector] public List<GameObject> SideLaserList = new List<GameObject>();

    private void Start()
    {
        SetUpWeapeon();
        MainLine.MaxLength = MaxLength;
        timeStamp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //lineRenderer.enabled = true;
            MainLine.EnablePrepare();
            Shoot();
        }else
        {
            //lineRenderer.enabled = false;
            MainLine.DisablePrepare();
        }

        if (Input.GetButton("Fire1") && timeStamp <= 0)
        {
            timeStamp = CoolDown;
            DealDamage(firePoint,true);
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
                if(PierceTemp == 0)
                {
                    Infinite = false;
                    break;
                }else
                {
                    PierceTemp -= 1;
                }
            }

        }

        PierceTemp = pierce;
        if(Infinite == false)
        {
            int Index = 0;
            for (int i = 0; i < hitInfo.Length;i++)
            {
                if (hitInfo[i].transform.GetComponent<Health>() != null)
                {
                    Index = i;
                    if(PierceTemp <= 0)
                    {
                        break;
                    }else
                    {
                        PierceTemp -= 1;
                    }
                }
            }

            //lineRenderer.SetPosition(0  , firePoint.position);
            //lineRenderer.SetPosition(1, hitInfo[Index].point);
            MainLine.SetUp(firePoint.position, hitInfo[Index].point,false);
        }
        else
        {
            //lineRenderer.SetPosition(0, firePoint.position);
            //lineRenderer.SetPosition(1, firePoint.position + firePoint.up * MaxLength);
            MainLine.SetUp(firePoint.position, firePoint.position + firePoint.up * MaxLength,true);
        }
    }

    void DealDamage(Transform FirePoint,bool Main)
    {
        int PierceTemp = pierce;
        //RaycastHit2D[] hitInfo = Physics2D.RaycastAll(FirePoint.position, FirePoint.up);
        RaycastHit2D[] hitInfo = Physics2D.CapsuleCastAll(FirePoint.position, new Vector2(size, size),CapsuleDirection2D.Vertical, FirePoint.rotation.eulerAngles.z, FirePoint.up, MaxLength); ;

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


       // if(hitInfo1.transform.tag == "Enemy")
       // {
       //     hitInfo1.transform.GetComponent<Health>().TakeDamage(damage);
       // }
    }

    public override void updateSize(int multiplier)
    {
        size = (float)multiplier / 100f * baseSize;
        size = size * AreaMultiplier;

        float LineSize = (float)multiplier / 100f * AreaMultiplier;

        for (int i = 0; i < SideLaserList.Count; i++)
        {
            KnedlikLib.ScaleParticleByFloat(SideLaserList[i], LineSize, false);
        }

        LineSize = size * (projectileCount + 1);
        KnedlikLib.ScaleParticleByFloat(MainLine.gameObject, LineSize, false);

    }
}
