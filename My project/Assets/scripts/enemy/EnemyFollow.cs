using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    Transform target;
    public float speed;
    public float maxSpeed;
    private Rigidbody2D rb; 
    private float pom = 30;

    Health health;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        health = GetComponent<Health>();
        
    }

    // Update is called once per frame
    void Update()
    {
       // Vector3 direction = player.position - transform.position;
      //  float distance = Vector3.Distance(player.position, transform.position);
      //  float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
  
       // direction.Normalize();
      //  movement = direction;

        
    }

    private void FixedUpdate()
    {
        if(health.stop == false)
        {
            Vector3 dir = target.position - transform.position;
           rb.AddForce(dir.normalized * speed * Time.deltaTime);
            setMaxSpeed(maxSpeed);
        }else
        {
            health.stop = false;
        }

    }

    void moveCharacter(Vector2 direction)
    {
        rb.AddForce(direction * speed, ForceMode2D.Force);
        setMaxSpeed(maxSpeed);
    }

    private void setMaxSpeed(float maxSpeed)
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, pom);
    }
}
