using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCannon : MonoBehaviour
{
    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] Transform firePoint3;
    Transform target;

    [SerializeField] int damage;
    [SerializeField] float bulletForce;
    [SerializeField] float fireRate;
    [SerializeField] float burstDelay;
    [SerializeField] float rotSpeed;
    [SerializeField] float range;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int BulletsInBurst;
    int index;
    float timeStamp;
    Rigidbody2D rb2;
   // Rigidbody2D rb3;

    

    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb2 = target.GetComponent<Rigidbody2D>();
       // rb3 = gameObject.GetComponent<Rigidbody2D>();
        index = 0;
    }

    
    void Update()
    {
        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(Vector3.Distance(target.position,transform.position) < range)
        {
            if (InterceptionPoint(target.position, firePoint3.position, rb2.velocity, bulletForce, out var direction))
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
               // rb3.rotation = angle;
               transform.rotation = Quaternion.Euler(0,0,angle);
               // rb.rotation = angle;
               // rb.velocity = direction * bulletForce;
            }else
            {
                transform.LookAt(target);
            }

            if (timeStamp <= 0)
            {
                shoot();
            }
        }

       
    }

    void shoot()
    {
      GameObject bulet =  Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
      Rigidbody2D rb = bulet.GetComponent<Rigidbody2D>();
      foddeBullet ee = bulet.GetComponent<foddeBullet>();
        ee.damage = damage;
        rb.velocity = firePoint1.up * bulletForce;

        bulet = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        rb = bulet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint1.up * bulletForce;
        ee = bulet.GetComponent<foddeBullet>();
        ee.damage = damage;

        index++;
        if(index >= BulletsInBurst)
        {
            index = 0;
            timeStamp = burstDelay;
        }else
        {
            timeStamp = fireRate;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
       
    }
}
