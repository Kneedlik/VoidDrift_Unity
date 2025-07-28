using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPosition1 : MonoBehaviour
{
    public Transform obj;


    // Update is called once per frame
    void Update()
    {
        this.transform.position = obj.position;
    }
}
