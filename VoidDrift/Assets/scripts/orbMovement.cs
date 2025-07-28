using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbMovement : MonoBehaviour

{
    private Rigidbody2D rb;
    public Transform parent;
    public float speed;
    int direction;
    public float maxDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        float distance;
        distance = transform.position.y - parent.position.y;

        if(distance >= maxDistance)
        {
            direction = 0;
        }

        if (distance <= maxDistance * -1)
        {
            direction = 1;
        }

        if(direction == 0)
        {
            rb.AddForce(new Vector3(0, -1, 0) * Time.deltaTime * speed);
        }else
        {
            rb.AddForce(new Vector3(0, 1, 0) * Time.deltaTime * speed);
        }
    }
}
