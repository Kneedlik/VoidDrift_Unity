using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterDroneAura : MonoBehaviour
{
    [SerializeField] GameObject IgnoreObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != IgnoreObj)
        {
            BoosterDroneSystem.instance.BoostUnit(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != IgnoreObj)
        {
            BoosterDroneSystem.instance.RemoveUnit(collision.gameObject);
        }
    }
}
