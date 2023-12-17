using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    public bool Colliding;
    public string Tag;

    private void Start()
    {
        Colliding = false; 
        if (Tag == "")
        {
            Tag = "Player";
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tag)
        {
            Colliding = true;
        }   
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Tag)
        {
            Colliding = true;
        }

    }


}
