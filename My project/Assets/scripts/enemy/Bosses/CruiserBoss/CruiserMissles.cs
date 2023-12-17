using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserMissles : MonoBehaviour
{
    CruiserBossAI BossAI;
    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] GameObject Prefab;
    [SerializeField] int Amount = 3;
    [SerializeField] float Delay;
    [SerializeField] float Burst;

    void Start()
    {
        BossAI = GetComponent<CruiserBossAI>();
    }

    public void Fire()
    {
        StartCoroutine(FireCorutine());
    }

    IEnumerator FireCorutine()
    {
        GameObject Obj;
        Rigidbody2D rb;

        for (int i = 0; i < Amount; i++)
        {
            Obj = Instantiate(Prefab, firePoint1.position, firePoint1.rotation);
            rb = Obj.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint1.up * Burst, ForceMode2D.Impulse);

            Obj = Instantiate(Prefab, firePoint2.position, firePoint2.rotation);
            rb = Obj.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint2.up * Burst, ForceMode2D.Impulse);

            yield return new WaitForSeconds(Delay);
        }   
    }
}
