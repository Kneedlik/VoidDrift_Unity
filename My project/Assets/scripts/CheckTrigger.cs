using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    public bool Colliding;

    private void Start()
    {
        Colliding = false;   
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Colliding = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Colliding = false; 
    }


}
