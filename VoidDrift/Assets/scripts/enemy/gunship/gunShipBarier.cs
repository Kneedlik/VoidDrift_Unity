using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShipBarier : MonoBehaviour
{
   
    public Transform firePoint1;
    public Transform firePoint2;
    public LineRenderer line1;

    private bool gun;
    private bool fire = false;

    float dissapearTime;
    public float laserDuration;
    public float coolDown;
    float timeStamp;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dissapearTime > 0)
        {
            dissapearTime -= Time.deltaTime;
            line1.enabled = true;
        }

        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        else line1.enabled = false;

        if(gun)
        {
            line1.SetPosition(0, firePoint1.position);
        }
        else
        {
            line1.SetPosition(0, firePoint2.position);
        }

    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile" && dissapearTime <= 0 && timeStamp <= 0)
        {
            shoot(collision.gameObject);
        }
    }

    void shoot(GameObject projectile)
    {
        Vector3 pom = projectile.transform.position;

        if(gun)
        {
            line1.SetPosition(1, pom);
            gun = false;  
        }
        else
        {
            line1.SetPosition(1, pom);
            gun = true;
        }

        dissapearTime = laserDuration;
        Destroy(projectile);
        timeStamp = coolDown;

        
    }

    public void setFireOn()
    {
        fire = true;
    }

    public void SetFrireOff()
    {
        fire = false;
    }
}
