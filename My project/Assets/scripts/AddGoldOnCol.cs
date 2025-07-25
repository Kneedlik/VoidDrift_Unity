using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGoldOnCol : MonoBehaviour
{
    public int Amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MasterManager.Instance.AddGold(Amount, true);
            Destroy(gameObject);
        }
    }
}
