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
                    Vector3 pos = generatePosition();

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
                Vector3 pos = generatePosition();

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

    Vector3 generatePosition()
    {
        Vector3 pos = new Vector3(0,0,0);
        int rand = Random.Range(0, 2);

        if (rand == 1)
        {
            float rand1 = Random.Range(offsetX * -1, offsetX);
            int rand2 = Random.Range(0, 2);
            if(rand2 == 1)
            {
                pos = new Vector3(transform.position.x + rand1, transform.position.y + offsetY, 0);
            }else
            {
                pos = new Vector3(transform.position.x + rand1, transform.position.y + offsetY * -1, 0);
            }
           
        }
        else
        {
            float rand1 = Random.Range(offsetY * -1, offsetY);
            int rand2 = Random.Range(0, 2);

            if (rand2 == 1)
            {
                pos = new Vector3(transform.position.x + offsetX, transform.position.y + rand1, 0);
            }else
            {
                pos = new Vector3(transform.position.x + offsetX * -1, transform.position.y + rand1, 0);
            }

               
        }

        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, new Vector3(offsetX * 2,offsetY * 2,0));
    }
}
