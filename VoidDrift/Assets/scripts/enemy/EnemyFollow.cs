using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    protected Transform target;
    public float speed;
    public float maxSpeed;
    public float Multiplier = 1f;
    protected Rigidbody2D rb; 

    protected Health health;
    protected StunOnHit Stun;

    void Start()
    {
       SetVars();
    }

    public void SetVars()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        health = GetComponent<Health>();
        Stun = GetComponent<StunOnHit>();

    }

    private void FixedUpdate()
    {
        bool StunTemp = false;
        if(Stun != null)
        {
            StunTemp = Stun.stop;
        }

        if (StunTemp == false)
        {
            Vector3 dir = target.position - transform.position;
            rb.AddForce(dir.normalized * speed * Multiplier);
            KnedlikLib.SetMaxSpeed(maxSpeed * Multiplier, rb);
        }
    }

    private void LateUpdate()
    {
        KnedlikLib.SetMaxSpeed(maxSpeed * Multiplier, rb);
    }
}
