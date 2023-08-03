using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwawe : MonoBehaviour
{
    public float destroyTime;
    public float force;
    public int damage;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 dir = (collision.transform.position - transform.position).normalized;
        if (collision.transform.tag.Contains("Enemy") && collision.isTrigger == false)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(dir * force, ForceMode2D.Impulse);
            Health health = collision.GetComponent<Health>();
            health.TakeDamage(damage);
        }
    }

    
}
