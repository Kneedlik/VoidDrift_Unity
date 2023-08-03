using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GianSlayerSystem : MonoBehaviour
{
   public int tickNeeded;
    public static GianSlayerSystem instance;

    List<GameObject> objects = new List<GameObject>();
    List<int> Damage = new List<int>();
    List<int> index = new List<int>();
   
    

    void Start()
    {
        instance = this;
    }

    
    public void GiantSlay(GameObject target,int damage, ref int scaledDamage)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == null)
            {
                objects.RemoveAt(i);
                Damage.RemoveAt(i);
                index.RemoveAt(i);
            }
        }

        if(objects.Contains(target))
        {
            int i = objects.IndexOf(target);
            index[i]++;

            if (index[i] >= tickNeeded)
            {
                Damage[i]++;
                index[i] = 0;
            }
            else index[i]++;

            scaledDamage += Damage[i];
        }else
        {
            objects.Add(target);
            Damage.Add(0);
            index.Add(0);
        }
    }
}
