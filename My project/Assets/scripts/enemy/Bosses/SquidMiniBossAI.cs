using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidMiniBossAI : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float CoolDown;
    [SerializeField] float Delay;
    [SerializeField] int Bursts;
    [SerializeField] int Projectiles;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float ProjectileForce;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] Transform FirePoint;
    int CurrentBurst;
    float timestamp;
    bool flip;
    Rigidbody2D rb;


    void Start()
    {
        flip = false;
        CurrentBurst = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timestamp > 0)
        {
            timestamp -= Time.deltaTime;
        }

        if (timestamp <= 0)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Force);
        KnedlikLib.SetMaxSpeed(maxSpeed, rb);
    }

    public void Shoot()
    {
        float angle = 360 / Projectiles;
        float pom = angle;

        if (flip)
        {
            angle += pom / 2;
        }

        for (int i = 0; i < Projectiles; i++)
        {

            GameObject B = Instantiate(BulletPrefab, FirePoint.position, Quaternion.Euler(0, 0, angle));
            B.GetComponent<foddeBullet>().damage = damage;
            Rigidbody2D RB = B.GetComponent<Rigidbody2D>();
            RB.velocity = B.transform.up * ProjectileForce;

            angle += pom;
        }
        CurrentBurst++;

        if (flip)
        {
            flip = false;
        }
        else
        {
            flip = true;
        }

        if (CurrentBurst == Bursts)
        {
            CurrentBurst = 0;
            float Rand = Random.Range(0f,3f);
            timestamp = CoolDown + Rand;
        }
        else
        {
            timestamp = Delay;
        }
    }
}
