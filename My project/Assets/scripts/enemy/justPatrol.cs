using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    int wayPointIndex;
    public float nextWayPointDistance;
    Rigidbody2D rb;
    public float speed;
    public float MaxSpeed;
    public float rotationSpeed;
    public bool StopAtEnd;
    bool Stop;

    protected void Start()
    {
        Stop = false;
        wayPointIndex = 0;
        rb = GetComponent<Rigidbody2D>();
    }

   

    protected void FixedUpdate()
    {
        if (Stop == false)
        {
            rb.AddForce(transform.up * speed, ForceMode2D.Force);
        }
        //lookAtRB( new Vector2(waypoints[wayPointIndex].position.x, waypoints[wayPointIndex].position.y));
        lookAt(waypoints[wayPointIndex].position);

       float distance = Vector2.Distance(rb.position, waypoints[wayPointIndex].position);
        if (distance < nextWayPointDistance)
        {
            IncreaseIndex();
        }

        KnedlikLib.SetMaxSpeed(MaxSpeed,rb);
    }

    protected void IncreaseIndex()
    {
        wayPointIndex++;
        if (wayPointIndex >= waypoints.Length)
        {
            wayPointIndex = 0;
            if (StopAtEnd)
            {
                Stop = true;
            }
        }
    }

    void lookAtRB(Vector2 lookDir)
    {
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        rb.rotation = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.deltaTime);
    }

    void lookAt(Vector3 lookDir)
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
}
