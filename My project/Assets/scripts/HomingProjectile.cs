using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    Transform target;
    public float force;
    public float rotSpeed;
    public int damage;
    
    Rigidbody2D rb;
    public float destroyTime;
   // public bool isTrigger = true;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        // Quaternion rotTarget = Quaternion.LookRotation(target.position - transform.position);
        Quaternion rotTarget3D = Quaternion.LookRotation(target.position - new Vector3(transform.position.x, transform.position.y));
        Quaternion rotTarget = Quaternion.Euler(0, 0, rotTarget3D.eulerAngles.y < 180 ? 270 - rotTarget3D.eulerAngles.x : rotTarget3D.eulerAngles.x - 270);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotSpeed * Time.deltaTime);
        

        Destroy(gameObject, destroyTime);
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * force * Time.deltaTime,ForceMode2D.Force); 
    }

  //  private void OnTriggerEnter2D(Collider2D collision)
  //  {
  //
    //        if (collision.transform.tag != "Enemy" && collision.isTrigger == false)
    //        {
     //           if (collision.transform.tag == "Player")
     //           {
     //               collision.gameObject.GetComponent<plaerHealth>().TakeDamage(damage);
     //           }
//
    //            rb.velocity = Vector2.zero;
    //
     //           Destroy(gameObject);
    //        }
        
  //  }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Enemy")
        {
            if (collision.transform.tag == "Player")
            {
                if(damage > 0)
                {
                    collision.gameObject.GetComponent<plaerHealth>().TakeDamage(damage);
                }
            }

            rb.velocity = Vector2.zero;

            gameObject.GetComponent<DropLootOnDeath>().DropLoot();
            Destroy(gameObject);
        }
    }


}
