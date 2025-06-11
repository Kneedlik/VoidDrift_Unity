using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoBossExplo : MonoBehaviour
{
    [SerializeField] GameObject ExploPrefab;
    [SerializeField] float Delay;
    float TimeStamp;

    void Start()
    {
        TimeStamp = Delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if(TimeStamp <= 0)
        {
            Instantiate(ExploPrefab,transform.position,Quaternion.Euler(0,0,0));
            Destroy(gameObject);
        }
    }
}
