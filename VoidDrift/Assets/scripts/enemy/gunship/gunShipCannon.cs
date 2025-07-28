using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShipCannon : MonoBehaviour
{
    protected Transform target;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float fireRate;
    public float timeBetweenBursts;
    [SerializeField] int damage;
    [SerializeField] gunShipAI Ship;

    public int bulletsInABurst = 2;
    int currentBurst;

    bool fire;
    bool gun;

    float timer;

    public Transform firePoint1;
    public Transform firePoint2;

    Rigidbody2D rb2;



     void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb2 = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Ship.Ready == false)
        {
            return;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (fire && timer <= 0)
        {
            StartCoroutine(Shoot());
            currentBurst++;

            if (currentBurst >= bulletsInABurst)
            {
                currentBurst = 0;
            }

            timer = timeBetweenBursts;

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

    IEnumerator Shoot()
    {
        for (int i = 0; i < bulletsInABurst; i++)
        {
           
            
            if (gun)
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
                foddeBullet ee = bullet.GetComponent<foddeBullet>();
                ee.damage = damage;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                gun = false;

                if (InterceptionPoint(target.position, firePoint1.position, rb2.velocity, bulletForce, out var direction))
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                    rb.rotation = angle;
                    rb.velocity = direction * bulletForce;

                }
                else
                {
                    rb.velocity = (target.transform.position - firePoint1.position).normalized * bulletForce;

                }
            }
            else
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
                foddeBullet ee = bullet.GetComponent<foddeBullet>();
                ee.damage = damage;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                gun = true;

                if (InterceptionPoint(target.position, firePoint2.position, rb2.velocity, bulletForce, out var direction))
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                    rb.rotation = angle;
                    rb.velocity = direction * bulletForce;

                }
                else
                {
                    rb.velocity = (target.transform.position - firePoint2.position).normalized * bulletForce;

                }
            }

            yield return new WaitForSeconds(fireRate);
        }

            

       
    }

    public class MyMath
    {
        public static int solveQuadratic(float a, float b, float c, out float x1, out float x2)
        {
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                x1 = Mathf.Infinity;
                x2 = -x1;
                return 0;
            }
            x1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            x2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
            return discriminant > 0 ? 2 : 1;

        }
    }

    public bool InterceptionPoint(Vector2 a, Vector2 b, Vector2 va, float speedB, out Vector2 result)
    {
        var aToB = b - a;
        var dc = aToB.magnitude;
        var alpha = Vector2.Angle(aToB, va) * Mathf.Deg2Rad;
        var speedA = va.magnitude;
        var r = speedA / speedB;
        if (MyMath.solveQuadratic(1 - r * r, 2 * r * dc * Mathf.Cos(alpha), -(dc * dc), out var x1, out var x2) == 0)
        {
            result = Vector2.zero;
            return false;
        }
        var da = Mathf.Max(x1, x2);
        var t = da / speedB;
        Vector2 c = a + va * t;

        result = (c - b).normalized;
        return true;
    }
}
