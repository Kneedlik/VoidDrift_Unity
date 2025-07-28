using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private Transform currentTarget;
   
    public float speed = 200;
    public float patrolSpeed;
    public float currentSpeed;
    public float nextWayPointDistance = 3f;
    public float alertRadius;
    public float rotationLerp;

    public float stopRadius;
    public float backRadius;
    public float breakAwayRadius;

    Path path;
    int currentWatPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Rigidbody2D targetRB;

    private bool alert = false;
    private bool stop = false;
    private bool back = false;

    //patrol
    private bool patrol = true;
    public Transform[] waypoints;
    private int wayPointIndex;
    



    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        wayPointIndex = 0;
        targetRB = target.GetComponent<Rigidbody2D>();

        currentTarget = waypoints[0];
        InvokeRepeating("updatePath", 0f, .5f);
        currentSpeed = speed / 2;
    }

    void updatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, currentTarget.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWatPoint = 0;    
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= alertRadius)
        {
            alert = true;
            patrol = false;
            currentTarget = target;
        }

        if (distance <= stopRadius)
        {
            stop = true;
        }
        else stop = false;

        if (distance <= backRadius)
        {
            back = true;
        }
        else back = false;

        if(alert && distance >= breakAwayRadius)
        {
            alert = false;
            patrol = true;
            currentTarget = waypoints[wayPointIndex];
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        { return; }

        if (currentWatPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        float distance;
        if(patrol)
        {
            
                Vector2 lookDir = ((Vector2)path.vectorPath[currentWatPoint] - rb.position).normalized;

                distance = Vector2.Distance(rb.position, waypoints[wayPointIndex].position);
                if (distance < nextWayPointDistance)
                {
                    IncreaseIndex();
                }

                //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

                lookAt(lookDir);

                moveCharacter(patrolSpeed);

                if (distance < nextWayPointDistance)
                {
                    currentWatPoint++;
                }  
        }

        if (alert)
        {
            if (stop == false)
            {
                moveCharacter(speed);
            }

            if(stop)
            {
                
                if (rb.velocity.magnitude < targetRB.velocity.magnitude)
                {
                    currentSpeed += 30;
                }else
                {
                    currentSpeed -= 30;
                }

                if(currentSpeed > speed)
                {
                    currentSpeed = speed;
                }

                if(currentSpeed < 0)
                {
                    currentSpeed = 0;
                }

                moveCharacter(currentSpeed);
            }

            if(back)
            {
                Vector2 direction = ((Vector2)path.vectorPath[currentWatPoint] - rb.position).normalized;
                Vector2 force = direction * speed * Time.deltaTime;
                force.x = force.x * -1;
                force.y = force.y * -1;
                rb.AddForce(force, ForceMode2D.Force);


                 distance = Vector2.Distance(rb.position, path.vectorPath[currentWatPoint]);

                if (distance < nextWayPointDistance)
                {
                    currentWatPoint++;
                }
            }

            Vector2 lookDir;
            lookDir = target.localPosition - transform.position;

            lookAt(lookDir);
        }
    }

    void lookAt(Vector2 lookDir)
    {
        
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        rb.rotation = Mathf.LerpAngle(rb.rotation, angle, rotationLerp);
    }

    void IncreaseIndex()
    {
        wayPointIndex++;
        if(wayPointIndex >= waypoints.Length)
        {
            wayPointIndex = 0;
        }
        currentTarget = waypoints[wayPointIndex];
    }

    void moveCharacter(float Speed)
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWatPoint] - rb.position).normalized;
        Vector2 force = direction * Speed * Time.deltaTime;
        rb.AddForce(force, ForceMode2D.Force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWatPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWatPoint++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
        Gizmos.DrawWireSphere(transform.position, stopRadius);
        Gizmos.DrawWireSphere(transform.position, backRadius);
        Gizmos.DrawWireSphere(transform.position, breakAwayRadius);
    }

    public bool getAlert()
    {
        return alert;
    }
}
