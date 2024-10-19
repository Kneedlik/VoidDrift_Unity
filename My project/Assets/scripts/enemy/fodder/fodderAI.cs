using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fodderAI : simpleAI
{
    public fodderCannon cannon;
    public float rotSpeed;
    int rand;
    int rand1;
    bool perch;
    Vector2 lookDir;

     float timeStamp;
    public float perchTime;
    bool ready;
    public float endOfGridDistance;
    
    Vector3[] offset;
    public float offsetD;

    protected override void Start()
    {
        base.Start();
        alert = false;
        offset = new Vector3[4];
        
        offset[0] = new Vector3(target.position.x + offsetD,target.position.y,0);
        offset[1] = new Vector3(target.position.x - offsetD, target.position.y, 0);
        offset[2] = new Vector3(target.position.x, target.position.y + offsetD, 0);
        offset[3] = new Vector3(target.position.x, target.position.y - offsetD, 0);

        cannon.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= alertRadius)
        {
            cannon.enabled = true;
            alert = true;
            patrol = false;
            currentTarget = target.position;
        }


        if(alert)
        {
            float[] offsetDistance;
            offsetDistance = new float[4];

            offset[0] = new Vector3(target.position.x + offsetD, target.position.y, 0);
            offset[1] = new Vector3(target.position.x - offsetD, target.position.y, 0);
            offset[2] = new Vector3(target.position.x, target.position.y + offsetD, 0);
            offset[3] = new Vector3(target.position.x, target.position.y - offsetD, 0);

            for (int i = 0; i < 3; i++)
            {
                offsetDistance[i] = Vector3.Distance(transform.position, offset[i]);
            }
            sort(offsetDistance, 4, offset);

            if (rand1 == -1)
            {
                rand1 = Random.Range(0, 2);
               // Debug.Log(rand1);
            }

            if (rand1 == 0)
            {
                currentTarget = offset[1];
            }
            else if (rand1 == 1)
            {
                currentTarget = offset[2];
            }

            if (cannon.salvoFinished && ready)
            {
                ready = false;
                timeStamp = perchTime;
            }

            if(reachedEndOfPath)
            {
                cannon.currentBurst = cannon.numberOfBursts;
            }
        }

        if(distance > breakAwayRadius)
        {
            cannon.enabled = false;
            alert = false;
            patrol = true;
            if (waypoints[wayPointIndex] != null)
            {
                currentTarget = waypoints[wayPointIndex].position;
            }else
            {
                waypoints[wayPointIndex] = transform; 
            }

           
        }

        if (timeStamp >= 0)
        {
            perch = true;
            timeStamp -= Time.deltaTime;
        }
        else perch = false;
     
    }

    protected override void FixedUpdate()
    {
        if (path == null || currentTarget == null)
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

       // lookDir = path.vectorPath[currentWatPoint+3] ;
        if (alert)
        {

            if (cannon.salvoFinished == false)
            {
                moveCharacter(speed);
                //rb.AddForce(transform.up * speed, ForceMode2D.Force);
                lookAt(currentTarget);
                rand = -1;
                ready = true;
            }
            else
            {
               
                rb.AddForce(transform.up * speed * SpeedMultiplier, ForceMode2D.Force);
                if (rand == -1)
                {
                    rand = Random.Range(0, 2);
                    Debug.Log(rand);
                }

                if (perch)
                {
                    rand1 = -1;

                    if (rand == 0)
                    {
                        rb.rotation += rotSpeed;
                    }
                    else
                    {
                        rb.rotation -= rotSpeed;
                    }
                   
                }
            }
        }else if(patrol && distance < endOfGridDistance)
        {
            // Vector2 lookDir = ((Vector2)path.vectorPath[currentWatPoint] - rb.position).normalized;
            // Vector3 lookDir = path.vectorPath[currentWatPoint];
            lookDir = rb.velocity;


            if (waypoints[wayPointIndex] != null)
            {
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
        setMaxSpeed(maxSpeed);
    }

    private void LateUpdate()
    {
        setMaxSpeed(maxSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
        Gizmos.DrawWireSphere(transform.position, breakAwayRadius);
      
    }

    void sort(float[] numbers,int count, Vector3[] offsets)
    {
        float pom;
        Vector3 Pom; 
        int i, j;

        for(j= count -1;j>= 0;j--)
        {
            for(i=0; i < j;i++)
            {
                if (numbers[i] > numbers[i+1])
                {
                    pom = numbers[i];
                    numbers[i] = numbers[i + 1];
                    numbers[i + 1] = pom;

                    Pom = offset[i];
                    offset[i] = offset[i + 1];
                    offset[i + 1] = Pom;
                }
            }
        }
    }

    
}
