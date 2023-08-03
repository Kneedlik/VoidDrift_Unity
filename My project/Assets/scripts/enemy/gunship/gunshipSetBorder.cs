using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunshipSetBorder : MonoBehaviour
{
    public LineRenderer line1;
    public LineRenderer line2;

    public Transform border1;
    public Transform border2;
    public Transform border3;
    public Transform border4;


    void Start()
    {
        line1.SetPosition(0, border3.position);
        line1.SetPosition(1, border1.position);

        line2.SetPosition(0, border4.position);
        line2.SetPosition(1, border2.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
