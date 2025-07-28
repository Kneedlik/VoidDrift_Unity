using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTracker : MonoBehaviour
{
    Transform target;
    public Transform indicator;
    public Vector3 averageVel;
    public Vector3 averageAccel;

    private Vector3 prevPos;
    private Vector3 prevAccel;
    private Vector3 prevVel;


    private void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Check());
    }

    

    IEnumerator Check()
    {
        yield return new WaitForEndOfFrame();

        Vector3 Velocity = (target.position - prevPos) / Time.deltaTime;
        Vector3 Accel = Velocity - prevVel;

        averageVel = Velocity;
        averageAccel = Accel;

        getProjectedPos(1);

        prevPos = target.position;
        prevVel = Velocity;
        prevAccel = Accel;

    }

    public Vector3 getProjectedPos(float ftime)
    {
        Vector3 ret = target.position + (averageVel * Time.deltaTime *(ftime / Time.deltaTime)) + (0.5f * averageAccel * Time.deltaTime * Mathf.Pow(ftime / Time.deltaTime,2));
        indicator.position = ret;
        return ret;
    }
}
