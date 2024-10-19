using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;
    public float maxSpeed;
    public float SpeedMultiplier = 1;
    public float rotationSpeed;
    public float nextWayPointDistance = 3f;
    protected int wayPointIndex;

    public bool patrol = true;
    public bool alert;

    public float alertRadius;
    public float breakAwayRadius;

    public void IncreasePartolIndex()
    {
        wayPointIndex++;
        if (wayPointIndex >= waypoints.Length)
        {
            wayPointIndex = 0;
        }
    }
}
