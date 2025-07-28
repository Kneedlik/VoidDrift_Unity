using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserBossAI : BaseAI
{
    [SerializeField] float PatrolSpeed;
    [SerializeField] float ChaseSpeed;
    [SerializeField] float MaxChaseSpeed;

    float distance;
    float wayPointDistance;
    float currentOffsetDistance;
    [SerializeField] float nextOffsetDistance;
    Transform Player;

    CruiserConstants TurretManager;
    CruiserLaser Laser;
    [SerializeField] CruiserMissles Missles;
    Rigidbody2D rb;

    [SerializeField] float chaseRadius;
    bool chase;
    bool switchingState;
    Vector3[] offset;
    [SerializeField] float offsetDistance;
    [SerializeField] float offsetDistanceShort;
    int offsetIndex;


    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Laser = GetComponent<CruiserLaser>();
        rb = GetComponent<Rigidbody2D>();
        wayPointIndex = 0;
        Invoke("FireMissiles", 1);
        switchingState = false;
        TurretManager = GetComponent<CruiserConstants>();
        offset = new Vector3[8];
    }

    void Update()
    {
        distance = Vector3.Distance(Player.position, transform.position);
        switchingState = false;

        if(patrol)
        {
            Laser.Active = false;
            Missles.Active = false;
            TurretManager.Active = false;

            wayPointDistance = Vector3.Distance(waypoints[wayPointIndex].position, transform.position);
            if(wayPointDistance <= nextWayPointDistance)
            {
                IncreasePartolIndex();
            }

            if (distance <= alertRadius)
            {
                alert = true;
                patrol = false;
                chase = false;
                switchingState = true;
            }

            KnedlikLib.LookAtSmooth(transform, waypoints[wayPointIndex].position, rotationSpeed);
        }

        if (alert)
        {
            if (distance >= breakAwayRadius)
            {
                alert = false;
                patrol = true;
                chase = false;
                return;
            }

            if (distance > chaseRadius)
            {
                if (chase == false)
                {
                    chase = true;
                    switchingState = true;
                }
            }
            else
            {
                if (chase == true)
                {
                    chase = false;
                    switchingState = true;
                }
            }    

            if(chase)
            {
                Laser.fireMode = 2;
                Laser.Active = true;
                TurretManager.Active = false;
                KnedlikLib.LookAtSmooth(transform, Player.position, rotationSpeed);

                if(distance < Missles.Range)
                {
                    Missles.FireMode = 2;
                    if(Missles.Active == false)
                    {
                        float Rand = Random.Range(0f, 3.5f);
                        Missles.Active = true;
                        Missles.timeStamp = Rand;
                    }
                }
            }else
            {
                offset[0] = new Vector3(Player.position.x + offsetDistance, Player.position.y,0);
                offset[1] = new Vector3(Player.position.x + offsetDistanceShort, Player.position.y - offsetDistanceShort, 0);
                offset[2] = new Vector3(Player.position.x, Player.position.y - offsetDistance, 0);
                offset[3] = new Vector3(Player.position.x - offsetDistanceShort, Player.position.y - offsetDistanceShort, 0);
                offset[4] = new Vector3(Player.position.x - offsetDistance, Player.position.y, 0);
                offset[5] = new Vector3(Player.position.x - offsetDistanceShort, Player.position.y + offsetDistanceShort, 0);
                offset[6] = new Vector3(Player.position.x, Player.position.y + offsetDistance, 0);
                offset[7] = new Vector3(Player.position.x + offsetDistanceShort, Player.position.y + offsetDistanceShort, 0);
                currentOffsetDistance = Vector3.Distance(offset[offsetIndex], transform.position);
                if(currentOffsetDistance < nextOffsetDistance)
                {
                    IncreaseOffsetIndex();
                }

                KnedlikLib.LookAtSmooth(transform, offset[offsetIndex], rotationSpeed);

                if (switchingState)
                {
                    int Rand = Random.Range(0, 7);
                    offsetIndex = Rand;
                }

                Laser.fireMode = 1;
                Missles.FireMode = 1;
                Missles.Active = true;
                //Laser.StartCooling();
                Laser.Active = true;
                TurretManager.Active = true;

                if(switchingState == true)
                {
                    float Rand1 = Random.Range(0,1f);
                    float Rand2 = Random.Range(Rand1 + 6f,Rand1 + 10f);
                    float Rand3 = Random.Range(Rand2 + 6f,Rand2 + 10f);
                    int order = Random.Range(1,6);

                    switch(order)
                    {
                        case 1:
                            TurretManager.ActivateTurrets(Rand1);
                            Missles.timeStamp = Rand2;
                            Laser.timeStamp = Rand3;
                            break;
                        case 2:
                            TurretManager.ActivateTurrets(Rand1);
                            Missles.timeStamp = Rand3;
                            Laser.timeStamp = Rand2;
                            break;
                        case 3:
                            TurretManager.ActivateTurrets(Rand2);
                            Missles.timeStamp = Rand1;
                            Laser.timeStamp = Rand3;
                            break;
                        case 4:
                            TurretManager.ActivateTurrets(Rand2);
                            Missles.timeStamp = Rand3;
                            Laser.timeStamp = Rand1;
                            break;
                        case 5:
                            TurretManager.ActivateTurrets(Rand3);
                            Missles.timeStamp = Rand1;
                            Laser.timeStamp = Rand2;
                            break;
                        case 6:
                            TurretManager.ActivateTurrets(Rand3);
                            Missles.timeStamp = Rand2;
                            Laser.timeStamp = Rand1;
                            break;
                    }
                }

            }

        }
    }

    private void FixedUpdate()
    {
        if (patrol)
        {
            rb.AddForce(transform.up * PatrolSpeed, ForceMode2D.Force);
            KnedlikLib.SetMaxSpeed(MaxPatrolSpeed,rb);
        }

        if(alert)
        {
            if(chase)
            {
                rb.AddForce(transform.up * ChaseSpeed, ForceMode2D.Force);
                KnedlikLib.SetMaxSpeed(MaxChaseSpeed, rb);
            }
            else
            {
                rb.AddForce(transform.up * speed, ForceMode2D.Force);
                KnedlikLib.SetMaxSpeed(maxSpeed, rb);
            }  
        }
    }

    public void IncreaseOffsetIndex()
    {
        offsetIndex++;
        if(offsetIndex > offset.Length)
        {
            offsetIndex = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
        Gizmos.DrawWireSphere(transform.position, breakAwayRadius);
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, Missles.Range);
    }


}
