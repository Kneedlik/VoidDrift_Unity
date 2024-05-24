
using UnityEngine;

public class MiningLaser : weapeon
{
    public LineRenderer lineRenderer;
    float timeStamp;

    private void Start()
    {
        SetUpWeapeon();
        timeStamp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            lineRenderer.enabled = true;
            Shoot();
        }else
        {
            lineRenderer.enabled = false;
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

            lineRenderer.SetPosition(0  , firePoint.position);
            lineRenderer.SetPosition(1, hitInfo[Index].point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.up * 100);
        }
    }

    void DealDamage()
    {
        int PierceTemp = pierce;
        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, firePoint.up);

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
}
