using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
   public int EnemyAmount;
   [SerializeField] Transform player;
    public float despawnDistance = 0;

    void Start()
    {
        EnemyAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAmount = transform.childCount;

        if (despawnDistance != 0)
        {
            foreach (Transform En in transform)
            {
                if (Vector3.Distance(En.position, player.position) >= despawnDistance)
                {
                    Destroy(En.gameObject);
                }

            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.position, despawnDistance);
    }
}
