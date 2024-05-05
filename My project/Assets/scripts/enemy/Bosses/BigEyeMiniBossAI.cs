using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEyeMiniBossAI : MonoBehaviour
{
    public bool MapVariant = false;
    Transform target;
    public float speed;
    public float maxSpeed;
    public float reducedVelocity;
    [SerializeField] float SlowDuration;
    private Rigidbody2D rb;

    float timeStamp;
    Health health;
    public bool Slowed;
    float distance;
    bool aggro = false;
    [SerializeField] float AggroDistance;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        health = GetComponent<Health>();
        health.function += SlowDown;
        Slowed = false;
    }
    private void FixedUpdate()
    {
        if (MapVariant == false || aggro)
        {
            Vector3 dir = target.position - transform.position;
            rb.AddForce(dir.normalized * speed);

            if (Slowed == false)
            {
                KnedlikLib.SetMaxSpeed(maxSpeed, rb);
            }
            else
            {
                KnedlikLib.SetMaxSpeed(reducedVelocity, rb);
            }
        }
    }

    private void Update()
    {
        distance = Vector3.Distance(rb.position, target.position);
        if(MapVariant && distance < AggroDistance)
        {
            aggro = true;
        }

        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0 && Slowed)
        {
            Slowed = false;
        }
    }

    public void SlowDown(GameObject self,int damage,ref int Damage)
    {
        timeStamp = SlowDuration;
        Slowed = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AggroDistance);       
    }

}
