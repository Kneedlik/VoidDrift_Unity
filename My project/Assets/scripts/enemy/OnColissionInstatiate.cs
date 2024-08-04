using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColissionInstatiate : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] bool PlayerOnly;
    [SerializeField] bool destroy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || PlayerOnly)
        {
            Instantiate(Object,transform.position,transform.rotation);
            if(destroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
