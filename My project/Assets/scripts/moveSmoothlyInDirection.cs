using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSmoothlyInDirection : MonoBehaviour
{
    public Vector3 direction;
    public float speed;


   

    private void FixedUpdate()
    {
       // transform.position = transform.position + direction * speed;
        transform.Translate(direction.normalized * speed);
    }
}
