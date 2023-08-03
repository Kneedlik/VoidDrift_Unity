using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketLauncher : MonoBehaviour
{
    bool shootCheck;
    public Transform firePoint1;
    public Transform firePoint2;
    public float cooldown;
    public GameObject rocket;
    GameObject rocket1;
    bool currentPoint;
    public float rocketSpeed;
    public int maxAmmo;
    public int currentAmmo;
    plaerHealth health;
    bool healCheck;

    

    float timeStamp;


    void Start()
    {
        shootCheck = false;
        currentAmmo = maxAmmo;
        health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            shootCheck = true;
        }

        if (shootCheck)
        {
            if (timeStamp <= 0)
            {
                shootCheck = false;
                if(currentAmmo > 0)
                {
                    Shoot();
                    timeStamp = cooldown;
                }
                
            }
        }

        

        healCheck = health.getHealCheck();
        if(healCheck)
        {
            currentAmmo = maxAmmo;
        }
    }

    void Shoot()
    {
        if (currentPoint)
        {
            rocket1 = Instantiate(rocket, firePoint1.position,firePoint1.rotation);
            currentPoint = false;
            Rigidbody2D rb = rocket1.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint1.up * rocketSpeed,ForceMode2D.Force);
        }
        else
        {
            rocket1 = Instantiate(rocket, firePoint2.position,firePoint2.rotation);
            currentPoint = true;
            Rigidbody2D rb = rocket1.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint2.up * rocketSpeed, ForceMode2D.Force);
        }
        currentAmmo--;
    }
}
