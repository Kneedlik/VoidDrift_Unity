using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciate_on_start : MonoBehaviour
{

    public GameObject[] itemPool1;
    public float[] dropChancePool1;

    float[] borders;

    void Start()
    {
        float randomNumber1 = Random.Range(0, 100);
       // Debug.Log(randomNumber1);


        float pom = 0;
        borders = new float[itemPool1.Length + 1];
        borders[0] = 0;

        for (int i = 0; i < itemPool1.Length; i++)
        {
            pom += dropChancePool1[i];
            borders[i + 1] = pom;
        }

        for (int i = 0; i < itemPool1.Length; i++)
        {
            if (randomNumber1 > borders[i] && randomNumber1 < borders[i + 1])
            {
              GameObject E = Instantiate(itemPool1[i], transform.position, Quaternion.identity);
                if(transform.parent != null)
                {
                    E.transform.parent = transform.parent;
                }
               
            }
        }

        Destroy(gameObject);

    }

}
