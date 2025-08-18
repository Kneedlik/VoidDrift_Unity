using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imperial_jet_ai : simpleAI
{
    public projectileCannon cannon;
    public IJrocketLauncher launcher;
    
    int rand1;
    Vector2 lookDir;

    public float perchDistance;
    public float minDistance;
    bool vectorLocked;
    bool targetSet;
    bool lineSet;

    Vector3[] offset;
    public float offsetD;

    public Health[] alliedHealth;
    int[] currentHealth;
    

    protected override void Start()
    {
        base.Start();
        alert = false;
        offset = new Vector3[4];
        currentHealth = new int[alliedHealth.Length];

        for (int i = 0; i < alliedHealth.Length; i++)
        {
            currentHealth[i] = alliedHealth[i].maxHealth;
        }
       

        offset[0] = new Vector3(target.position.x + offsetD, target.position.y, 0);
        offset[1] = new Vector3(target.position.x - offsetD, target.position.y, 0);
        offset[2] = new Vector3(target.position.x, target.position.y + offsetD, 0);
        offset[3] = new Vector3(target.position.x, target.position.y - offsetD, 0);
        targetSet = false;
    }

    // Update is called once per frame
    protected override void Update()
    {

        distance = Vector3.Distance(target.position, transform.position);

        for (int i = 0; i < alliedHealth.Length; i++)
        {
            if (alliedHealth[i].health < currentHealth[i])
            {
                alert = true;
                patrol = false;
                currentHealth[i] = alliedHealth[i].health;
                cannon.enabled = true;
                launcher.enabled = true;
            }
        }

        if (alert)
        {
            float[] offsetDistance;
            offsetDistance = new float[4];

            offset[0] = new Vector3(target.position.x + offsetD, target.position.y, 0);
            offset[1] = new Vector3(target.position.x - offsetD, target.position.y, 0);
            offset[2] = new Vector3(target.position.x, target.position.y + offsetD, 0);
            offset[3] = new Vector3(target.position.x, target.position.y - offsetD, 0);

            if (targetSet == false)
            {

                for (int i = 0; i < 3; i++)
                {
                    offsetDistance[i] = Vector3.Distance(transform.position, offset[i]);
                }
                sort(offsetDistance, 4, offset);
                rand1 = Random.Range(0, 2);

            }

            if (vectorLocked)
            {
                if (lineSet == false)
                {
                    currentTarget = transform.up * 100;
                    lineSet = true;
                }

                if (distance > perchDistance)
                {
                    vectorLocked = false;
                    targetSet = false;
                }


            }
            else if (vectorLocked == false)
            {

                if (rand1 == 0)
                {
                    currentTarget = offset[1];
                }
                else if (rand1 == 1)
                {
                    currentTarget = offset[2];
                }
                targetSet = true;

                if (distance < minDistance)
                {
                    vectorLocked = true;
                    lineSet = false;
                }
            }
        }
        else
        {
            cannon.enabled = false;
            launcher.enabled = false;
            currentTarget = waypoints[wayPointIndex].position;
        }

        if (distance > breakAwayRadius)
        {
            alert = false;
            patrol = true;
            currentTarget = waypoints[wayPointIndex].position;
        }   
    }

    protected override void FixedUpdate()
    {
        if (patrol)
        {
            // Vector2 lookDir = ((Vector2)path.vectorPath[currentWatPoint] - rb.position).normalized;
            // Vector3 lookDir = path.vectorPath[currentWatPoint];
            //lookDir = rb.velocity;

            distance = Vector2.Distance(rb.position, waypoints[wayPointIndex].position);
            if (distance < nextWayPointDistance)
            {
                IncreaseIndex();
            }else
            {
                rb.AddForce(speed * transform.up, ForceMode2D.Force);
            }

            //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

            lookAt(waypoints[wayPointIndex].position);
            //moveCharacter(speed);

            if (distance < nextWayPointDistance)
            {
                currentWatPoint++;
            }
        }

        setMaxSpeed(maxSpeed);

        if (path == null)
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

        // lookDir = path.vectorPath[currentWatPoint+3] ;
        

        if (alert)
        {                
            moveCharacter(speed);
            lookAt(currentTarget);              
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, breakAwayRadius);
        Gizmos.DrawWireSphere(transform.position, perchDistance);
    }

    void sort(float[] numbers, int count, Vector3[] offsets)
    {
        float pom;
        Vector3 Pom;
        int i, j;

        for (j = count - 1; j >= 0; j--)
        {
            for (i = 0; i < j; i++)
            {
                if (numbers[i] > numbers[i + 1])
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

    public bool getVectorLocked()
    {
        return vectorLocked;
    }

    public void SetAlert(bool Set)
    {
        alert = Set;    
    }

    public void SetPatrol(bool Set)
    {
        patrol = Set;
    }
}
