using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDroneAura : MonoBehaviour
{
    [SerializeField] int Damage;
    [SerializeField] float CoolDown;
    [SerializeField] float PrepTime;
    [SerializeField] float AttackDelay;
    [SerializeField] float AttackDuration;
    [SerializeField] GameObject LightningPrefab;
    [SerializeField] float LightningCoolDown;
    float LightningRadius;
    float TimeStampLightning;
    float TimeStamp;
    float TimeStampAtack;
    SpriteRenderer spriteRenderer;

    bool Preparing;
    bool Attacking;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LightningRadius = GetComponent<CircleCollider2D>().radius;
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if(TimeStampAtack > 0)
        {
            TimeStampAtack -= Time.deltaTime;
        }

        if(TimeStampLightning > 0)
        {
            TimeStampLightning -= Time.deltaTime;
        }

        if(TimeStamp <= 0)
        {
            if (Attacking)
            {
                Attacking = false;
                TimeStamp = CoolDown;
                spriteRenderer.enabled = false;
            }
            else if (Preparing)
            {
                Attacking = true;
                Preparing = false;
                TimeStamp = AttackDuration;
            }
        }

        if(Attacking)
        {
            if(TimeStampLightning >= 0)
            {
                
                LightningBolt Bolt = Instantiate(LightningPrefab, transform.position, transform.rotation).GetComponent<LightningBolt>();
                if (Bolt != null)
                {
                    Bolt.StartObject.transform.position = transform.position;
                    Vector3 PosTemp = Random.insideUnitCircle.normalized;
                    Bolt.EndObject.transform.position = (PosTemp * LightningRadius) + transform.position;
                    Bolt.transform.SetParent(transform.parent);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag != "Player")
        {
            return; 
        }

        if(Attacking)
        {
            if(TimeStampAtack <= 0)
            {
                plaerHealth health = collision.GetComponent<plaerHealth>();
                if(health != null)
                {
                    health.TakeDamage(Damage);
                }
                TimeStampAtack = AttackDelay;
            }
        }else if(Preparing == false)
        {
            if (TimeStamp <= 0)
            {
                Preparing = true;
                spriteRenderer.enabled = true;
                TimeStamp = PrepTime;
            }
        }
    }
}
