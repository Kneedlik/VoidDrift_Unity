
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    //public delegate void specialFunction(GameObject target, int damage, ref int scaledDamage);
    //public specialFunction OnDamageFunction;

    public int health = 100;
    public int maxHealth;
    public int baseMaxHealth;
    public GameObject damageN;
    public GameObject self;
    public GameObject healthBarGObject;
    public healthBar healthBar;
    public int armor = 0;
    public float multiplier = 1;

    public GameObject ParticleExplosion;
    public GameObject DeathAnim;
    public float DeathDelay = 0;
    public List<SimpleVirtual> DeathFunc = new List<SimpleVirtual>();
    public List<SimpleVirtual> OnDamageFunc = new List<SimpleVirtual>();
    //public List<int> DeathFunc123 = new List<int>();
    FlashColor flashColor;

    public bool respawning = false;
     Vector3 location;
    [SerializeField] float respawnTime;
    [SerializeField] GameObject prefab;
    
    public float randOffset = 30;

    public bool levelScaling = false;
    public bool Boss = false;
    public bool AlertOnHit = false;

    bool Dead;
    DamageNumberMaster DmMaster;

    private void Awake()
    {
        if (multiplier == 0)
        {
            multiplier = 1;
        }

        if (healthBarGObject != null)
        {
            healthBar = healthBarGObject.GetComponent<healthBar>();
            healthBarGObject.SetActive(false);
        }
        health = maxHealth;
        flashColor = GetComponent<FlashColor>();

        if (baseMaxHealth == 0)
        {
            baseMaxHealth = maxHealth;
        }
    }

    private void Start()
    {
        DmMaster = GameObject.FindWithTag("DamageNumberMaster").GetComponent<DamageNumberMaster>();

        if(baseMaxHealth == 0)
        {
            baseMaxHealth = maxHealth;
        }
        health = maxHealth;
        
        Dead = false;
        if (respawning)
        {
            location = transform.position;
        }

        if (levelScaling)
        {
            maxHealth = levelingSystem.instance.ScaleHpByLevel(baseMaxHealth);
        }
    }
    public void TakeDamage(int damage,Color32 color = default(Color32))
    {
        if (Dead == false)
        {
            // onDamageEffects(damage);
            // float pom = damageMultiplier * damage ;
            // damage = (int)pom;
            float pom = damage * multiplier;
            damage = (int)pom;

            //Debug.Log("1");
            if (OnDamageFunc != null && OnDamageFunc.Count > 0)
            {
                //Debug.Log("2");
                for (int i = 0; i < OnDamageFunc.Count; i++)
                {
                    //Debug.Log("3");
                    if (OnDamageFunc[i] != null)
                    {
                        //Debug.Log("Activating");
                        OnDamageFunc[i].function();
                    }
                }
            }

            damage -= armor;
            if (damage < 0)
            {
                damage = 0;
            }

            if (damage > 0)
            {
                if (eventManager.OnDamageEnemy != null)
                {
                    eventManager.OnDamageEnemy(damage);
                }

                health -= damage;

                if (healthBarGObject != null)
                {
                    if (healthBarGObject.activeSelf == false)
                    {
                        healthBarGObject.SetActive(true);
                        healthBar.SetMaxHealth(maxHealth);
                    }
                    healthBar.SetHealth(health);
                }

                if (Boss)
                {
                    if (BossBarManager.Instance.health.Contains(this) == false)
                    {
                        BossBarManager.Instance.AddBar(this);
                    }
                    else
                    {
                        BossBarManager.Instance.UpdateBar();
                    }
                }

                if (AlertOnHit)
                {
                    BaseAI AI = gameObject.GetComponent<BaseAI>();
                    if (AI != null && AI.alert == false)
                    {
                        AI.alert = true;
                        AI.patrol = false;
                    }
                }

                if (flashColor != null)
                {
                    flashColor.Flash();
                }

                if (damageN)
                {
                    if(color == default(Color))
                    {
                        damagePopUp(damage, Color.white);
                        //Debug.Log("Default");
                    }else
                    {
                        damagePopUp(damage, color);
                    }
                    
                }
                if (health <= 0)
                {
                    Die();
                }
            }
        }
    }

   /* public void TakeDamage(int damage,Color32 color)
    {
        if (Dead == false)
        {
            float pom = damage * multiplier;
            damage = (int)pom;

            if (OnDamageFunction != null)
            {
                OnDamageFunction(gameObject, damage, ref damage);
            }

            damage -= armor;
            if (damage < 0)
            {
                damage = 0;
            }

            if (OnDamageFunction != null)
            {
                OnDamageFunction(gameObject, damage, ref damage);
            }

            if (damage > 0)
            {
                if (eventManager.OnDamageEnemy != null)
                {
                    eventManager.OnDamageEnemy(damage);
                }

                health -= damage;

                if (healthBarGObject != null)
                {
                    if (healthBarGObject.activeSelf == false)
                    {
                        healthBarGObject.SetActive(true);
                        healthBar.SetMaxHealth(maxHealth);
                    }
                    healthBar.SetHealth(health);
                }

                if (Boss)
                {
                    if (BossBarManager.Instance.health.Contains(this) == false)
                    {
                        BossBarManager.Instance.AddBar(this);
                    }
                    else
                    {
                        BossBarManager.Instance.UpdateBar();
                    }

                }

                if (AlertOnHit)
                {
                    BaseAI AI = gameObject.GetComponent<BaseAI>();
                    if (AI != null && AI.alert == false)
                    {
                        AI.alert = true;
                        AI.patrol = false;
                    }
                }

                if (flashColor != null)
                {
                    flashColor.Flash();
                }

                if (damageN)
                {
                    damagePopUp(damage, color);
                }
                if (health <= 0)
                {
                    Die();
                }
            }
        }
    } */

    public void PreDestroy()
    {
        DropLootOnDeath[] d = gameObject.GetComponents<DropLootOnDeath>();
        dropXP xp = gameObject.GetComponent<dropXP>();

        if (xp != null)
        {
            xp.addXP();
        }

        if (d != null)
        {
            for (int i = 0; i < d.Length; i++)
            {
                d[i].DropLoot();
            }
        }

        if (ParticleExplosion != null)
        {
            Instantiate(ParticleExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
        }

        if (DeathAnim != null)
        {
            Instantiate(DeathAnim, transform.position, Quaternion.identity);
        }
    }

    public void Final()
    {
        if (respawning == false)
        {
            if (healthBarGObject != null)
            {
                Destroy(healthBarGObject);
            }

            if (self != null)
            {
                Destroy(self);
            }
            else Destroy(gameObject);

        }
        else if (prefab != null)
        {
            RespawnManager.instance.respawn(respawnTime, prefab, location, true);
            if (healthBarGObject != null)
            {
                Destroy(healthBarGObject);
            }

            if (self != null)
            {
                Destroy(self);
            }
            else Destroy(gameObject);
        }
        else
        {
            RespawnManager.instance.respawn(respawnTime, gameObject, location, false);
        }
    }

    public void Die()
    {
        Dead = true;

        if(gameObject.layer == 12)
        {
            MasterManager.Instance.progressionState.MapEnemiesKilled++;
        }else MasterManager.Instance.progressionState.EnemiesKilled++;

        if(Boss)
        {
            BossBarManager.Instance.RemoveBar(this);
        }

        if (eventManager.OnKill != null)
        {
            eventManager.OnKill(gameObject);
        }

        if (DeathFunc != null && DeathFunc.Count > 0)
        {
            for (int i = 0; i < DeathFunc.Count; i++)
            {
                if (DeathFunc[i] != null)
                {
                    DeathFunc[i].function();
                }
            }
        }
        
        Invoke("PreDestroy",DeathDelay);
        Invoke("Final",DeathDelay);   
    }

 /*   public void onDamageEffects(int damage)

    {
        if (eventManager.OnDamage != null)
        {
            eventManager.OnDamage(damage, gameObject);
        }

        if (health <= 0)
        {
            Die();
        }
    }*/

    public void damagePopUp(int Damage,Color32 color)
    {
        if(DmMaster == null)
        {
            return;
        }

        if(DmMaster.Full)//
        {
            return;
        }

        Vector3 pos = transform.position;
        float offsetX = Random.Range(-Constants.DamageNumberOffset,Constants.DamageNumberOffset);
        float offsetY = Random.Range(-Constants.DamageNumberOffset,Constants.DamageNumberOffset);

        offsetX = offsetX / 10;
        offsetY = offsetY / 10;

        pos.x = pos.x + offsetX;
        pos.y = pos.y + offsetY;

        var go = Instantiate(damageN,pos, Quaternion.identity);
        go.transform.SetParent(DmMaster.transform);

        TMP_Text Text = go.GetComponent<TMP_Text>();
        Text.text = Damage.ToString();
        Text.color = color;
        Text.fontSize = ScaleDamagePopUp(go.GetComponent<TMP_Text>().fontSize,Damage);
    }

    public float ScaleDamagePopUp(float FontSize,int damage)
    {
        int FirstMin = 20;
        int FirstMax = 140;
        float FirstPercent = 0.5f;

        int SecondMin = 140;
        int SecondMax = 400;
        float SecondPercent = 0.5f;

        int ThirdMin = 400;
        int ThirdMax = 2200;
        float ThirdPercent = 0.5f;

        int FourthMin = 2200;
        int FourthMax = 40000;
        float FourthPercent = 1f;

        float Fraction;
        int DamageExceding;
        int Diff;

        float Scale = 1f;
        if(damage > FirstMin && damage < FirstMax)
        {
            DamageExceding = damage - FirstMin;
            Diff = FirstMax - FirstMin;

            Fraction = DamageExceding / Diff;
            Scale = 1f + Fraction * FirstPercent;
        }else if (damage > SecondMin && damage < SecondMax)
        {
            DamageExceding = damage - SecondMin;
            Diff = SecondMax - SecondMin;

            Fraction = DamageExceding / Diff;
            Scale = 1f + Fraction * SecondPercent;
        }
        else if (damage > ThirdMin && damage < ThirdMax)
        {
            DamageExceding = damage - ThirdMin;
            Diff = ThirdMax - ThirdMin;

            Fraction = DamageExceding / Diff;
            Scale = 1f + Fraction * ThirdPercent;
        }
        else if (damage > FourthMin && damage < FourthMax)
        {
            DamageExceding = damage - SecondMin;
            Diff = FourthMax - FourthMin;

            Fraction = DamageExceding / Diff;
            Scale = 1f + Fraction * FourthPercent;
        }

        if(Scale > Constants.DamageNumberMaxScale)
        {
            Scale = Constants.DamageNumberMaxScale;
        }

        FontSize = FontSize * Scale;
        return FontSize;
    }

    public void setUp()
    {
        if (levelScaling && levelingSystem.instance != null)
        {
            maxHealth = levelingSystem.instance.ScaleHpByLevel(baseMaxHealth);
        }
        health = maxHealth;
    }

    private void OnEnable()
    {
        setUp();
    }
}
