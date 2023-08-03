using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiting : MonoBehaviour
{
    public float speed;
    Vector3 rot;


    void Start()
    {
        rot = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rot * Time.deltaTime * speed);
    }
}
