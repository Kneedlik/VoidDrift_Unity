using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPopUp : MonoBehaviour
{
    [SerializeField] bool Special = false;


    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            if (Special)
            {
                levelingSystem.instance.SetUpLevelMenuSpecial();
            }
            else
            {
                levelingSystem.instance.SetUpLevelMenuNormal();
            }
            Destroy(gameObject);
        }
    }
}
