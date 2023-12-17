using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserLaser : MonoBehaviour
{
    CruiserBossAI bossAI;
    [SerializeField] Transform firePoint;
    [SerializeField] LineRenderer TargetingLine;
    [SerializeField] LineRenderer FireLine;
    [SerializeField] float TargetingTime;
    [SerializeField] float FireDuration;
    [SerializeField] float FireDelay;
    [SerializeField] int Damage;
    [SerializeField] float CoolingDuration;
    float timeStamp;
    Transform Player;
    

    public bool targeting;
    public bool locked;
    public bool fireing;
    public bool Cooling;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        bossAI = gameObject.GetComponent<CruiserBossAI>();
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        TargetingLine.enabled = false;
        FireLine.enabled = false;
        StartTargeting();
    }

    public void StartTargeting()
    {
        timeStamp = TargetingTime;
        targeting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (targeting)
        {
            TargetingLine.enabled = true;
            if (timeStamp > 0)
            {
                TargetingLine.SetPosition(0, firePoint.position);
                Vector3 temp = (Player.position - firePoint.position).normalized;
                TargetingLine.SetPosition(1, firePoint.position + temp * 100);
            }else
            {
                targeting = false;
                locked = true;
                timeStamp = FireDelay;
                pos = Player.position;
            }
        }

        if(locked)
        {
            if (timeStamp <= 0)
            {
                locked = false;
                fireing = true;
                TargetingLine.enabled = false;
                timeStamp = FireDuration;

                Vector3 temp = (Player.position - firePoint.position).normalized;
                RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, temp * 100);
                for (int i = 0; i < hitInfo.Length; i++)
                {
                    Debug.DrawRay(firePoint.position, temp * 100, Color.green, 100);

                    if (hitInfo[i].transform.GetComponent<plaerHealth>() != null)
                    {
                        plaerHealth Health = hitInfo[i].transform.GetComponent<plaerHealth>();
                        Health.TakeDamage(Damage);
                    }
                }
            }
        }

        if(fireing)
        {
            FireLine.enabled = true;

            if (timeStamp > 0)
            {
                FireLine.SetPosition(0,firePoint.position);
                Vector3 temp = (pos - firePoint.position).normalized;
                FireLine.SetPosition(1,firePoint.position + temp * 100);
            }
            else
            {
                FireLine.enabled=false;
                fireing = false;
                Cooling = true;
                timeStamp = CoolingDuration;
            }
        }

        if(Cooling)
        {
            if(timeStamp <= 0)
            {
                Cooling = false;
                targeting = true;
                timeStamp = TargetingTime;
            }
        }
    }
}
