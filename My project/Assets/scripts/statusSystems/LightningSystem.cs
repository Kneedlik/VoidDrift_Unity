using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSystem : MonoBehaviour
{
    public static LightningSystem instance;
    public int damage1;
    public int chain;
    public int chance;
    public float distance;

    public bool removeBrittle = false;
    public int removeBrittleDamage;
    public int removeBrittleChance = 0;

    public List<GameObject> shocked = new List<GameObject>();
    public float shockDuration;
    public bool shock;
    public int armorDamage;
    public int shockChance;

    [SerializeField] GameObject lightningObject;
    [SerializeField] GameObject ImpactEffect;
    public float lightningDuration;
    GameObject Begin;
    GameObject End;


    void Start()
    {
        instance = this;
    }

    public void lightningProc(GameObject target,int damage,ref int Damage)
    {
        int rand = Random.Range(0, 100);

        if (rand <= chance)
        {
            Health health = target.GetComponent<Health>();
           
            List<Transform> Enemies = new List<Transform>();
            Enemies.Add(target.transform);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Transform currentTarget = target.transform;
            Instantiate(ImpactEffect, currentTarget.position, Quaternion.Euler(90, 0, 0));
            RemoveBrittle(currentTarget.gameObject, health);
            Begin = target;

            if (shock)
            {
                int rand1 = Random.Range(0, chance);

                if(rand1 <= 100)
                {
                    StartCoroutine(Shock(currentTarget.gameObject));
                }
             
            }

            for (int i = 0; i < chain; i++)
            {
                if(KnedlikLib.FindClosestEnemy(Begin.transform,out currentTarget,Enemies))
                {
                    if (Vector3.Distance(Begin.transform.position, currentTarget.position) <= distance)
                    {
                        Health h = currentTarget.GetComponent<Health>();
                        Enemies.Add(currentTarget);

                        RemoveBrittle(currentTarget.gameObject, h);
                        Instantiate(ImpactEffect, currentTarget.position, Quaternion.Euler(90, 0, 0));

                        if (shock)
                        {
                            int rand1 = Random.Range(0, chance);

                            if (rand1 <= 100)
                            {
                                StartCoroutine(Shock(currentTarget.gameObject));
                            }
                        }

                        End = currentTarget.gameObject;
                       StartCoroutine(lightningGraphics(Begin, End));
                        Begin = currentTarget.gameObject;
                        h.TakeDamage(damage1);
                    }
                }
            }
            health.TakeDamage(damage1);
        }
    }

    public IEnumerator Shock(GameObject target)
    {
        if (shocked.Contains(target) == false)
        {
            Health health = target.GetComponent<Health>();
           
            shocked.Add(target);
            health.armor -= armorDamage;

            yield return new WaitForSeconds(shockDuration);

            if (health != null)
            {
                health.armor += armorDamage;
            }
            shocked.Remove(target);
            
        }
    }

    IEnumerator lightningGraphics(GameObject begin, GameObject ending)
    {
        GameObject pom = Instantiate(lightningObject,transform);
        //Debug.Log("EEE");
        LightningBolt script = pom.GetComponent<LightningBolt>();
        script.StartObject.transform.position = begin.transform.position;
        script.EndObject.transform.position = ending.transform.position;
        

        yield return new WaitForSeconds(lightningDuration / 3);

        if(begin != null)
        {
            script.StartObject.transform.position = begin.transform.position;
        }

        if(ending != null)
        {
            script.EndObject.transform.position = ending.transform.position;
        }

        yield return new WaitForSeconds(lightningDuration / 3);

        if (begin != null)
        {
            script.StartObject.transform.position = begin.transform.position;
        }

        if (ending != null)
        {
            script.EndObject.transform.position = ending.transform.position;
        }
        
        yield return new WaitForSeconds(lightningDuration / 3);
        Destroy(pom);
    }

    public void RemoveBrittle(GameObject target,Health health)
    {
        if (removeBrittle)
        {
            if (BrittleSystem.Instance.BrittleEnemies.Contains(target))
            {
                BrittleSystem.Instance.RemoveBrittle(target);
                health.TakeDamage(removeBrittleDamage);
            }
        }
    }

    

}
