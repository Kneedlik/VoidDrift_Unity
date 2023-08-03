using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody2D rb;
    public float rocketSpeed;
    public float destroyTime;
    public GameObject explosion;
    public int damage;
    float currentSpeed;
    public float speedGrowth;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = 0;
    }


    private void FixedUpdate()
    {
        if(currentSpeed < rocketSpeed)
        {
            currentSpeed += speedGrowth;
        }

        rb.AddForce(transform.up * currentSpeed,ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player" && collision.isTrigger == false)
        {
            if(collision.GetComponent<Health>() != null)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
            Instantiate(explosion, transform.position,transform.rotation);
            Destroy(gameObject);  
        } 
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
