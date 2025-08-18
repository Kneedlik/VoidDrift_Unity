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
    [SerializeField] Color32 BrittleColor;

    public bool explode;
    public int explodeDamage;
    [SerializeField] GameObject explosionPrefab;
    public float explodeRadius;

    public bool freeze;
    public float freezeDuration;
    public bool secondExplosion;
    [SerializeField] GameObject FreezeObject;

    [SerializeField] float ListClearTimer = 3f;
    float Timestamp;
    
    void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Timestamp <= 0)
        {
            for (int i = 0;i < BrittleEnemies.Count;i++)
            {
                if (BrittleEnemies[i] == null)
                {
                    BrittleEnemies.RemoveAt(i);
                }
            }

            Timestamp = ListClearTimer;
        }else
        {
            Timestamp -= Time.deltaTime;
        }
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
                //KnedlikLib.scaleParticleSize(target, P, 1);
                P.transform.SetParent(target.transform);

                StartCoroutine(StartBrittle(target));
                if (explode)
                {
                    GameObject E = Instantiate(explosionPrefab, target.transform.position, Quaternion.Euler(0, 0, 0));
                    explosion exp = E.GetComponent<explosion>();
                    int pom = KnedlikLib.ScaleDamage(explodeDamage, true, true);
                    pom = KnedlikLib.ScaleStatusDamage(pom);

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

    public void RemoveBrittle(GameObject target,Color32 TargetColor,SpriteRenderer TargetSpriteRenderer)
    {
        Health health = target.GetComponent<Health>();
        health.multiplier -= armorPierce;
        simpleAI ai1 = target.GetComponent<simpleAI>();
        EnemyFollow ai2 = target.GetComponent<EnemyFollow>();

        if (ai1 != null)
        {
            ai1.SpeedMultiplier += speedDecrease;
        }
        else if (ai2 != null)
        {
            ai2.Multiplier += speedDecrease;
        }

        foreach (Transform child in target.transform)
        {
            if(child.tag.Contains("Brittle"))
            {
                Destroy(child.gameObject);
            }
        }

        TargetSpriteRenderer.color = TargetColor;
        BrittleEnemies.Remove(target);
    }

    IEnumerator StartBrittle(GameObject target)
    {
        simpleAI ai1 = target.GetComponent<simpleAI>();
        EnemyFollow ai2 = target.GetComponent<EnemyFollow>();
        Health health = target.GetComponent<Health>();
        if(health == null)
        {
            yield break;
        }

        if (speedDecrease > 1)
        {
            speedDecrease = 1;
        }

        if (ai1 != null)
        {
            ai1.SpeedMultiplier -= speedDecrease;
        } else
        {
            if (ai2 != null)
            {
                ai2.Multiplier -= speedDecrease;
            }
        }

        health.multiplier += armorPierce;

        SpriteRenderer S = target.GetComponent<SpriteRenderer>();
        Color32 C;
        C = S.color;
        S.color = BrittleColor;

        yield return new WaitForSeconds(duration);
        if (target != null)
        {
            RemoveBrittle(target,C,S);
        }
    }

    IEnumerator Freeze(GameObject target)
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GameObject F = Instantiate(FreezeObject, target.transform.position, Quaternion.Euler(0, 0, 0));
   
            //KnedlikLib.scaleParticleSize(target, F, 0.9f);
            F.transform.SetParent(target.transform);

            yield return new WaitForSeconds(freezeDuration);

            if (target != null)
            {
                if (secondExplosion)
                {
                    GameObject E = Instantiate(explosionPrefab, target.transform.position, Quaternion.Euler(0, 0, 0));
                    explosion exp = E.GetComponent<explosion>();
                    int pom = KnedlikLib.ScaleDamage(explodeDamage, true, true);
                    pom = KnedlikLib.ScaleStatusDamage(pom);
                    exp.damage = pom;

                    KnedlikLib.ScaleParticleByFloat(E, explodeRadius, true);
                }

                Destroy(F);

                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints2D.None;
                }
            }
        }
    }

    
    

    

}
