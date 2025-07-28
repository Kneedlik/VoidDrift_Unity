using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingPopUp : MonoBehaviour
{
    NonLevelUpgradeSorting sorting;
    [SerializeField] string SortingTag;
    

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sorting = GameObject.FindWithTag(SortingTag).GetComponent<NonLevelUpgradeSorting>();
            levelingSystem.instance.SetUpLevelMenu(true, sorting);
            Destroy(gameObject);
        }
    }
}
