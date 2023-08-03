using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class small_eye_ai : simpleAI
{
    public float alertRadius;
    public float breakAwayRadius;
    public float endOfGridDistance;
    public float dashForce;
    public float dashTime;
    public float dashCoolDown;

    public int dashDamage;
    public float blowBack;
    public float recoverTime;
    public float spinSpeed;
    public GameObject[] lights;

    float timestamp;
    float timestamp2;
    bool dashing;
    bool recovering;
    CircleCollider2D collider;
    public DetectColosion Fire;

    public GameObject explosion;


    protected override void Start()
    {
        base.Start();
        collider = GetComponent<CircleCollider2D>();
    }


    protected override void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= alertRadius)
        {
            alert = true;
            patrol = false;
            currentTarget = target.position;

        }

        if (alert && distance >= breakAwayRadius)
        {
            alert = false;
            patrol = true;
            currentTarget = waypoints[wayPointIndex].position;
        }

        if (timestamp > 0)
        {
            timestamp -= Time.deltaTime;
        }

        if(timestamp2 > 0)
        {
            timestamp2 -= Time.deltaTime;
        }

        if (dashing)
        {
            collider.isTrigger = true;
        }
        else collider.isTrigger = false;

        if(timestamp2 <= 0)
        {
            recovering = false;
        }else recovering = true;
    }

    protected override void FixedUpdate()
    {
        if (path == null)
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

        if (alert)
        {
            if (dashing == false && recovering == false)
            {
                currentTarget = target.position;
                lookAtRB(rb.velocity);
                moveCharacter(speed);
            }

            if (timestamp <= 0 && Fire.fire == true)
            {
                dashing = true;
                StartCoroutine(Dash());
                timestamp = dashCoolDown;
            }

            if(recovering)
            {
                rb.rotation += spinSpeed;
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(false);
                }
            }else
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(true);
                }
            }

            distance = Vector3.Distance(target.position, transform.position);
        }

        if (patrol)
        {
            Patrol(speed);
            lookAtRB(rb.velocity);
        }

    }

    IEnumerator Dash()
    {
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        Vector2 force = direction * dashForce * Time.deltaTime;

        rb.AddForce(force, ForceMode2D.Impulse);
        timestamp = dashTime;


        yield return new WaitForSeconds(dashTime);
        setMaxSpeed(maxSpeed);
        dashing = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dashing && collision.transform.tag == "Player" && recovering == false)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            setMaxSpeed(0);
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            rb.AddForce(-dir * blowBack, ForceMode2D.Impulse);
            timestamp2 = recoverTime;
            dashing = false;  
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
        Gizmos.DrawWireSphere(transform.position, breakAwayRadius);

    }
}
