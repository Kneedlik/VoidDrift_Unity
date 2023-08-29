
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int maxHealth;
    public int baseMaxHealth;
    public GameObject damageN;
    public GameObject self;
    public GameObject healthBarGObject;
    public healthBar healthBar;
    public float damageMultiplier = 1;
    public int armor = 0;
    public float multiplier;

    public GameObject ParticleExplosion;
    public GameObject DeathAnim;
    FlashColor flashColor;

    public bool respawning = false;
     Vector3 location;
    [SerializeField] float respawnTime;
   [SerializeField] GameObject prefab;
    
    public float randOffset = 30;

    public bool levelScaling = false;

    public bool stop;
    
    private void Start()
    {
        if(respawning)
        {
            location = transform.position;
        }

        multiplier = 1;
        if (healthBarGObject != null)
        {
            healthBar = healthBarGObject.GetComponent<healthBar>();
            healthBarGObject.SetActive(false);
        }
        health = maxHealth;
        flashColor = GetComponent<FlashColor>();
        baseMaxHealth = maxHealth;

        if(levelScaling)
        {
            float pom = baseMaxHealth * levelingSystem.instance.healthPerLevel * (levelingSystem.instance.level - 1);
            maxHealth = baseMaxHealth + (int)pom;
        }
    }
    public void TakeDamage(int damage)
    {
        // onDamageEffects(damage);

        // float pom = damageMultiplier * damage ;
        // damage = (int)pom;
        float pom = damage * multiplier;
        damage = (int)pom;

        damage -= armor;
        if (damage < 0)
        {
            damage = 0;
        }

        if(damage > 0)
        {
            if(eventManager.OnDamageEnemy != null)
            {
                eventManager.OnDamageEnemy(damage);
            }

            health -= damage;

            if(healthBarGObject != null)
            {
                if (healthBarGObject.activeSelf == false)
                {
                    healthBarGObject.SetActive(true);
                    healthBar.SetMaxHealth(maxHealth);
                }
                healthBar.SetHealth(health);
            }

            if (flashColor != null)
            {
                flashColor.Flash();
            }

            if (damageN)
            {
                damagePopUp(damage, Color.white);
            }
            if (health <= 0)
            {
                Die();
            }
        }

        
       if(stop == false)
        {
            stop = true;
        }
    }

    public void TakeDamage(int damage,Color32 color)
    {
        damage -= armor;
        if(damage < 0)
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

            if(healthBarGObject != null)
            {
                if (healthBarGObject.activeSelf == false)
                {
                    healthBarGObject.SetActive(true);
                    healthBar.SetMaxHealth(maxHealth);
                }
                healthBar.SetHealth(health);
            }

            if (flashColor != null )
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

    public void Die()
    {
        DropLootOnDeath[] d = gameObject.GetComponents<DropLootOnDeath>();
        dropXP xp = gameObject.GetComponent<dropXP>();

        if (eventManager.OnKill != null)
        {
            eventManager.OnKill(gameObject);
        }

        if (xp != null)
        {
            xp.addXP();
        }

        if(d != null)
        {
            for (int i = 0; i < d.Length; i++)
            {
                d[i].DropLoot();
            }
        }

        if(ParticleExplosion != null)
        {
            Instantiate(ParticleExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
        }

        if(DeathAnim != null)
        {
            Instantiate(DeathAnim,transform.position,Quaternion.identity);
        }

        
        if(respawning == false)
        {
            if(healthBarGObject != null)
            {
                Destroy(healthBarGObject);
            }
            
            if(self != null)
            {
                Destroy(self);
            }else Destroy(gameObject);
           
        }else if(prefab != null)
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
            RespawnManager.instance.respawn(respawnTime,gameObject,location, false);
        }
        
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
        Vector3 pos = transform.position;
        float offsetX = Random.Range(-randOffset,randOffset);
        float offsetY = Random.Range(-randOffset,randOffset);

        offsetX = offsetX / 10;
        offsetY = offsetY / 10;

        pos.x = pos.x + offsetX;
        pos.y = pos.y + offsetY;

        var go = Instantiate(damageN,pos, Quaternion.identity);
        go.GetComponent<TMP_Text>().text = Damage.ToString();
        go.GetComponent<TMP_Text>().color = color;
       // go.GetComponent<TextMesh>().text = Damage.ToString();
    }

    public void setUp()
    {
        if (levelScaling)
        {
            float pom = 1 * levelingSystem.instance.healthPerLevel * (levelingSystem.instance.level - 1);
            maxHealth = 1 + (int)pom;
        }

        health = maxHealth;
    }

}
