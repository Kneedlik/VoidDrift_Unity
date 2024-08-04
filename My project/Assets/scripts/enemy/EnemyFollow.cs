using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    Transform target;
    public float speed;
    public float maxSpeed;
    public float Multiplier = 1f;
    private Rigidbody2D rb; 
    private float pom = 30;

    Health health;
    StunOnHit Stun;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        health = GetComponent<Health>();
        Stun = GetComponent<StunOnHit>();  
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
        KnedlikLib.SetMaxSpeed(maxSpeed, rb);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, pom);
    }
}
