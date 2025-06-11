using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SaucerAI : BaseAI
{
    CruiserConstants TurretManager;
    Rigidbody2D Rigidbody;
    int OnCoolDown;
    public float timeStamp;
    bool Finished;
    bool Flip;
    float distance;
    Transform Player;
    float wayPointDistance;
    bool chase;
    bool stop;
    [SerializeField] float RandMin;
    [SerializeField] float RandMax;

    [SerializeField] float PatrolSpeed;
    [SerializeField] float ChaseSpeed;
    [SerializeField] float MaxChaseSpeed;
    [SerializeField] float ChaseRadius;
    [SerializeField] float StopRadius;

    [SerializeField] Transform Centre;

    //Attack1
    [SerializeField] int ProjectileAmountA1;
    [SerializeField] float ProjectileForceA1;
    [SerializeField] float WaweDelayA1;
    [SerializeField] int WaweCountA1;
    [SerializeField] GameObject BulletPrefabA1;
    [SerializeField] float CooldownA1;
    [SerializeField] int DamageA1;
    [SerializeField] float ProjectileDistanceA1;

    //Attack2
    [SerializeField] float CooldownA2;
    [SerializeField] float WaweDelayA2;
    [SerializeField] float WaweCountA2;
    [SerializeField] GameObject MissilePrefabA2;
    [SerializeField] List<Transform> FirePointsA2 = new List<Transform>();

    //Attack3
    [SerializeField] float DurationA3;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        TurretManager = GetComponent<CruiserConstants>();
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Finished = true;
        //TurretManager.Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.position, Centre.position);
        wayPointDistance = Vector3.Distance(waypoints[wayPointIndex].position, transform.position);

        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(alert)
        {
            KnedlikLib.LookAtSmooth(transform, Player.position, rotationSpeed);

            if (distance >= breakAwayRadius)
            {
                alert = false;
                patrol = true;
                chase = false;
                stop = false;
                return;
            }

            if (distance > ChaseRadius)
            {
                if (chase == false)
                {
                    chase = true;
                    stop = false;
                }
            }
            else if (distance < StopRadius)
            {
                if (stop == false)
                {
                    stop = true;
                    chase = false;
                }
            }else
            {
                stop = false;
                chase = false;
            }

            if (timeStamp <= 0 && Finished)
            {
                DecideAttacl();
            }
        }
        else
        {
            KnedlikLib.LookAtSmooth(transform, waypoints[wayPointIndex].position, rotationSpeed);
            if (wayPointDistance <= nextWayPointDistance)
            {
                IncreasePartolIndex();
            }

            if (distance <= alertRadius)
            {
                alert = true;
                patrol = false;
                chase = false;
                stop = false;
            }

        }
    }

    void FixedUpdate()
    {
        if (patrol)
        {
            Rigidbody.AddForce(transform.up * PatrolSpeed, ForceMode2D.Force);
            KnedlikLib.SetMaxSpeed(MaxPatrolSpeed, Rigidbody);
        }

        if (alert)
        {
            if (chase)
            {
                Rigidbody.AddForce(transform.up * ChaseSpeed, ForceMode2D.Force);
                KnedlikLib.SetMaxSpeed(MaxChaseSpeed, Rigidbody);
            }
            else if (stop == false)
            {
                Rigidbody.AddForce(transform.up * speed, ForceMode2D.Force);
                KnedlikLib.SetMaxSpeed(maxSpeed, Rigidbody);
            }
        }
    }

    public void DecideAttacl()
    {
        int Rand;

        switch (OnCoolDown)
        {
            case 0:
                Rand = Random.Range(1, 4);
                if (Rand == 1)
                {
                    Attack1();
                    OnCoolDown = 1;
                }
                else if (Rand == 2)
                {
                    Attack2();
                    OnCoolDown = 2;
                }
                else if (Rand == 3)
                {
                    Attack3();
                    OnCoolDown = 3;
                }
                break;
            case 1:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    Attack2();
                    OnCoolDown = 2;
                }
                else if (Rand == 2)
                {
                    Attack3();
                    OnCoolDown = 3;
                }
                break;
            case 2:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    Attack1();
                    OnCoolDown = 1;
                }
                else if (Rand == 2)
                {
                    Attack3();
                    OnCoolDown = 3;
                }
                break;
            case 3:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    Attack1();
                    OnCoolDown = 1;
                }
                else if (Rand == 2)
                {
                    Attack2();
                    OnCoolDown = 2;
                }
                break;

        }
    }

    IEnumerator Attack1Corutine()
    {
        Finished = false;

        for (int i = 0; i < WaweCountA1; i++)
        {
            float angle = 360 / ProjectileAmountA1;
            float pom = angle;

            if (Flip)
            {
                angle += pom / 2;
                Flip = false;
            }
            else
            {
                Flip = true;
            }

            for (int j = 0; j < ProjectileAmountA1; j++)
            {
                GameObject B = Instantiate(BulletPrefabA1, Centre.position, Quaternion.Euler(0, 0, angle));
                B.GetComponent<foddeBullet>().damage = DamageA1;
                Rigidbody2D RB = B.GetComponent<Rigidbody2D>();
                B.transform.position = B.transform.position + B.transform.up * ProjectileDistanceA1;
                RB.velocity = B.transform.up * ProjectileForceA1;

                angle += pom;
            }

            yield return new WaitForSeconds(WaweDelayA1);
        }

        float rand = Random.Range(RandMin, RandMax);
        timeStamp = CooldownA1 + rand;
        Finished = true;
    }

    public void Attack1()
    {
        StartCoroutine(Attack1Corutine());
    }

    IEnumerator Attack2Corutine()
    {
        Finished = false;

        for (int i = 0;i < WaweCountA2; i++)
        {
            for (int j = 0;j < FirePointsA2.Count; j++)
            {
                Instantiate(MissilePrefabA2, FirePointsA2[j].position, FirePointsA2[j].rotation);
            }
        
            yield return new WaitForSeconds(WaweDelayA2);
        }

        float rand = Random.Range(RandMin, RandMax);
        timeStamp = CooldownA1 + rand;
        Finished = true;
    }

    public void Attack2()
    {
        StartCoroutine(Attack2Corutine());
    }

    IEnumerator Attack3Corutine()
    {
        Finished=false;
        TurretManager.Active = true;
        TurretManager.ActivateTurrets(0);

        yield return new WaitForSeconds(DurationA3);

        TurretManager.Active = false;
        Finished = true;
    }

    public void Attack3()
    {
        StartCoroutine(Attack3Corutine());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Centre.position, alertRadius);
        Gizmos.DrawWireSphere(Centre.position, breakAwayRadius);
        Gizmos.DrawWireSphere(Centre.position, ChaseRadius);
        Gizmos.DrawWireSphere(Centre.position, StopRadius);
    }
}
