using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBolt : Projectile
{
    public float speed;
    Rigidbody2D rb;
    Transform target;
    List<Transform> targets = new List<Transform>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = new Vector3(0, 0, 0);

        if(KnedlikLib.FindClosestEnemy(transform,out target,targets) == false)
        {
            Destroy(gameObject);
        }

        if(target != null)
        {
             dir = (target.position - transform.position).normalized;
            rb.velocity = dir * speed;
        }

        KnedlikLib.lookAt2d(transform, target.position, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targets.Contains(collision.transform) == false)
        {
            Health health = collision.GetComponent<Health>();
            int pom = health.health;
            health.TakeDamage(damage);
            damage -= pom;

            if(damage < 0)
            {
                Destroy(gameObject);
            }
            targets.Add(collision.transform);
        }
    }
}
