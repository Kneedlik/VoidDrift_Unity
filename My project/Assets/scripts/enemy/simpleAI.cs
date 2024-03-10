using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class simpleAI : BaseAI
{
    //path
    protected Path path;
    protected Seeker seeker;
    protected int currentWatPoint = 0;

    protected Transform target;
    public Vector3 currentTarget;
    protected float distance;
    public bool reachedEndOfPath = false;

    protected Rigidbody2D rb;


    protected virtual void Start()
    {
        seeker = GetComponent<Seeker>();
        target = GameObject.FindWithTag("Player").transform;
        currentTarget = target.position;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("updatePath", 0f, .5f);

        if(waypoints.Length == 0)
        {
            waypoints = new Transform[1];
            waypoints[0] = transform;
        }

    }

    protected void updatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, currentTarget, OnPathComplete);
        }
    }

    protected void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWatPoint = 0;
        }
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected void moveCharacter(float Speed)
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

    protected void moveUp()
    {
        rb.AddForce(speed * transform.up * Time.deltaTime);
    }

    protected void lookAt(Vector3 lookDir)
    {

       
      //  float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

       // rb.rotation = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.deltaTime);

       Quaternion rotTarget3D = Quaternion.LookRotation(lookDir - new Vector3(transform.position.x, transform.position.y));
        Quaternion rotTarget = Quaternion.Euler(0, 0, rotTarget3D.eulerAngles.y < 180 ? 270 - rotTarget3D.eulerAngles.x : rotTarget3D.eulerAngles.x - 270);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);

      //  transform.Rotate(lookDir,rotationSpeed * Time.deltaTime);


        //lookDir = target.transform.position;
        // Quaternion rotGoal = Quaternion.LookRotation(lookDir);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, rotationSpeed);
    }

    protected void lookAtRB(Vector2 lookDir)
    {
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        rb.rotation = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.deltaTime);
    }

    protected void setMaxSpeed(float speed)
    {
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    protected bool checkDistance(float distance)
    {

        float dis = Vector3.Distance(target.position, transform.position);

        if (dis > distance)
        {
            return true;
        }
        else return false;
    }

    protected void IncreaseIndex()
    {
        wayPointIndex++;
        if (wayPointIndex >= waypoints.Length)
        {
            wayPointIndex = 0;
        }
        currentTarget = waypoints[wayPointIndex].position;
    }

    protected void Patrol(float speed)
    {
        if(waypoints.Length > 0)
        {
            Vector2 lookDir = rb.velocity;

            distance = Vector2.Distance(rb.position, waypoints[wayPointIndex].position);
            if (distance < nextWayPointDistance)
            {
                IncreaseIndex();
            }

            //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

            lookAtRB(lookDir);
            moveCharacter(speed);

            if (distance < nextWayPointDistance)
            {
                currentWatPoint++;
            }
        }
    }

}
