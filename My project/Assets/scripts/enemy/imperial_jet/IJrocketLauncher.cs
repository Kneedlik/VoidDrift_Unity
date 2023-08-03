using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IJrocketLauncher : MonoBehaviour
{
    public GameObject rocket;
    bool fire;
    public imperial_jet_ai ai;
    public float delay;
    public Transform[] firePoint;
    int firePointIndex;
    bool done;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ai.getVectorLocked())
        {
            done = false;
        }

        if(fire && ai.getVectorLocked() == false && done == false)
        {
            Instantiate(rocket, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation);
            increaseIndex();
            done = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fire = false;
        }
    }

    void increaseIndex()
    {
        firePointIndex++;

        if (firePointIndex >= firePoint.Length)
        {
            firePointIndex = 0;
        }
    }


}
