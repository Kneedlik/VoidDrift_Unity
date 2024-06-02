using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : BulletScript
{
    public float rocketSpeed;
    public GameObject explosion;
    float currentSpeed;
    public float speedGrowth;
    public float maxSpeed;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        damage = damagePlus;
        Destroy(gameObject, destroyTime);
        currentSpeed = 0;
    }


    private void FixedUpdate()
    {
        if(currentSpeed < rocketSpeed)
        {
            currentSpeed += speedGrowth;
        }

        rb.AddForce(transform.up * currentSpeed,ForceMode2D.Force);
        KnedlikLib.SetMaxSpeed(maxSpeed, rb);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player" && collision.isTrigger == false && collision.GetComponent<Health>() != null)
        {
            DealDamageToEmemy(collision);
            Debug.Log("Alive");
            if(explosion == null)
            {
                Debug.Log("0000");
            }

            Instantiate(explosion,transform.position,transform.rotation);
            Destroy(gameObject);
        } 
    }
}
