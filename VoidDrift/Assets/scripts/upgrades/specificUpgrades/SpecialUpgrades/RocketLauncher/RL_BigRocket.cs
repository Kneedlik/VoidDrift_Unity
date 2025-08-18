using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_BigRocket : upgrade
{
    // Start is called before the first frame update
    public static RL_BigRocket instance;
    [SerializeField] int Damage;
    [SerializeField] float MultiplierAmount;
    [SerializeField] int CurrentShot;
    [SerializeField] int ShotsNeeded;
    rocketLauncher RocketLauncher;
    public GameObject BigRocketPrefab;
    public float BigRocketForce;
    public float DamageMultiplier = 1f;


    void Start()
    {
        instance = this;
        RocketLauncher = GameObject.FindWithTag("Weapeon").GetComponent<rocketLauncher>();
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {
            eventManager.OnFire += FireBigRocket;
            description = string.Format("Big Big Rocket Damage + {0}%",MultiplierAmount * 100);
        }else
        {
            DamageMultiplier += MultiplierAmount;
        }
        level++;
    }

    public void FireBigRocket(GameObject Weapeon)
    {
        CurrentShot++;
        if (CurrentShot >= ShotsNeeded)
        {
            GameObject Temp = Instantiate(BigRocketPrefab, RocketLauncher.firePoint.position, RocketLauncher.firePoint.rotation);
            Rigidbody2D TempRB = Temp.GetComponent<Rigidbody2D>();
            TempRB.AddForce(BigRocketForce * RocketLauncher.firePoint.up, ForceMode2D.Impulse);

            BulletScript Bullet = Temp.GetComponent<BulletScript>();
            Bullet.setDamage(ScaleBigRocketDamage());

            CurrentShot = 0;
        }
    }

    int ScaleBigRocketDamage()
    {
        float DamageTemp = Damage * DamageMultiplier;
        DamageTemp = DamageTemp * RocketLauncher.damageMultiplier;
        DamageTemp = DamageTemp * (PlayerStats.sharedInstance.damageMultiplier / 100f);
        DamageTemp += PlayerStats.sharedInstance.ExtraDamage;
        
        return (int)DamageTemp;
    }
}
