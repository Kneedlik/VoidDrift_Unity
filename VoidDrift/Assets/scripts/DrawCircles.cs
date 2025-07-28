using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircles : MonoBehaviour
{
    [SerializeField] List<float> Circles = new List<float>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < Circles.Count; i++)
        {
            Gizmos.DrawWireSphere(transform.position, Circles[i]);
        }
    }

}
