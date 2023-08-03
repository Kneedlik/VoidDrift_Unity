using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullLoot : MonoBehaviour
{
    public float radius;
    private Transform target;
    private bool pull = false;
    private Rigidbody2D rb;
    public bool rotate;
    public float rotationSpeed = 1;

    public float pullSpeed;
    public float maxPullSpeed;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        direction = target.position - transform.position;
        direction.Normalize();

        if(distance <= radius)
        {
            pull = true;
        }else pull = false;
    }

    private void FixedUpdate()
    {
        if(pull)
        {
            if(rotate)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.deltaTime);
                rb.AddForce(transform.right * pullSpeed * Time.deltaTime);
                setMaxSpeed(maxPullSpeed);
            }
            else
            {
                rb.AddForce(direction * pullSpeed, ForceMode2D.Force);
                setMaxSpeed(maxPullSpeed);
            }
            
        }
    }



    private void setMaxSpeed(float max)
    {
        if(rb.velocity.magnitude > max)
        {
            rb.velocity = rb.velocity.normalized * max;
        }
    }

   

}
