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
    public float HealingOrbDropChance;
    public GameObject HealingOrbPrefab;
    public List<type> ownedColours = new List<type>();

    weapeon gun;
    PlayerMovement speedScript;
    Transform player;
    plaerHealth health;

    private void Awake()
    {
        speedScript = GetComponent<PlayerMovement>();
        player = GetComponent<Transform>();
        health = GetComponent<plaerHealth>();
        OnLevel = null;
        addRevive(0);
        sharedInstance = this;
        gun = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();

        //ResetUpgrades();
    }

    public void increaseMaxHP(int HP)
    {
        health.setMaxHP(HP + health.maxHealth);
        health.increaseHP(health.health + HP);
    }

    public void increaseDMG(int dmg)
    {
        damageMultiplier += dmg;
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
    }

    public void IncreaseSpeed(int speed)
    {
        SpeedMultiplier += speed;   
    }

    public void IncreaseAS(int AS)
    {
        ASmultiplier += AS;
    }

    public void IncreaseEXP(int exp)
    {
        EXPmultiplier += exp;
    }

    public void increaseProjectiles(int amount)
    {
        gun.projectileCount += amount;
        bonusProjectiles += amount;

        //gun.ResetFirePoints();
        player.rotation = Quaternion.Euler(0, 0, 0);
        gun.setFirepoints();
    }

    public void increaseSideProjectiles(int amount)
    {
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
        speedScript.updateMS(SpeedMultiplier);
        gun.updateAS(ASmultiplier);
        gun.updateSize(areaMultiplier);
        gun.updateDamage(damageMultiplier);

        Summon[] summons = FindObjectsOfType<Summon>();

        for (int i = 0; i < summons.Length; i++)
        {
            summons[i].scaleSummonDamage();
            summons[i].scaleSize();
        }

        if(OnLevel != null)
        {
            OnLevel();
        }    
    }

    private void OnApplicationQuit()
    {
        damageMultiplier = 100;
        atackSpeedMultiplier = 100;
        areaMultiplier = 100;
    }

    public void ResetUpgrades()
    {
        damageMultiplier = 100;
        atackSpeedMultiplier = 100;
        areaMultiplier = 100;
        SpeedMultiplier = 100;
        ASmultiplier = 100;
        EXPmultiplier = 100;
        ExtraDamage = 0;
        bonusProjectiles = 0;
        sideProjectiles = 0;
        ProjectileForce = 1;
        thorns = 0;
        SummonDamage = 100;
        revives = 0;
        TickRate = 1;
        ownedColours = new List<type>();

        gun.ResetStats();
        player.rotation = Quaternion.Euler(0, 0, 0);
        gun.setSideFirepoints();
        gun.ResetFirePoints();
        gun.setFirepoints();

        Summon[] summons = FindObjectsOfType<Summon>();

        for (int i = 0; i < summons.Length; i++)
        {
            Destroy(summons[i]);
        }

        UpdateStats();

        upgradeList[] Upgrades = FindObjectsOfType<upgradeList>();
        for (int i = 0; i < Upgrades.Length; i++)
        {
            for (int j = 0; j < Upgrades[i].list.Count; j++)
            {
                Upgrades[i].list[j].upgrade.level = 0;
                Upgrades[i].list[j].upgrade.description = Upgrades[i].list[j].BaseDescription;
                Upgrades[i].list[j].upgrade.rarity = Upgrades[i].list[j].BaseRarity;
            }
        }

        eventManager.ClearAllEffects();
    }

}
