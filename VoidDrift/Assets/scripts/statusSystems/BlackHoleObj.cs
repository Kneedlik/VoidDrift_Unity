using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleObj : MonoBehaviour
{
    public int Damage;
    public float Force;
    public float CoolDown;
    public float DestroyTime;
    [SerializeField] float SizeIncreaseSpeed;
    Dictionary<GameObject,Rigidbody2D> Caught = new Dictionary<GameObject,Rigidbody2D>();
    float TimeStamp;
    float BaseSize;

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
        BaseSize = transform.localScale.x;
        transform.localScale = new Vector3(0.1f, 0.1f, 1);
    }

    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if (TimeStamp <= 0)
        {
            TimeStamp = CoolDown;
            foreach (KeyValuePair<GameObject, Rigidbody2D> item in Caught)
            {
                if (item.Key != null)
                {
                    Health health = Caught[item.Key].GetComponent<Health>();
                    if (health != null)
                    {
                        int DamageTemp = KnedlikLib.ScaleDamage(Damage, true, true);
                        health.TakeDamage(DamageTemp);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(transform.localScale.x < BaseSize)
        {
            transform.localScale = new Vector3(transform.localScale.x + SizeIncreaseSpeed,transform.localScale.y + SizeIncreaseSpeed,1);
        }

        foreach(KeyValuePair<GameObject,Rigidbody2D> item in Caught)
        {
            if (item.Key != null)
            {
                Vector3 dir = (Caught[item.Key].transform.position - transform.position).normalized;
                dir = dir * -1;
                Caught[item.Key].AddForce(dir * Force, ForceMode2D.Force);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            return;
        }

        if(Caught.ContainsKey(collision.gameObject) == false)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                Caught.Add(collision.gameObject, rb );
            }else
            {
                return;
            }
        }
    }
}
