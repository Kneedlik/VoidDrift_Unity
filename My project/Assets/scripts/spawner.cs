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

    [SerializeField] string PrintPrefix;
    [SerializeField] string FileName;

    float timeStamp;
    void Start()
    {
        PrintWawePowerLevel();
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

                    En = wawes[count].decideEnemy();
                   
                    E = Instantiate(En, pos, Quaternion.identity);
                    E.transform.SetParent(parent);

                    Health health = E.GetComponent<Health>();
                    float pom = health.maxHealth * wawes[count].healthMultiplier;
                    health.maxHealth = (int)pom;

                    dropXP XP = E.GetComponent<dropXP>();
                    pom = XP.xpValue * wawes[count].XpMultiplier;
                    XP.xpValue = (int)pom;
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
                if(gameObject.activeInHierarchy == false)
                {
                    return;
                }
                if (wawes[count].Boss != null)
                {
                    if (wawes[count].SpawnedBoss == false)
                    {
                        Vector3 pos = KnedlikLib.GenerateRandPosition(transform.position, offsetX, offsetY);
                        Instantiate(wawes[count].Boss, pos, Quaternion.identity);
                    }
                }
            }
        }
    }

    public void setNewWawe()
    {
         count++;
         if (wawes.Count <= count)
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

    void PrintWawePowerLevel()
    {
        int PowerLevel = 0;
        int XpNeeded = 0;
        float Temp;
        float XpTemp;
        List<string> StringList = new List<string>();
        string FullLine;

        StringList.Add(FileName + "                    ");
        for(int i = 0;i < wawes.Count;i++)
        {
            PowerLevel = 0;
            XpNeeded = 0;
            for(int j = 0;j < wawes[i].enemies.Count;j++)
            {
                //Debug.Log(i);
                //Debug.Log();
                Health health = wawes[i].enemies[j].GetComponent<Health>();
                float TrueChance = (float)wawes[i].chanses[j] / 100f;
                Temp = health.maxHealth * wawes[i].healthMultiplier * TrueChance * wawes[i].spawnN;
                Temp = Temp / wawes[i].spawnRate;
                PowerLevel += (int)Temp;

                dropXP Xp = wawes[i].enemies[j].GetComponent<dropXP>();
                XpTemp = Xp.xpValue * wawes[i].XpMultiplier * TrueChance * wawes[i].spawnN;
                XpTemp = XpTemp / wawes[i].spawnRate;
                XpNeeded += (int)XpTemp;

            }

            if (wawes[i].Boss == null)
            {
                FullLine = string.Format("{0}, Wawe: {1}, Power level: {2}, Spawn Rate: {3}, Spawn number: {4}, Health: {5}, Xp: {6}                 ",PrintPrefix, i, PowerLevel, wawes[i].spawnRate, wawes[i].spawnN, wawes[i].healthMultiplier, XpNeeded * 60);
            }else
            {
                FullLine = string.Format("{0}, Wawe: {1}, Power level: {2}, Spawn Rate: {3}, Spawn nubber: {4}, Health: {5}, Xp: {6}      Boss round ",PrintPrefix, i, PowerLevel, wawes[i].spawnRate, wawes[i].spawnN, wawes[i].healthMultiplier, XpNeeded * 60);
            }
            //Debug.Log(FullLine);
            StringList.Add(FullLine);
        }
        SaveManager.SaveLog(FileName,StringList);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, new Vector3(offsetX * 2,offsetY * 2,0));
    }
}
