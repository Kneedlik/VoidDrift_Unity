using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        sharedInstance = this;
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
                        KnedlikLib.scaleParticleSize(target, P, 1f);
                        P.transform.SetParent(target.transform);

                        SpriteRenderer S = target.GetComponent<SpriteRenderer>();
                        Color32 C = S.color;
                        S.color = color;

                        poisonedEnemies.Add(target);

                        StartCoroutine(startPoison(health, target));
                        if(duration != 0)
                        {
                            StartCoroutine(stopPoison(target,P,C));
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

        while( health != null && poisonedEnemies.Contains(target))
        {
            if (target.activeInHierarchy)
            {

                int extra = 0;

                if (maxHealthDMG)
                {
                    float pom = health.maxHealth * maxHealthDMGamount;
                    extra = (int)pom;
                }

                health.TakeDamage(Damage + extra);

                if (infect)
                {
                    GameObject pom = findWithinDistance(target);
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

    IEnumerator stopPoison(GameObject target,GameObject P,Color32 C)
    {
        yield return new WaitForSeconds(duration);

        
        

        if(target != null && target.activeInHierarchy)
        {
            Destroy(P);
            SpriteRenderer S = target.GetComponent<SpriteRenderer>();
            if (S != null)
            {
                S.color = C;
            }
        }
        
        
        if(poisonedEnemies.Contains(target))
        {
            poisonedEnemies.Remove(target);
        }
    }

    GameObject findWithinDistance(GameObject target)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(target.transform.position, enemies[i].transform.position) <= distance && poisonedEnemies.Contains(enemies[i]) == false)
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
