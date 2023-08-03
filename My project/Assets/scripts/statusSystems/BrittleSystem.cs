using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrittleSystem : MonoBehaviour
{
    public static BrittleSystem Instance;
    public float armorPierce;
    public float chance;
    public float duration;
    public float speedDecrease;
    public List<GameObject> BrittleEnemies = new List<GameObject>();
    [SerializeField] GameObject particles;

    public bool explode;
    public int explodeDamage;
    [SerializeField] GameObject explosionPrefab;
    public float explodeRadius;

   public bool freeze;
   public float freezeDuration;
    public bool secondExplosion;
    [SerializeField] GameObject FreezeObject;
    
    void Start()
    {
        Instance = this;
    }

    public void ApplyBrittle(GameObject target, int damage, ref int plusDamage)
    {
        float rand = Random.Range(0, 100);
        if (chance >= rand)
        {
            if (BrittleEnemies.Contains(target) == false)
            {
                BrittleEnemies.Add(target);

                GameObject P = Instantiate(particles, target.transform.position, Quaternion.Euler(0, 0, 0));
                KnedlikLib.scaleParticleSize(target, P, 1);
                P.transform.SetParent(target.transform);

                StartCoroutine(StartBrittle(target));
                if (explode)
                {
                   GameObject E = Instantiate(explosionPrefab, target.transform.position, Quaternion.Euler(0, 0, 0));
                    explosion exp = E.GetComponent<explosion>();
                    int pom = KnedlikLib.ScaleDamage(explodeDamage, true, true);

                    exp.damage = pom;

                    KnedlikLib.ScaleParticleByFloat(E, explodeRadius, true);

                }

                if(freeze)
                {
                    StartCoroutine(Freeze(target));
                }
            }
        }
    }

    public void RemoveBrittle(GameObject target)
    {
        float Speed = 0;
        Health health = target.GetComponent<Health>();
        health.multiplier -= armorPierce;
        simpleAI ai1 = target.GetComponent<simpleAI>();
        EnemyFollow ai2 = target.GetComponent<EnemyFollow>();

        if (ai1 != null)
        {
            Speed = ai1.speed;
            ai1.speed = ai1.speed / (1 - speedDecrease);
        }
        else
        {
            if (ai2 != null)
            {
                Speed = ai2.speed;
                ai2.speed = ai2.speed / (1 - speedDecrease);
            }
        }

        foreach(Transform child in target.transform)
        {
            if(child.tag.Contains("Brittle"))
            {
                Destroy(child.gameObject);
            }
        }

        BrittleEnemies.Remove(target);
    }

    IEnumerator StartBrittle(GameObject target)
    {
        float Speed = 0;
        float multiplier;
        simpleAI ai1 = target.GetComponent<simpleAI>();
        EnemyFollow ai2 = target.GetComponent<EnemyFollow>();
        Health health = target.GetComponent<Health>();

        if (speedDecrease > 1)
        {
            speedDecrease = 1;
        }

        if (ai1 != null)
        {
            Speed = ai1.speed;
            ai1.speed = ai1.speed * (1 - speedDecrease);
        } else
        {
            if (ai2 != null)
            {
                Speed = ai2.speed;
                ai2.speed = ai2.speed * (1 - speedDecrease);
            }
        }

        multiplier = health.multiplier;
        health.multiplier += armorPierce;

        yield return new WaitForSeconds(duration);
        if (target != null)
        {

            if (ai1 != null)
            {
                ai1.speed = Speed;
            }
            else if (ai2 != null)
            {
                ai2.speed = Speed;
            }

            health.multiplier = multiplier;
            RemoveBrittle(target);
        }
    }

    IEnumerator Freeze(GameObject target)
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
       GameObject F = Instantiate(FreezeObject, target.transform.position, Quaternion.Euler(0, 0, 0));
        KnedlikLib.scaleParticleSize(target, F, 0.9f);
        F.transform.SetParent(target.transform);

        yield return new WaitForSeconds(freezeDuration);

        if(secondExplosion)
        {
            GameObject E = Instantiate(explosionPrefab, target.transform.position, Quaternion.Euler(0, 0, 0));
            explosion exp = E.GetComponent<explosion>();
            int pom = KnedlikLib.ScaleDamage(explodeDamage, true, true);
        }

        rb.constraints = RigidbodyConstraints2D.None;
    }

    
    

    

}
