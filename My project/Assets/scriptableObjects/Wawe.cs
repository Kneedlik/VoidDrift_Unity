using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wawe", menuName = "Wawes")]

public class Wawe : ScriptableObject
{
    public float end;
    public int min;
    public int max;
    public List<GameObject> enemies = new List<GameObject>();
    public List<float> chanses = new List<float>();
    public float spawnRate;
    public int spawnN;

    public float healthMultiplier = 1;
    public GameObject Boss;
    [HideInInspector] public bool SpawnedBoss = false;

   public GameObject decideEnemy()
    {
        GameObject En = null;

        if (enemies.Count > 1)
        {
            float rand = Random.Range(0, 100);
            float[] pom = new float[10];
            float j = 0;
            pom[0] = 0;

            for (int i = 0; i < chanses.Count; i++)
            {
                j += chanses[i];
                pom[i + 1] = j;
            }

            rand = Random.Range(0, j);

            for (int i = 0; i < chanses.Count; i++)
            {
                if (rand > pom[i] && rand <= pom[i + 1])
                {
                    En = enemies[i];
                    break;
                }
            }
        }else
        {
            En = enemies[0];
        }
        return En;
    }

    public float GetSpawnRate()
    {
        return spawnRate;
    }


}
