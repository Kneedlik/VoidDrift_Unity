using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGUn : MonoBehaviour
{
    public Transform firePoint;
    
    public float range;
    public float spread;
    public int pellets;
    GameObject[] E;
    public LineRenderer line;
    public GameObject cube;
     LineRenderer[] lines;

    public float shotDuration;
    float dissapearTime;

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
        dissapearTime = 0;

        float offset = spread / (pellets -1);
        float start = (spread / 2) * -1;
        Vector2 firePoints;
        
        E = new GameObject[pellets];
        lines = new LineRenderer[pellets];
       // lines = new LineRenderer[pellets];

        for (int i = 0; i < pellets; i++)
        {
            //lines[i] = line;

            firePoints.y = start + firePoint.position.y;
            firePoints.x = 0;

            float distance = Vector2.Distance(firePoint.position, firePoints);
            

            while(distance < range)
            {
                firePoints.x += 0.1f;
                distance = Vector2.Distance(firePoint.position, firePoints);
            }

            E[i] = Instantiate(cube);
            E[i].transform.position = firePoints;
            E[i].transform.rotation = Quaternion.identity;
            E[i].transform.parent = gameObject.transform;

            lines[i] = Instantiate(line);

            start += offset;
        }
        currentAmmo = magSize;
    }

    void Update()
    {
        
        if(currentAmmo <= 0)
        {
            shootCheck = false;
            reloadTimeStamp += Time.deltaTime;
            if(reloadTimeStamp >= reloadSpeed)
            {
                currentAmmo = magSize;
                reloadTimeStamp = 0;
            }
        }

        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (dissapearTime > 0)
        {
            dissapearTime -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            shootCheck = true;
        }

        if (shootCheck && timeStamp <= 0 && currentAmmo > 0) {
            for (int i = 0; i < pellets; i++){
                lines[i].enabled = true;
            }
            dealDamage();
            timeStamp = coolDown;
            dissapearTime = shotDuration;
            currentAmmo--;
            shootCheck = false;
        }

        if (dissapearTime <= 0)
         {
         for (int i = 0; i < pellets; i++) {
         lines[i].enabled = false;
         }
        }else
        {
         Shoot();
        }
    }

    void Shoot()
    {
        for (int i = 0; i < pellets; i++)
        {
            Vector2 rayCastDir = E[i].transform.position - firePoint.position;
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, rayCastDir);

            if (hitInfo && Vector2.Distance(firePoint.position, hitInfo.point) < Vector2.Distance(firePoint.position, E[i].transform.position))
            {
                lines[i].SetPosition(0, firePoint.position);
                lines[i].SetPosition(1, hitInfo.point);
            }
            else
            {
                lines[i].SetPosition(0, firePoint.position);
                lines[i].SetPosition(1, E[i].transform.position);
            }
        }
        
    }

    void dealDamage()
    {
        for (int i = 0; i < pellets; i++)
        {
            Vector2 rayCastDir = E[i].transform.position - firePoint.position;
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, rayCastDir);

            if (hitInfo && Vector2.Distance(firePoint.position, hitInfo.point) < Vector2.Distance(firePoint.position, E[i].transform.position) && hitInfo.transform.GetComponent<Health>() != null)
            {
                Health health = hitInfo.transform.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(pelletDamage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
