using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiningSummon : MonoBehaviour
{
    public GameObject main;
    Transform player;
    


    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
      //   e = Instantiate(main, player.position, Quaternion.Euler(0, 0, 0));

       // followPosition p = main.GetComponent<followPosition>();
       // p.obj = player.gameObject;
       ResetSums();
    }

    private void ResetSums()
    {
        GameObject[] summons = GameObject.FindGameObjectsWithTag("Summon");

        for (int i = 0; i < summons.Length; i++)
        {
            SpiningSummon s = summons[i].GetComponent<SpiningSummon>();
            if(s != null)
            {
                s.main.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }



}
