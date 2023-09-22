using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats sharedInstance;
    public delegate void LevelEffect();
    public static LevelEffect OnLevel;

    public int damageMultiplier = 100;
    public int atackSpeedMultiplier = 100;
    public int areaMultiplier = 100;
    public int SpeedMultiplier = 100;
    public int ASmultiplier = 100;
    public int EXPmultiplier = 100;
    public int ExtraDamage = 0;

    public int bonusProjectiles = 0;
    public int sideProjectiles = 0;

    public float ProjectileForce = 1;
    public int thorns = 0;

    public int SummonDamage = 100;

    public int revives = 0;
    [SerializeField] Slider slider;

    public float TickRate = 1;

    public List<type> ownedColours = new List<type>();

    weapeon gun;
    PlayerMovement speedScript;
    

    private void Awake()
    {
        OnLevel = null;
        addRevive(0);
        sharedInstance = this;
        gun = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();
    }

    public void increaseMaxHP(int HP)
    {
        plaerHealth health = GameObject.FindGameObjectWithTag("Player").GetComponent<plaerHealth>();
        health.setMaxHP(HP + health.maxHealth);
        health.increaseHP(health.health + HP);
    }

    public void increaseDMG(int dmg)
    {
        damageMultiplier += dmg;
        gun.updateDamage(damageMultiplier); 
    }

    public int scaleDamage(int damage)
    {
        float pom = damage * (damageMultiplier / 100f);
        damage = (int)pom;
        return damage;
    }

    public void increaseAREA(int area)
    {
        areaMultiplier += area;
        Debug.Log("AREA");
        gun.updateSize(areaMultiplier);
    }

    public void IncreaseSpeed(int speed)
    {
        SpeedMultiplier += speed;
        Debug.Log("SPEED");
        speedScript = GetComponent<PlayerMovement>();
        speedScript.updateMS(SpeedMultiplier);
    }

    public void IncreaseAS(int AS)
    {
        ASmultiplier += AS;
        Debug.Log("AS");
        gun.updateAS(ASmultiplier);
    }

    public void IncreaseEXP(int exp)
    {
        EXPmultiplier += exp;
        Debug.Log("EXP");
    }

    public void increaseProjectiles(int amount)
    {
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>(); 
        gun.projectileCount += amount;
        bonusProjectiles += amount;
        
        gun.ResetFirePoints();
        player.rotation = Quaternion.Euler(0, 0, 0);
        gun.setFirepoints();
    }

    public void increaseSideProjectiles(int amount)
    {
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        sideProjectiles += amount;
        gun.sideProjectiles = sideProjectiles;

        player.rotation = Quaternion.Euler(0, 0, 0);
        gun.setSideFirepoints();
    }

    //public void increasePeojetileSpeed(int amount)
   // {
   //     gun.Force = gun.BaseForce * ProjectileForce;
   // }

    public void addRevive(int amount)
    {
        if(revives < 10)
        {
            revives += amount;
            slider.value = revives;
        }
       
    }

   
    public void UpdateStats()
    {
       
        increaseDMG(0);

        Summon[] summons = FindObjectsOfType<Summon>();

        for (int i = 0; i < summons.Length; i++)
        {
            summons[i].scaleSummonDamage();
            summons[i].scaleSize();
        }

        if(OnLevel != null)
        {

        }
        
    }

    private void OnApplicationQuit()
    {
        damageMultiplier = 100;
        atackSpeedMultiplier = 100;
        areaMultiplier = 100;
    }

}
