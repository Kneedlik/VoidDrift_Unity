using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockObject : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LightningSystem.instance.Shock(collision.gameObject));
        Health health = collision.GetComponent<Health>();
        health.TakeDamage(damage);
    }
}
