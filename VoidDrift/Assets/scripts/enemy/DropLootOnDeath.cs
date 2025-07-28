using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLootOnDeath : MonoBehaviour
{
    public List<GameObject> itemPool1 = new List<GameObject>();
    public List<float> dropChancePool1 = new List<float>();
    [SerializeField] Transform pos;

    public int Amount = 1;
    public float ForceMin;
    public float ForceMax;
   
    float[] borders;

 /*   private void OnDestroy()
    {
        if(!isQuit)
        {
            float randomNumber1 = Random.Range(0, 100);
            Debug.Log(randomNumber1);


            float pom = 0;
            borders = new float[itemPool1.Length + 1];
            borders[0] = 0;

            for (int i = 0; i < itemPool1.Length; i++)
            {
                
                pom += dropChancePool1[i];
                borders[i+1] = pom;
            }

            for (int i = 0; i < itemPool1.Length; i++)
            {
                if(randomNumber1 > borders[i] && randomNumber1 < borders[i+1])
                {
                    Instantiate(itemPool1[i],transform.position,Quaternion.identity);
                }
            }

            Debug.Log(borders[1]);

        }
    }
 */
    public void DropLoot()
    {
        float randomNumber1 = Random.Range(0.0f, 100.0f);
       
        float pom = 0;
        borders = new float[itemPool1.Count + 1];
        borders[0] = 0;

        for (int i = 0; i < itemPool1.Count; i++)
        {

            pom += dropChancePool1[i];
            borders[i + 1] = pom;
        }

        for (int i = 0; i < itemPool1.Count; i++)
        {
            if (randomNumber1 > borders[i] && randomNumber1 <= borders[i + 1])
            {
                if(Amount < 1)
                {
                    Amount = 1;
                }

                for (int j = 0; j < Amount; j++)
                {
                    if (pos != null)
                    {
                        GameObject Temp = Instantiate(itemPool1[i], pos.position, Quaternion.Euler(0, 0, 0));
                        if (ForceMax > 0)
                        {
                            Push(Temp);
                        }

                    }
                    else
                    {
                        GameObject Temp = Instantiate(itemPool1[i], transform.position, Quaternion.Euler(0, 0, 0));
                        if (ForceMax > 0)
                        {
                            Push(Temp);
                        }
                    }
                }
            }
        }
    }

    public void Push(GameObject Obj)
    {
        float Force = Random.Range(ForceMin,ForceMax);

        float DirX = Random.Range(-1f, 1f);
        float DirY = Random.Range(-1f, 1f);
        Vector3 Dir = new Vector3(DirX, DirY,0);

        Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.AddForce(Dir * Force, ForceMode2D.Impulse);
        }
    }


}
