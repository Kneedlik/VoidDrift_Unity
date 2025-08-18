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
    [SerializeField] bool OnCrit;
    [SerializeField] bool OnImpactGunOnly;
    [SerializeField] bool OnImpactSummon;
    public bool Stun;
    public Color32 Colour = new Color32(255,255,255,255);

    public List<GameObject> IgnoreTargets = new List<GameObject>();

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
                        if (force > 0)
                        {
                            if (Stun)
                            {
                                KnedlikLib.TryStun(collision.gameObject);
                            }
                            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                            //Debug.Log(dir);
                            //Debug.Log(force);
                            rb.velocity = rb.velocity.normalized;
                            Tenacity tenacity = collision.GetComponent<Tenacity>();
                            float knockBackTemp = force;
                            if (tenacity != null)
                            {
                                knockBackTemp = tenacity.CalculateForce(knockBackTemp);
                            }

                            rb.AddForce(dir * knockBackTemp, ForceMode2D.Impulse);
                        }
                    }
                }
            }

            if (function != null)
            {
                function(collision.gameObject, damage, ref damage);
            }

            Health health = collision.GetComponent<Health>();

            if (isEnemy)
            {
                if (PlayerDamage > 0)
                {
                    plaerHealth pHealth = collision.GetComponent<plaerHealth>();
                    if (pHealth != null)
                    {
                        pHealth.TakeDamage(PlayerDamage);
                    }
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
                    health.TakeDamage(pom,Colour);
                }
            }
            else
            {
                plaerHealth pHealth = collision.GetComponent<plaerHealth>();
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

                    if (eventManager.ImpactGunOnly != null && OnImpactGunOnly)
                    {
                        eventManager.ImpactGunOnly(collision.gameObject, gameObject);
                    }

                    if (Impact && eventManager.OnImpact != null)
                    {
                        eventManager.OnImpact(collision.gameObject, damage, ref damagePlus);
                    }

                    if(OnImpactSummon && eventManager.SummonOnImpact != null)
                    {
                        eventManager.SummonOnImpact(collision.gameObject, damage, ref damagePlus);
                    }

                    if (eventManager.OnCrit != null && OnCrit)
                    {
                        Color32 TempColor = eventManager.OnCrit(collision.gameObject, damagePlus, ref damagePlus);
                        Color32 BaseColor = new Color32(0, 0, 0, 0);
                        if (!TempColor.Equals(BaseColor))
                        {
                            Colour = TempColor;
                        }
                    }

                    if (PostImpact && eventManager.PostImpact != null)
                    {
                        eventManager.PostImpact(collision.gameObject, damagePlus, ref damagePlus);
                    }

                    health.TakeDamage(damagePlus, Colour);
                    Colour = new Color32(255, 255, 255, 255);
                }
            }
        }
    }
}
