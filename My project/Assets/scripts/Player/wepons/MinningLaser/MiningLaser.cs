
using UnityEngine;

public class MiningLaser : MonoBehaviour
{
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public int damage = 5;
    public float fireRate = 10;


    private float nextTimeToFire;


    private void Start()
    {
        nextTimeToFire = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            lineRenderer.enabled = true;
            Shoot();
        }else
        {
            lineRenderer.enabled = false;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            DealDamage();
        }
    }

    void Shoot()
    {
       RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
        if(hitInfo)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.up * 100);

        }
    }

    void DealDamage()
    {
        RaycastHit2D hitInfo1 = Physics2D.Raycast(firePoint.position, firePoint.up);

        if (hitInfo1 && hitInfo1.transform.GetComponent<Health>() != null)
        {
           Health health = hitInfo1.transform.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
        }

       // if(hitInfo1.transform.tag == "Enemy")
       // {
       //     hitInfo1.transform.GetComponent<Health>().TakeDamage(damage);
       // }
    }
}
