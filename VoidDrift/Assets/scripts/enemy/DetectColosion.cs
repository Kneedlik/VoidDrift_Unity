using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectColosion : MonoBehaviour
{
   public bool fire;

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fire = true;
        }
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fire = false;
        }
    }



}
