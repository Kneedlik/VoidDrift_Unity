using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setRotation : MonoBehaviour
{

    [SerializeField] Vector3 v;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(v.x,v.y,v.z);
    }
}
