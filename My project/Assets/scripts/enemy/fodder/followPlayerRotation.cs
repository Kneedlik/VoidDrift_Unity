using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerRotation : MonoBehaviour
{
    Transform target;
    Rigidbody2D rb;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookDir;
        lookDir = target.localPosition - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        rb.rotation = angle;
    }
}
