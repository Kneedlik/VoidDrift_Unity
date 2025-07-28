using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeSummon : Summon
{
    float TimeStamp;
    [SerializeField] float DamageDelay;
    [SerializeField] GameObject ParticleExplo;
    [SerializeField] GameObject DamageObj;
    public float TrueDamage;

    // Start is called before the first frame update
    void Start()
    {
        ScaleSummonStats();
        PlayerStats.OnLevel += ScaleSummonStats;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeStamp >= 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if(TimeStamp < 0)
        {
            TimeStamp = fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(ParticleExplo, transform.position, Quaternion.Euler(-90, 0, 0));
        Invoke("DealDamage",DamageDelay);
    }

    void DealDamage()
    {
        explosion Explo = Instantiate(DamageObj, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<explosion>();
        Explo.damage = damage;
        Explo.TrueDamage = TrueDamage;
        
    }
}
