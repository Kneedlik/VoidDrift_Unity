using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformPull : MonoBehaviour
{
    [SerializeField] bool UseConstants;
    public float pullRadius;
    public float speed;
    float TruePullRadius;
    float TrueSpeed;
    
    Transform target;
    void Start()
    {
        if(UseConstants)
        {
            TruePullRadius = Constants.ItemPullDistance;
            TrueSpeed = Constants.ItemPullSpeed;
        }else
        {
            TruePullRadius = pullRadius;
            TrueSpeed = speed;
        }

        target = GameObject.FindWithTag("Player").transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, TruePullRadius);   
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= TruePullRadius)
        {
            Vector3 a = transform.position;
            Vector3 b = target.position;
            transform.position = Vector3.MoveTowards(a,b,TrueSpeed * Time.deltaTime);
          //  transform.Translate(pos * speed * Time.deltaTime);
        }
       
    }


}
