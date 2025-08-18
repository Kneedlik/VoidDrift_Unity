using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeSystem : MonoBehaviour
{
    [SerializeField] GameObject PrimingObject;
    [SerializeField] GameObject PrimedObject;
    [SerializeField] GameObject ImpactParticles;

    public static PrimeSystem instance;
    public Dictionary<GameObject, GameObject> PrimedEnemies = new Dictionary<GameObject, GameObject>();
   // public Dictionary<GameObject, GameObject> PrimedE = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, GameObject> PrimingEnemies = new Dictionary<GameObject, GameObject>();
    // public Dictionary<GameObject, GameObject> PrimingE = new Dictionary<GameObject, GameObject>();
    public int bonusDamage;
    public float timeToPrime;
    public float duration;
    public float chance;

    public float multiplier = 1;
    public float speedMultiplier = 1;

    void Start()
    {
        instance = this;
    }

    public void prime(GameObject target, int damage,ref int scaledDamage)
    {
        float rand = Random.Range(0, 100);
        GameObject priming;

        if(rand <= chance) 
        {
            if (PrimedEnemies.ContainsKey(target) == false && PrimingEnemies.ContainsKey(target) == false)
            {
                priming = Instantiate(PrimingObject,target.transform.position,Quaternion.Euler(0,0,0));
                priming.transform.SetParent(target.transform);
                //Priming.Add(target);
                //PrimingObjects.Add(priming);
                PrimingEnemies.Add(target, priming);
                StartCoroutine(startPrime(target));
            }
        }

        if (PrimedEnemies.ContainsKey(target))
        {
            rand = Random.Range(0, 180);
            Instantiate(ImpactParticles, target.transform.position, Quaternion.Euler(rand, -90, 0));

            scaledDamage += detonate(damage);
            Destroy(PrimedEnemies[target]);
            PrimedEnemies.Remove(target);
           
            //   float pom = damage * ( (float)bonusDamage / 100f);
            //  damage = (int)pom;
        }
           
    }

    public int detonate(int damage)
    {
        float pom = damage * ((float)bonusDamage / 100f) * multiplier;
        int d = (int)pom;
        return d;
         
    }

    IEnumerator startPrime(GameObject target)
    {
        yield return new WaitForSeconds(timeToPrime * speedMultiplier);
        GameObject primed = null;

        if(target != null)
        {
            primed = Instantiate(PrimedObject, target.transform.position, Quaternion.Euler(0, 0, 0));
            primed.transform.SetParent(target.transform);

            //PrimedObjects.Add(primed);
            //PrimedEnemies.Add(target);
            PrimedEnemies.Add(target, primed);

            Destroy(PrimingEnemies[target]);
            PrimingEnemies.Remove(target);
        }
        yield return new WaitForSeconds(duration);
        if (target != null)
        {
            if(PrimedEnemies.ContainsKey(target))
            {
                if (PrimedEnemies[target] != null)
                {
                    Destroy(PrimedEnemies[target]);
                }
                PrimedEnemies.Remove(target);
            }
            
        }
    }


}
