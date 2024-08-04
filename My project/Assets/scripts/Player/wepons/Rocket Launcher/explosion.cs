using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class explosion : MonoBehaviour
{
    public delegate void specialFunction(GameObject target, int damage, ref int scaledDamage);
    public specialFunction function;

    public int damage;
    public int PlayerDamage;
    public float TrueDamage;
    public float destroyTime;
    public float force;
    public bool isEnemy = false;
    public bool LevelScaling;

    [SerializeField] bool ScaleDmg;
    [SerializeField] bool ScaleArea;
    [SerializeField] bool Impact;
    [SerializeField] bool PostImpact;
    public bool Stun;

    
    void Start()
    {
      //  if (destroyTime != 0)
      //  {
            Destroy(gameObject, destroyTime);
      //  }

        if(!isEnemy)
        {
            if(ScaleDmg)
            {
                KnedlikLib.ScaleDamage(damage, true, true);
            }

            if(ScaleArea)
            {
                KnedlikLib.ScaleParticleByFloat(gameObject,1,true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            Vector3 dir = (collision.transform.position - transform.position).normalized;

            if (force > 0)
            {
                if (collision.isTrigger == false)
                {
                    if (isEnemy || collision.tag != "Player")
                    {
                        if (Stun)
                        {
                            KnedlikLib.TryStun(collision.gameObject);
                        }
                        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                        Debug.Log(dir);
                        Debug.Log(force);
                        rb.velocity = rb.velocity.normalized;
                        rb.AddForce(dir * force, ForceMode2D.Impulse);
                    }
                }
            }

            if (function != null)
            {
                function(collision.gameObject, damage, ref damage);
            }

            Health health = collision.GetComponent<Health>();
            plaerHealth pHealth = collision.GetComponent<plaerHealth>();

            if (isEnemy)
            {
                if (pHealth != null)
                {
                    pHealth.TakeDamage(PlayerDamage);
                }

                if (health != null)
                {
                    int pom = damage;
                    if (LevelScaling)
                    {
                        pom = KnedlikLib.ScaleByLevel(damage);
                    }

                    if (TrueDamage > 0)
                    {
                        float pom2 = KnedlikLib.GetPercencHP(collision.gameObject, TrueDamage);
                        pom += (int)pom2;
                    }
                    health.TakeDamage(pom);
                }
            }
            else
            {
                if (pHealth != null)
                {
                    if (PlayerDamage > 0)
                    {
                        pHealth.TakeDamage(PlayerDamage);
                    }
                }
                if (health != null)
                {
                    int damagePlus = damage;
                    if (Impact && eventManager.OnImpact != null)
                    {
                        eventManager.OnImpact(collision.gameObject, damage, ref damagePlus);
                    }

                    if (PostImpact && eventManager.PostImpact != null)
                    {
                        eventManager.PostImpact(collision.gameObject, damagePlus, ref damagePlus);
                    }
                    health.TakeDamage(damagePlus);
                }
            }
        }
    }
}
