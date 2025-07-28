using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCanon : MonoBehaviour
{
    bool ShootCheck;
    Transform target;
    GameObject BulletPrefab;

   [SerializeField] Transform pos1;
   [SerializeField] Transform pos2;

    public float FireRate;
    float timeStamp;

    public int Damage;
    public int Force;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            ShootCheck = true;
        }else
        {
            ShootCheck = false;
        }

        if(timeStamp > 0 )
        {
            timeStamp -= Time.deltaTime;
        }else
        {
            if(KnedlikLib.FindClosestEnemy(transform,out target))
            {
                Shoot();
                timeStamp = FireRate;
            }
            
        }
    }

    public void Shoot()
    {
        GameObject bullet1 = Instantiate(BulletPrefab, pos1);
        GameObject bullet2 = Instantiate(BulletPrefab, pos2);
        
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2  = bullet2.GetComponent<Rigidbody2D>();

        Rigidbody2D rbEnem = target.GetComponent<Rigidbody2D>();
        if (KnedlikLib.InterceptionPoint(target.position,pos1.position,rbEnem.velocity,Force,out var direction1,out float Angle1))
        {
            rb1.rotation = Angle1;
            rb1.velocity = direction1 * Force;
        }else
        {
            rb1.velocity = (target.position - pos1.position).normalized * Force;
        }

        if (KnedlikLib.InterceptionPoint(target.position, pos2.position, rbEnem.velocity, Force, out var direction2, out float Angle2))
        {
            Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
            rb2.rotation = Angle2;
            rb2.velocity = direction2 * Force;
        }else
        {
            rb2.velocity = (target.position - pos1.position).normalized * Force;
        }

    }
}
