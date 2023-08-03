using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformPull : MonoBehaviour
{
    public float pullRadius;
    public float speed;
    
    Transform target;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pullRadius);   
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= pullRadius)
        {
            Vector3 a = transform.position;
            Vector3 b = target.position;
            transform.position = Vector3.MoveTowards(a,b,speed * Time.deltaTime);
          //  transform.Translate(pos * speed * Time.deltaTime);
        }
       
    }


}
