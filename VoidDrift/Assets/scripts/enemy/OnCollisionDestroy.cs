using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDestroy : MonoBehaviour
{
    [SerializeField] bool UseHealth = false;
    [SerializeField] List<string> names = new List<string>();
    bool pom;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pom = false;
        for (int i = 0; i < names.Count; i++)
        {
            if (collision.gameObject.CompareTag(names[i]))
            {
                pom = true;
            }
        }

        if (pom)
        {
            if(UseHealth)
            {
                Health health = GetComponent<Health>();
                if (health != null)
                {
                    health.Die();
                }
            }else
            {
                Destroy(gameObject);
            }
            
        }

    }
}
