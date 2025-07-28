using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireActivate : MonoBehaviour
{
    public gunShipLaser G;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            G.setFireOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            G.SetFrireOff();
        }
    }

}
