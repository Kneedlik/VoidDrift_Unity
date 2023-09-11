using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissableOnDistance : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float distance;

    void Start()
    {
        
    }

    
    void Update()
    {
        foreach (Transform e in transform)
        {
            if(e.gameObject.activeSelf)
            {
                if(Vector3.Distance(player.position,e.position) > distance)
                {
                    e.gameObject.SetActive(false);
                }

            }else if(e.gameObject.activeSelf == false)
            {
                if(Vector3.Distance(player.position,e.position) < distance)
                {
                    e.gameObject.SetActive(true);
                    Health health = e.gameObject.GetComponent<Health>();
                    if(health != null)
                    {
                        health.setUp();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.position, distance);
    }
}
