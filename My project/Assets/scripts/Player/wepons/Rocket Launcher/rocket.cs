using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : BulletScript
{
    public int ImpactDamage;
    public float rocketSpeed;
    public GameObject explosion;
    float currentSpeed;
    public float speedGrowth;
    public float maxSpeed;
    [SerializeField] float Delay;
    public int AditionalExloAmount;
    public float AditionalExploDelay;
    [SerializeField] bool RotateParticle;
    [SerializeField] Vector3 ParticleRotation;
    bool Dead;
    Vector3 DeadPos;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        damage = damagePlus;
        if(destroyTime != 0)
        {
            Destroy(gameObject, destroyTime);
        }
        currentSpeed = 0;
    }


    private void FixedUpdate()
    {
        if(currentSpeed < rocketSpeed)
        {
            currentSpeed += speedGrowth;
        }

        rb.AddForce(transform.up * currentSpeed,ForceMode2D.Force);
        KnedlikLib.SetMaxSpeed(maxSpeed, rb);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player" && collision.isTrigger == false && collision.GetComponent<Health>() != null && Dead == false)
        {
            if(ImpactDamage > 0)
            {
                DealDamageToEmemy(collision, ImpactDamage);
            }

            Dead = true;
            DeadPos = collision.transform.position;
            //DealDamageToEmemy(collision);
            KnedlikLib.PlayDead(gameObject);
            if (Delay == 0)
            {
                Explode();
                if(RotateParticle)
                {
                    SpawnEffects(new Vector3(), true, ParticleRotation);
                }else SpawnEffects();
            }
            else
            {
                Invoke("Explode", Delay);
                Invoke("SpawnEffects", Delay);
            }

            float TempDelay = AditionalExploDelay * 2;
            for (int i = 0;i < AditionalExploDelay;i++)
            {
                Invoke("Explode", TempDelay);
                TempDelay += AditionalExploDelay;
            }
        } 
    }

    void Explode()
    {
        if (explosion != null)
        {
            explosion Explode = Instantiate(explosion, DeadPos, transform.rotation).GetComponent<explosion>();
            if (Explode != null)
            {
                Explode.damage = damagePlus;
            }
        }

        if (AditionalExploDelay <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float Multiplier)
    {
        maxSpeed = maxSpeed * Multiplier;
        speedGrowth = speedGrowth * Multiplier;
        rocketSpeed = rocketSpeed * Multiplier;
    }
}
