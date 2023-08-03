using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeSystem : MonoBehaviour
{
    [SerializeField] GameObject PrimingObject;
    [SerializeField] GameObject PrimedObject;
    [SerializeField] GameObject ImpactParticles;

    public static PrimeSystem instance;
    public List<GameObject> PrimedEnemies = new List<GameObject>();
    public List<GameObject> PrimedObjects = new List<GameObject>();
   // public Dictionary<GameObject, GameObject> PrimedE = new Dictionary<GameObject, GameObject>();
    public List<GameObject> Priming = new List<GameObject>();
    public List<GameObject> PrimingObjects = new List<GameObject>();
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
            if (PrimedEnemies.Contains(target) == false && Priming.Contains(target) == false)
            {
                priming = Instantiate(PrimingObject,target.transform.position,Quaternion.Euler(0,0,0));
                priming.transform.SetParent(target.transform);
                Priming.Add(target);
                PrimingObjects.Add(priming);
               // PrimedE.Add(target, priming);
                StartCoroutine(startPrime(target));
            }
        }

        if (PrimedEnemies.Contains(target))
        {
             rand = Random.Range(0, 180);
            Instantiate(ImpactParticles, target.transform.position, Quaternion.Euler(rand, -90, 0));

            scaledDamage += detonate(damage);
            int index = PrimedEnemies.IndexOf(target);
            GameObject temp = PrimedObjects[index];
            PrimedObjects.RemoveAt(index);
            PrimedEnemies.Remove(target);
            Destroy(temp);
           
            //   float pom = damage * ( (float)bonusDamage / 100f);
            //  damage = (int)pom;
        }
           
    }

    public int detonate(int damage)
    {
        float pom = damage * ((float)bonusDamage / 100f) * multiplier;
         int d = (int)pom - damage;
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

            PrimedObjects.Add(primed);
            PrimedEnemies.Add(target);

            int index = Priming.IndexOf(target);
            GameObject temp = PrimingObjects[index];
            PrimingObjects.RemoveAt(index);
            Destroy(temp);

            Priming.Remove(target);
        }
        yield return new WaitForSeconds(duration);
        if (target != null)
        {
            if(primed != null)
            {
                if (PrimedObjects.Contains(primed))
                {
                    PrimedObjects.Remove(primed);
                    Destroy(primed);
                }
            }
           

            if (PrimedEnemies.Contains(target))
            {
                PrimedEnemies.Remove(target);
            }
            
        }
    }


}
