using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class poisonSystem : MonoBehaviour
{
    public static poisonSystem sharedInstance;
    public int damage;
    public float chance;
    public float duration;

    public bool maxHealthDMG;
    public float maxHealthDMGamount;

    public bool infect;
    public float distance;

    public float speed;

    [SerializeField] Color32 color;
    [SerializeField] GameObject particles;

    public List<GameObject> poisonedEnemies = new List<GameObject>();

    //Corrupted
    public int CDamage;
    public float CTrueDamage;
    public float CDuration;
    public float CInfectDistance;
    public float CSpeed;
    [SerializeField] GameObject CParticles;
    public List<GameObject> CpoisonedEnemies = new List<GameObject>();

    //Black Poison
    public int BDamage;
    public float BTrueDamage;
    public float BDuration;
    public float BSpeed;
    [SerializeField] GameObject BParticles;
    public List<GameObject> BPoisonedEnemies = new List<GameObject>();

    void Awake()
    {
        sharedInstance = this;
    }

    public void CorruptedPoison(GameObject target,int damage,ref int plusdamage)
    {
        if(CpoisonedEnemies.Contains(target) == false && target.tag != "Player")
        {
            Health health = target.GetComponent<Health>();
            if(health != null) 
            {
                CpoisonedEnemies.Add(target);

                GameObject P = Instantiate(CParticles, target.transform.position, Quaternion.Euler(0, 0, 0));
                //KnedlikLib.scaleParticleSize(target, P, 1f);
                P.transform.SetParent(target.transform);
                StartCoroutine(CStartPoison(health, target));
                StartCoroutine(stopPoison(target,P,true,false));
            }
            
        }
    }

    IEnumerator CStartPoison(Health health,GameObject target)
    {
        yield return new WaitForSeconds(CSpeed * PlayerStats.sharedInstance.TickRate);
        int Damage = KnedlikLib.ScaleDamage(CDamage, true, true);
        Damage = KnedlikLib.ScaleStatusDamage(Damage);

        while (health != null && CpoisonedEnemies.Contains(target))
        {
            if (target.activeInHierarchy)
            {
                int extra = 0;

                float pomH = health.maxHealth * CTrueDamage;
                extra = (int)pomH;

                Color32 C = Constants.CorruptedColor;
                health.TakeDamage(Damage + extra,C);

                GameObject pom = findWithinDistance(target,CInfectDistance);
                if (pom != null)
                {
                    int z = 0;
                    CorruptedPoison(pom, 0, ref z);
                }
                
                yield return new WaitForSeconds(CSpeed * PlayerStats.sharedInstance.TickRate);
            }
            else
            {
                CpoisonedEnemies.Remove(target);
            }
        }

        if (CpoisonedEnemies.Contains(target))
        {
            CpoisonedEnemies.Remove(target);
        }
    }

    public void BlackPoison(GameObject target, int damage, ref int plusDamage)
    {
        if (BPoisonedEnemies.Contains(target) == false && target.tag != "Player")
        {
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                BPoisonedEnemies.Add(target);

                GameObject P = Instantiate(BParticles, target.transform.position, Quaternion.Euler(0, 0, 0));
                //KnedlikLib.scaleParticleSize(target, P, 1f);
                P.transform.SetParent(target.transform);
                StartCoroutine(BStartPoison(health, target));
                StartCoroutine(stopPoison(target, P, false,true));
            }

        }
    }

    IEnumerator BStartPoison(Health health, GameObject target)
    {
        yield return new WaitForSeconds(BSpeed * PlayerStats.sharedInstance.TickRate);
        int Damage = KnedlikLib.ScaleDamage(BDamage, true, true);
        Damage = KnedlikLib.ScaleStatusDamage(Damage);

        while (health != null && BPoisonedEnemies.Contains(target))
        {
            if (target.activeInHierarchy)
            {
                int extra = 0;

                float pomH = health.maxHealth * BTrueDamage;
                extra = (int)pomH;

                Color32 C = Constants.GreenColor;
                health.TakeDamage(Damage + extra, C);

                yield return new WaitForSeconds(BSpeed * PlayerStats.sharedInstance.TickRate);
            }
            else
            {
                BPoisonedEnemies.Remove(target);
            }
        }

        if (BPoisonedEnemies.Contains(target))
        {
            BPoisonedEnemies.Remove(target);
        }
    }

    public void Poison(GameObject target, int damage, ref int plusDamage)
    {
        float rand = Random.Range(0, 100);
        if(rand <= chance)
        {
            if (poisonedEnemies.Contains(target) == false)
            {

                if (target.tag != "Player")
                {
                    Health health = target.GetComponent<Health>();
                    if (health != null)
                    {
                        GameObject P = Instantiate(particles, target.transform.position, Quaternion.Euler(0, 0, 0));
                        //KnedlikLib.scaleParticleSize(target, P, 1f);
                        P.transform.SetParent(target.transform);

                        Color32 C = new Color32(255, 255, 255, 255);
                        SpriteRenderer S = target.GetComponent<SpriteRenderer>();
                        if (S != null)
                        {
                            C = S.color;
                        }
                        //S.color = color;

                        poisonedEnemies.Add(target);

                        StartCoroutine(startPoison(health, target));
                        if(duration != 0)
                        {
                            StartCoroutine(stopPoison(target,P,false,C));
                        } 
                    }
                }
            }
        }
       
      //  Destroy(target);
    }

    IEnumerator startPoison(Health health, GameObject target)
    {
        yield return new WaitForSeconds(speed);
        int Damage = KnedlikLib.ScaleDamage(damage, true, true);
        Damage = KnedlikLib.ScaleStatusDamage(Damage);

        while ( health != null && poisonedEnemies.Contains(target))
        {
            if (target.activeInHierarchy)
            {

                int extra = 0;

                if (maxHealthDMG)
                {
                    float pom = health.maxHealth * maxHealthDMGamount;
                    extra = (int)pom;
                }

                Color32 C = Constants.GreenColor;
                health.TakeDamage(Damage + extra,C);

                if (infect)
                {
                    GameObject pom = findWithinDistance(target,distance);
                    if (pom != null)
                    {
                        int z = 0;
                        Poison(pom, 0, ref z);
                    }
                }

                yield return new WaitForSeconds(speed * PlayerStats.sharedInstance.TickRate);
            }else
            {
                poisonedEnemies.Remove(target);
            }
        }

        if(poisonedEnemies.Contains(target))
        {
            poisonedEnemies.Remove(target);
        }
       
    }

    IEnumerator stopPoison(GameObject target, GameObject P,bool Corrupted,bool Black)
    {
        if (Black)
        {
            yield return new WaitForSeconds(BDuration);
        }
        else  if (Corrupted == false)
        {
            yield return new WaitForSeconds(duration);
        }else
        {
            yield return new WaitForSeconds(CDuration);
        }

        if (target != null && target.activeInHierarchy)
        {
            Destroy(P);
        }

        if (Black)
        {
            if (BPoisonedEnemies.Contains(target))
            {
                BPoisonedEnemies.Remove(target);
            }
        }
        if (Corrupted == false)
        {
            if (poisonedEnemies.Contains(target))
            {
                poisonedEnemies.Remove(target);
            }
        }
        else
        {
            if (CpoisonedEnemies.Contains(target))
            {
                CpoisonedEnemies.Remove(target);
            }
        }
    }

    IEnumerator stopPoison(GameObject target,GameObject P,bool Corrupted,Color32 C)
    {
        if (Corrupted == false)
        {
            yield return new WaitForSeconds(duration);
        }
        else
        {
            yield return new WaitForSeconds(CDuration);
        }

        if (target != null && target.activeInHierarchy)
        {
            Destroy(P);
            SpriteRenderer S = target.GetComponent<SpriteRenderer>();
            if (S != null)
            {
               // S.color = C;
            }
        }

        if (Corrupted == false)
        {
            if (poisonedEnemies.Contains(target))
            {
                poisonedEnemies.Remove(target);
            }
        }else
        {
            if(CpoisonedEnemies.Contains(target))
            {
                CpoisonedEnemies.Remove(target);
            }
        }
    }

    GameObject findWithinDistance(GameObject target,float Distance)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(target.transform.position, enemies[i].transform.position) <= Distance && poisonedEnemies.Contains(enemies[i]) == false)
            {
                return enemies[i];
            }
        }

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(gameObject.transform.position, distance);
    }
}
