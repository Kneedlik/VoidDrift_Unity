using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserMissles : MonoBehaviour
{
    public bool Active;
    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] GameObject Prefab;
    [SerializeField] int Amount = 3;
    [SerializeField] float Delay;
    [SerializeField] float Burst;
    public float Range;

    [SerializeField] float CoolingDuration;
    [SerializeField] float CoolingDurationAlternative;
    public int FireMode = 1;

    public float timeStamp;

    void Start()
    {
        Active = false;
    }

    public void Activate()
    {
        if (Active == false)
        {
            Active = true;
        }   
    }

    public void Deactivate()
    {
        if (Active)
        {
            Active = false;
        }
        
    }

    private void Update()
    {
        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (Active)
        {
            if(timeStamp <= 0)
            {
                Fire();
                if(FireMode == 1)
                {
                    float Rand = Random.Range(0f,4f);
                    timeStamp = CoolingDuration + Rand;
                }else if (FireMode == 2)
                {
                    float Rand = Random.Range(0f,2f);
                    timeStamp = CoolingDurationAlternative + Rand;
                }
            }
        
        }
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
