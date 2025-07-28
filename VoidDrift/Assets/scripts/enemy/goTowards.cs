using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goTowards : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Transform target;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 dir = target.position - transform.position;
        
      //  dir =  dir.normalized;

       // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // rb.rotation = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.deltaTime);
      //  rb.rotation = angle;
      // transform.rotation = Quaternion.Euler(0,0,angle);

        

        rb.AddForce(dir.normalized * speed * Time.deltaTime);
       // transform.position = rb.position;
    }
}
