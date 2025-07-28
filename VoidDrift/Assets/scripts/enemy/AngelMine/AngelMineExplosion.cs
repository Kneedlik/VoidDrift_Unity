using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMineExplosion : MonoBehaviour
{
    public int ProjectileAmount;
    public float MaxDistance;
    [SerializeField] float NewProjectileDelay;
    [SerializeField] GameObject Portal; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoStuff());
    }

    IEnumerator DoStuff()
    {
        for (int i = 0; i < ProjectileAmount; i++)
        {
            Vector3 Offset = new Vector3(Random.Range(MaxDistance * -1,MaxDistance), Random.Range(MaxDistance * -1,MaxDistance), 0);

            Instantiate(Portal,transform.position + Offset,Quaternion.Euler(0,0,0));
            yield return new WaitForSeconds(NewProjectileDelay);
        }
        

        Destroy(gameObject);
    }
}
