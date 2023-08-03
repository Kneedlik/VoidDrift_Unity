using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileShotGun : MonoBehaviour
{
    public Transform firePoint;

    public float range;
    public float spread;
    public int pellets;
    GameObject[] E;
    
    public GameObject cube;
    public GameObject pellet;
    GameObject pellet1;
    public float bulletSpeed;
    public int pelletDamage;

    bool shootCheck = false;
    public float coolDown;
    float timeStamp;

    public int magSize;
    public float reloadSpeed;
    public int currentAmmo;
    float reloadTimeStamp;


    void Awake()
    {
        timeStamp = 0;
        ;

        float offset = spread / (pellets - 1);
        float start = (spread / 2) * -1;
        Vector2 firePoints;

        E = new GameObject[pellets];
        
        // lines = new LineRenderer[pellets];

        for (int i = 0; i < pellets; i++)
        {
            //lines[i] = line;

            firePoints.y = start + firePoint.position.y;
            firePoints.x = 0;

            float distance = Vector2.Distance(firePoint.position, firePoints);
            

            while (distance < range)
            {
                firePoints.x += 0.1f;
                distance = Vector2.Distance(firePoint.position, firePoints);
            }

            E[i] = Instantiate(cube);
            E[i].transform.position = firePoints;
            E[i].transform.rotation = Quaternion.identity;
            E[i].transform.parent = gameObject.transform;

            

            start += offset;
        }
        currentAmmo = magSize;
    }

    void Update()
    {

        if (currentAmmo <= 0)
        {
            reloadTimeStamp += Time.deltaTime;
            if (reloadTimeStamp >= reloadSpeed)
            {
                currentAmmo = magSize;
                reloadTimeStamp = 0;
            }
        }

        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Fire1"))
        {
            shootCheck = true;
        }

        if (shootCheck && timeStamp <= 0 && currentAmmo > 0)
        {
            Shoot();
            shootCheck = false;
            timeStamp = coolDown;
            currentAmmo--;
        }

        
    }

    void Shoot()
    {
        for (int i = 0; i < pellets; i++)
        {
            Vector2 dir = (E[i].transform.position - firePoint.position).normalized;

            pellet1 = Instantiate(pellet,transform.position,Quaternion.identity);
            pellet1.transform.position = firePoint.position;
            Rigidbody2D rb = pellet1.GetComponent<Rigidbody2D>(); 
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
            rb.AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
