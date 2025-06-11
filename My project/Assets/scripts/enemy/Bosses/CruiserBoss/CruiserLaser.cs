using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserLaser : MonoBehaviour
{
    public bool Active;
    CruiserBossAI bossAI;
    [SerializeField] Transform firePoint;
    [SerializeField] LineRenderer TargetingLine;
    [SerializeField] LineRenderer FireLine;
    [SerializeField] float TargetingTime;
    [SerializeField] float FireDuration;
    [SerializeField] float FireDelay;
    [SerializeField] int Damage;
    [SerializeField] float CoolingDuration;
    [SerializeField] float CoolingDurationAlternative;
    public int fireMode = 1;
    public float timeStamp;
    Transform Player;

    float width;
    bool widthDone;
    

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
        width = FireLine.widthMultiplier;
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

        if (targeting && Active)
        {
            TargetingLine.enabled = true;
            if (timeStamp > 0)
            {
                TargetingLine.SetPosition(0, firePoint.position);
                Vector3 temp = (Player.position - firePoint.position).normalized;
                TargetingLine.SetPosition(1, firePoint.position + temp * 1000);
            }else
            {
                targeting = false;
                locked = true;
                Vector3 velocity = Player.GetComponent<Rigidbody2D>().velocity;
                timeStamp = FireDelay;
                pos = Player.position + velocity * FireDelay;

                TargetingLine.SetPosition(0, firePoint.position);
                Vector3 temp = (pos - firePoint.position).normalized;
                TargetingLine.SetPosition(1, firePoint.position + temp * 1000);
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
                FireLine.widthMultiplier = 0;
                widthDone = false;

                Vector3 temp = (pos - firePoint.position).normalized;
                RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, temp * 1000);
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
                FireLine.SetPosition(1,firePoint.position + temp * 1000);

                if (widthDone == false)
                {
                    if (FireLine.widthMultiplier < width)
                    {
                        FireLine.widthMultiplier = FireLine.widthMultiplier + ((width / FireDuration) * (Time.deltaTime * 2));
                    }
                    else
                    {
                        widthDone = true;
                    }

                    //Debug.Log(FireLine.widthMultiplier);
                }
                else if (fireing && widthDone)
                {
                    FireLine.widthMultiplier = FireLine.widthMultiplier - ((width / FireDuration) * (Time.deltaTime * 2));
                }

            }
            else
            {
                FireLine.enabled=false;
                fireing = false;
                Cooling = true;

                if(fireMode == 1)
                {
                    float Rand = Random.Range(0f,5f);
                    timeStamp = CoolingDuration + Rand;
                }
                else if(fireMode == 2)
                {
                    float Rand = Random.Range(0f,1.5f);
                    timeStamp = CoolingDurationAlternative + Rand;
                }    
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

    public void Activate()
    {
        Cooling = false;
        targeting = true;
        fireing = false;
        locked = false;
    }

    public void StartCooling()
    {
        Cooling = true;
        targeting = false;
        fireing = false;
        locked = false;
    }

    public bool InterceptionPoint(Vector2 Target,Vector2 firePoint,Vector2 TargetVelocity,float time,out Vector2 result)
    {
        result = Vector2.zero;
        return true;
    }
}
