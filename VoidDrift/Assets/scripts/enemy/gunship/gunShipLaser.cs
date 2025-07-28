using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShipLaser : MonoBehaviour
{
    private Transform target;
    public Transform firePoint1;
    public Transform firePoint2;
    public LineRenderer line;
    private float nextTimeToFire;
    public float FireRate;
    public int Damage;

    private bool gun;
    private bool fire = false;

    //lock on
    private bool lockOn;
    private float lockingOn;
    public float LockOnTime;

    //laser disapear
    float dissapearTime;
    public float laserDuration;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        nextTimeToFire = 0f;
        dissapearTime = 0f;
        lockingOn = LockOnTime;
       
    }

    void Update()
    {
        if (fire)
        {
            lockingOn -= Time.deltaTime;

            if (lockingOn <= 0)
            {
                lockingOn = 0;
                lockOn = true;
            }
            else lockOn = false;
        }
        else if (lockingOn < LockOnTime)
        {
            lockingOn += Time.deltaTime;
        } 

        if ( lockOn && Time.time >= nextTimeToFire && fire )
        {
            line.enabled = true;
           // shoot();
           if(gun)
            {
                gun = false;
            }else
            {
                gun = true;
            }

            dealDamage();
            nextTimeToFire = Time.time + 1f / FireRate;
            dissapearTime = laserDuration;
            
        }else
        { 
            if(dissapearTime <= 0)
            {
                line.enabled = false;
            }
            else
            {
                shoot();
            }
             
        }

            if (dissapearTime > 0)
            {
                dissapearTime -= Time.deltaTime;
            }    
    }

    void shoot()
    {
        if (gun)
        {
            line.SetPosition(0, firePoint1.position);
            line.SetPosition(1, target.position);
           // gun = false;
        }else
        {
            line.SetPosition(0, firePoint2.position);
            line.SetPosition(1, target.position);
          //  gun=true;
        }

    }

    void dealDamage()
    {
        plaerHealth health = target.GetComponent<plaerHealth>();

        if(health != null)
        {
            health.TakeDamage(Damage);
        }

    }

   public void setFireOn()
    {
        fire = true;
    }

  public  void SetFrireOff()
    {
        fire = false;
    }
}
