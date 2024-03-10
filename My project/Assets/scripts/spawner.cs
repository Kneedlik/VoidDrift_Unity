using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public List<Wawe> wawes = new List<Wawe>();

    public float offsetX;
    public float offsetY;
     float spawnRate;
     int spawnN;
   
    public bool active = true;
    public bool Endless = false;

   
    public Transform parent;

     int max;
     int min;

    float end;
   public int count;
    public Counter counter;

    float timeStamp;
    void Start()
    {
        count = 0;
        // StartCoroutine(spawnEnemy());
        spawnRate = wawes[count].spawnRate;
        min = wawes[count].min;
        max = wawes[count].max;
        spawnN = wawes[count].spawnN;
        end= wawes[count].end;
    }

    private void Update()
    {


        if(active && counter.EnemyAmount < max)
        {
            if (timeStamp <= 0 || counter.EnemyAmount < min)
            {

                for (int i = 0; i < spawnN; i++)
                {
                    GameObject E;
                    GameObject En = new GameObject();
                    Vector3 pos = KnedlikLib.GenerateRandPosition(transform.position,offsetX,offsetY);

                    En =  wawes[count].decideEnemy();
                   
                    E = Instantiate(En, pos, Quaternion.identity);
                    E.transform.SetParent(parent);

                    Health health = E.GetComponent<Health>();
                    float pom = health.maxHealth * wawes[count].healthMultiplier;
                    health.maxHealth = (int)pom;

                     
                }
                timeStamp = spawnRate;
            }
        }

        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
           
        }

        if(timer.instance.gameTime >= end)
        {
            if(wawes.Count < count)
            {
                if(Endless == false)
                {
                    gameObject.SetActive(false);
                }
            }else
            {
                setNewWawe();
            }
        }
    }

    public void setNewWawe()
    {
        count++;
         if (wawes.Count < count)
         {
             gameObject.SetActive(false);
         }
         else
         {
         spawnRate = wawes[count].GetSpawnRate();
          min = wawes[count].min;
          max = wawes[count].max;
          spawnN = wawes[count].spawnN;
          end = wawes[count].end;
         }
    }

    private IEnumerator spawnEnemy()
    {
        while (active && counter.EnemyAmount <= max)
        {
            for (int i = 0; i < spawnN; i++)
            {
                GameObject E;
                Vector3 pos = KnedlikLib.GenerateRandPosition(transform.position,offsetX,offsetY);

                GameObject En = wawes[count].decideEnemy();
                E = Instantiate(En, pos, Quaternion.identity);
                E.transform.SetParent(parent);
                
            }
            if(counter.EnemyAmount > min)
            {
                yield return new WaitForSeconds(spawnRate);
            }
            //else
           // {
           //     yield return new WaitForSeconds(0);
           // }
            
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, new Vector3(offsetX * 2,offsetY * 2,0));
    }
}
