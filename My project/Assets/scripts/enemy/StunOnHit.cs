using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StunOnHit : MonoBehaviour
{
    public bool stop;
    public float StunDuration;
    float timeStamp;
    Rigidbody2D Rb;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

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

    public void Stun()
    {
        
        timeStamp = StunDuration;

        if (Rb != null && stop == false)
        {
            Rb.velocity = Rb.velocity.normalized;
        }
        stop = true;
    }

   
}
