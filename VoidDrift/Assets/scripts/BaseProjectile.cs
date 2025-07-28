using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static eventManager;
using static UnityEngine.GraphicsBuffer;

public class BaseProjectile : MonoBehaviour
{
    public bool IsEnem;
    public int Damage;
    [SerializeField] bool Impact;
    [SerializeField] bool PostImpact;
    [SerializeField] bool ScaleDamage;
    [SerializeField] bool ScaleArea;
    public int bounce = 0;
    public int pierce = 0;

    public GameObject impactParticles;
    public GameObject impactEffect;

    public float destroyTime;


    void Start()
    {
        Destroy(gameObject,destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
        {

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rbTarget = collision.GetComponent<Rigidbody2D>();

            if (!IsEnem)
            {
                if (collision.tag != "Player")
                {
                    int DamageTemp = Damage;
                    if (ScaleDamage)
                    {
                        KnedlikLib.ScaleDamage(DamageTemp, true, true);
                    }

                    if (ScaleArea)
                    {
                        KnedlikLib.ScaleParticleByFloat(gameObject, 1, true);
                    }

                    int DamagePlus = DamageTemp;

                    if (Impact && eventManager.OnImpact != null)
                    {
                        eventManager.OnImpact(collision.gameObject, DamageTemp, ref DamagePlus);
                    }

                    if (PostImpact && eventManager.PostImpact != null)
                    {
                        eventManager.PostImpact(collision.gameObject, DamageTemp, ref DamagePlus);
                    }

                    Health health = collision.GetComponent<Health>();
                    health.TakeDamage(DamagePlus);

                    if (pierce <= 0)
                    {
                        if (bounce > 0)
                        {
                            bounce = bounce - 1;
                            Vector2 direction;
                            Transform target;

                            List<Transform> transforms = new List<Transform>();
                            transforms.Add(collision.transform);
                            if (KnedlikLib.FindClosestEnemy(gameObject.transform, out target, transforms))
                            {
                                rbTarget = target.GetComponent<Rigidbody2D>();

                                if (KnedlikLib.InterceptionPoint(target.position, transform.position, rbTarget.velocity, rb.velocity.magnitude, out direction))
                                {
                                    // rb.
                                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                                    rb.rotation = angle;
                                    rb.velocity = direction * rb.velocity.magnitude;
                                }
                                else
                                {
                                    float rand1 = Random.Range(0, 1);
                                    float rand2 = Random.Range(0, 1);
                                    direction = new Vector2(rand1, rand2);
                                    rb.velocity = direction * rb.velocity.magnitude;
                                }
                            }
                            else
                            {
                                float rand1 = Random.Range(0, 1);
                                float rand2 = Random.Range(0, 1);
                                direction = new Vector2(rand1, rand2);
                                rb.velocity = direction * rb.velocity.magnitude;
                            }
                        }
                        else
                        {
                            if (impactEffect != null)
                            {
                                Instantiate(impactEffect, transform.position, transform.rotation);
                            }

                            if (impactParticles != null)
                            {
                                Instantiate(impactParticles, transform.position, Quaternion.Euler(-90, 0, 0));
                            }
                            Destroy(gameObject);
                        }
                    }


                }

            }
            else
            {
                if (collision.tag == "Player")
                {
                    plaerHealth health = collision.GetComponent<plaerHealth>();
                    if (health != null)
                    {
                        health.TakeDamage(Damage);
                    }
                }
                else if (collision.tag != "Enemy")
                {
                    Health health = collision.GetComponent<Health>();
                    if (health != null)
                    {
                        health.TakeDamage(Damage);
                    }
                }

                if (impactEffect != null)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }

                if (impactParticles != null)
                {
                    Instantiate(impactParticles, transform.position, Quaternion.Euler(-90, 0, 0));
                }

                Destroy(gameObject);
            }
        }
    }

    

}
