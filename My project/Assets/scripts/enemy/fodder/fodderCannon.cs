using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class fodderCannon : MonoBehaviour
{
    Transform target;
    public GameObject bulletPrefab;
    public float bulletForce;
    public int bulletsInABurst;
    public float rapidFireDelay;
    public bool fire;
    public float coolDown;
    float timer;
    public Transform firePoint;
    Rigidbody2D rb2;

    public int numberOfBursts;
    public int currentBurst;
    public float timeBetweenBursts;
    public bool salvoFinished;

    [SerializeField] int damage;

    

    bool ready;
   
   
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb2 = target.GetComponent<Rigidbody2D>();
        fire = false;
        timer = coolDown;
        currentBurst = 0;
        salvoFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (salvoFinished && timer <= 0)
        {
            salvoFinished = false;
        }

        if (fire && timer <= 0)
        {
            ready = true;
            StartCoroutine(Shoot());
            currentBurst++;

            if (currentBurst >= numberOfBursts)
            {
                timer = timeBetweenBursts;
                currentBurst = 0;
                salvoFinished = true;
            }
            else
                timer = coolDown;
        }
        else ready = false;

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            fire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            fire = false;
        }
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < bulletsInABurst; i++)
        {
            AudioManager.instance.PlayId(7);
            GameObject bullet;
            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            foddeBullet ee = bullet.GetComponent<foddeBullet>();
            ee.damage = damage;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (InterceptionPoint(target.position, firePoint.position, rb2.velocity, bulletForce, out var direction))
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                rb.rotation = angle;
                rb.velocity = direction * bulletForce; 
                
            }
            else
            {
                rb.velocity = (target.transform.position - firePoint.position).normalized * bulletForce;  
            }
            
            //Vector2 dir = (target.position - firePoint.position).normalized;
            //rb.AddForce(dir * bulletForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(rapidFireDelay);
        }
    }

    public class MyMath
    {
        public static int solveQuadratic(float a,float b, float c,out float x1,out float x2)
        {
            var discriminant = b * b - 4 *a * c;
            if(discriminant < 0)
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

    public bool InterceptionPoint(Vector2 a,Vector2 b,Vector2 va,float speedB, out Vector2 result)
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
        var da = Mathf.Max(x1,x2);
        var t = da / speedB;
        Vector2 c = a + va * t;

        result = (c - b).normalized;
        return true;
    }

    public bool isReady()
    {
        if(ready)
        {
            return true;
        }else return false;

    }
}
