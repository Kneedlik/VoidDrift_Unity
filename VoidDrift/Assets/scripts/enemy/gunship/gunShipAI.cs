using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShipAI : simpleAI
{
   
    public float patrolSpeed;
    public float currentSpeed;
    
    public float stopRadius;
    public float backRadius;
    Rigidbody2D targetRB;

    private bool stop = false;
    public bool back = false;
    public bool Ready;
  
    protected override void Start()
    {
        base.Start();
        Ready = false;
        
        wayPointIndex = 0;
        targetRB = target.GetComponent<Rigidbody2D>();
        currentTarget = waypoints[0].position;
        currentSpeed = speed / 2;
        wayPointIndex = 0;
    }

    protected override void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= alertRadius)
        {
            Ready = true;
            alert = true;
            patrol = false;
            currentTarget = target.position;
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

        if (alert && distance >= breakAwayRadius)
        {
            alert = false;
            patrol = true;
            currentTarget = waypoints[wayPointIndex].position;
        }
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        
        if (path == null)
        { return; }

        if (Active == false)
        {
            return;
        }

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
        if (patrol)
        {
           // && reachedEndOfPath == false && path != null

           // Vector2 lookDir = ((Vector2)path.vectorPath[currentWatPoint]).normalized;

            distance = Vector2.Distance(rb.position, waypoints[wayPointIndex].position);
            if (distance < nextWayPointDistance)
            {
                IncreaseIndex();
            }

            //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

            //lookAt(currentTarget);

            //moveCharacter(patrolSpeed);

            lookAt(waypoints[wayPointIndex].position);
            rb.AddForce(speed * transform.up, ForceMode2D.Force);

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

            if (stop)
            {
                if (rb.velocity.magnitude < targetRB.velocity.magnitude)
                {
                    currentSpeed += 30;
                }
                else
                {
                    currentSpeed -= 30;
                }

                if (currentSpeed > speed)
                {
                    currentSpeed = speed;
                }

                if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                }

                moveCharacter(currentSpeed);
            }

            if (back)
            {
                //  Vector2 direction = ((Vector2)path.vectorPath[currentWatPoint] - rb.position).normalized;
                Vector2 direction = ((Vector2)currentTarget - rb.position).normalized;
                Vector2 force = direction * speed;
                force.x = force.x * -1;
                force.y = force.y * -1;
                rb.AddForce(force, ForceMode2D.Force);

                if (currentWatPoint < path.vectorPath.Count)
                {
                    distance = Vector2.Distance(rb.position, path.vectorPath[currentWatPoint]); //

                    if (distance < nextWayPointDistance)
                    {
                        currentWatPoint++;
                    }
                }
            }

            Vector3 lookDir;
            //lookDir = target.localPosition - transform.position;
            lookDir = target.position;

            lookAt(lookDir);
        }
        KnedlikLib.SetMaxSpeed(maxSpeed,rb);
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
