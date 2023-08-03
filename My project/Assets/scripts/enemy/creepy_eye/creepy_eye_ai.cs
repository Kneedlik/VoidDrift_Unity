using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creepy_eye_ai : simpleAI
{

    public float alertRadius;
    public float breakAwayRadius;
  public  float endOfGridDistance;
    public float dashForce;
    public float dashTime;
    public float dashCoolDown;
   
    public int dashDamage;

    float timestamp;
   public bool dashing;
    new CircleCollider2D collider;
    public DetectColosion Fire;


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

        if(timestamp > 0)
        {
            timestamp -= Time.deltaTime;
        }

        if (dashing)
        {
            collider.isTrigger = true;
        }
        else collider.isTrigger = false;
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

        if(alert)
        {
            if(dashing == false)
            {
                currentTarget = target.position;
                lookAtRB(rb.velocity);
                moveCharacter(speed);
            }
            
            if(timestamp <= 0 && Fire.fire == true)
            {
                dashing = true;
                StartCoroutine(Dash());
                timestamp = dashCoolDown;
            }

            distance = Vector3.Distance(target.position, transform.position);
        }

        if(patrol)
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
        if (dashing && collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<plaerHealth>().TakeDamage(dashDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
        Gizmos.DrawWireSphere(transform.position, breakAwayRadius);
       
    }
}
