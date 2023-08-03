using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    ParticleSystem ps;


    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ps != null && ps.IsAlive() == false)
        {
            Destroy(gameObject);
        }
    }
}
