using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarker : Summon
{
    public int targets;
    float timeStamp;
    Transform target;
    PrimeSystem instance;
   // [SerializeField] GameObject Targetingprefab;


    private void Start()
    {
        instance = GameObject.FindGameObjectWithTag("StatusSystem").GetComponent<PrimeSystem>();
    }

    void Update()
    {
        if(timeStamp >= 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0)
        {
            shoot();          
        }
    }

    void shoot()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> Enemies2 = new List<GameObject>();
       

        Renderer[] renderers = new Renderer[Enemies.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i] = Enemies[i].GetComponent<Renderer>();

            if (renderers[i].isVisible)
            {
                bool viable = true;

                if (instance.PrimedEnemies.Contains(Enemies[i]) || instance.Priming.Contains(Enemies[i]))
                {
                    viable = false;
                }

                if(viable == true)
                {
                  
                    Enemies2.Add(Enemies[i]);
                }

                
            }
            
        }

        int pom = 0;

        if (Enemies2.Count > 0)
        {
            if(Enemies2.Count <= targets)
            {
                for (int i = 0; i < Enemies2.Count; i++)
                {
                  //  GameObject t = Instantiate(Targetingprefab, Enemies2[i].transform.position, Quaternion.Euler(0, 0, 0));
                   // t.GetComponent<followPosition>().obj = Enemies2[i];
                    instance.prime(Enemies2[i],0,ref pom);

                }
            }else
            {
                List<int> L = new List<int>();
                for (int j = 0; j < targets; j++)
                {
                    int rand;
                    do
                    {
                         rand = Random.Range(0, Enemies2.Count);
                    } while(L.Contains(rand));

                   // GameObject t = Instantiate(Targetingprefab, Enemies2[rand].transform.position, Quaternion.Euler(0, 0, 0));
                   // t.GetComponent<followPosition>().obj = Enemies2[rand];
                    instance.prime(Enemies2[rand], 0, ref pom);
                }

                
            }
            timeStamp = fireRate;
        }
       
    }

    

}
