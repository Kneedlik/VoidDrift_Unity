using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StunOnHit : MonoBehaviour
{
    public bool stop;
    public float StunDuration;
    float timeStamp;

    // Update is called once per frame
    void Update()
    {
        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (timeStamp <= 0)
        {
            stop = false;
        }
    }

    public void Stun(bool ZeroVelocity = false,Rigidbody2D rbTarget = null)
    {
        stop = true;
        timeStamp = StunDuration;

        if (ZeroVelocity && rbTarget != null)
        {
            rbTarget.velocity = rbTarget.velocity.normalized;
        }
    }

   
}
