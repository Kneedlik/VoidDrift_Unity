using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plaerHealth : MonoBehaviour
{
    public static plaerHealth Instance;

    public int health = 100;
    public int baseHealth = 100;
    public int maxHealth;
    public healthBar healthBar;
   // public healthBar healthBar2;
    public float timer;
    public float Regen;
    public float RegenBuffer;
    public float RegenCooldown;
    float RegenTimeStamp;
  
    bool healCheck;

    public float iframes;
    bool invincible;
    public DivineShieldSystem shield;
    float timeStamp;
    [SerializeField] GameObject DamageParticle;

    //death
    [SerializeField] bool godMode = false;
    [SerializeField] float deathDelay;
    [SerializeField] GameObject ParticleExplosion;
    [SerializeField] ParticleSystem Smoke;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject XPbar;
    [SerializeField] GameObject minimap;
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject deathLight;

    //revive
    [SerializeField] GameObject reviveParticle;
    [SerializeField] Slider ReviveSlider;

     public GameObject shockWawe;

    [SerializeField] float healthBarFlashDuration;
    public Image PlayerHfill;
    Color32 BaseBarColor;
    public Coroutine flash;
    public bool half = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Regen += MasterManager.Instance.PlayerInformation.HealthRegen;
        float HealthTemp = baseHealth * MasterManager.Instance.PlayerInformation.HealthMultiplier;
        baseHealth = (int)HealthTemp;
        health = baseHealth;
        maxHealth = baseHealth;
        healthBar.SetMaxHealth(baseHealth);
       // healthBar2.SetMaxHealth(maxHealth);
       // InvokeRepeating("Regeneration",0.0f, 1.0f / regenTime);
       
        timer = 0.0f;
        BaseBarColor = PlayerHfill.color;
    }
    public void TakeDamage(int damage)
    {
        if (damage == 0)
        {
            return;
        }

        if(godMode)
        {
            return;
        }

        bool ready = false;
        if(shield.isActiveAndEnabled)
        {
            if(shield.ready)
            {
                ready = true;
            }
        }
       
        if(invincible == false && ready == false)
        {
            if(eventManager.OnDamage != null)
            {
                eventManager.OnDamage(damage);
            }


            if (half != true)
            {
                health -= damage;
            }else
            {
                health -= damage / 2;
            }

           
            healthBar.SetHealth(health);
            CameraFollow.instance.startShake(1, 1);
            flashHealthBar();

            float Rand = Random.Range(0.0f, 180.0f);
            Instantiate(DamageParticle, transform.position, Quaternion.Euler(Rand, 90, 0));
            Instantiate(shockWawe,transform.position,Quaternion.identity);
            timeStamp = iframes;

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayId(6);
            }
           
        }else if (shield.ready && invincible == false)
        {
            Instantiate(shockWawe, transform.position, Quaternion.identity);
            CameraFollow.instance.startShake(1, 1);
            flashHealthBar();
            StartCoroutine(shield.deactivate());
        }
        

        if (health <= 0 && godMode == false)
        {
            if(PlayerStats.sharedInstance.revives > 0)
            {
                StartCoroutine(Revive());
                StartCoroutine(FlashLight());
                gameObject.GetComponent<PlayerMovement>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                Smoke.Play();

                Invoke("Die", deathDelay);
                StartCoroutine(FlashLight());
                Invoke("Explode", deathDelay / 1.5f);
            }
        }
    }

    private void Update()
    {
        
        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }
        if(timeStamp > 0)
        {
            invincible = true;
        }else invincible = false;

        if(RegenTimeStamp > 0)
        {
            RegenTimeStamp -= Time.deltaTime;
        }

        if(RegenTimeStamp <= 0)
        {
            if (health < maxHealth)
            {
                RegenTimeStamp = RegenCooldown;
                RegenBuffer += Regen;
                float HealthTemp = RegenBuffer % 1;
                HealthTemp = RegenBuffer - HealthTemp;
                for (int i = 0;i < HealthTemp;i++)
                {
                    if(health < maxHealth)
                    {
                        health++;
                        RegenBuffer = RegenBuffer - 1;
                        healthBar.SetHealth(health);
                    }
                }
            }
        }



    }

    IEnumerator Revive()
    {
        yield return new WaitForSeconds(deathDelay / 1.5f);

        PlayerStats.sharedInstance.revives -= 1;
        ReviveSlider.value -= 1;
        health = maxHealth;
        healthBar.SetHealth(health);
        timeStamp = 1.5f;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        Instantiate(reviveParticle, transform.position, Quaternion.Euler(-90, 0, 0));

        //if(statsOnRevive.instance.level >= 0)
        //{
            PlayerStats.sharedInstance.increaseDMG(statsOnRevive.instance.damage);
            PlayerStats.sharedInstance.IncreaseAS(statsOnRevive.instance.fireRate);
            PlayerStats.sharedInstance.increaseMaxHP(statsOnRevive.instance.health);
        //}

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");       
        Renderer[] renderers = new Renderer[Enemies.Length];

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < Enemies.Length; i++)
        {
            renderers[i] = Enemies[i].GetComponent<Renderer>();
            if(renderers[i].isVisible)
            {
                Health h = Enemies[i].GetComponent<Health>();
                h.TakeDamage(h.maxHealth);
            }
        }

    }
    void Die()
    {
        Time.timeScale = 0;
        deathScreen.SetActive(true);

        XPbar.SetActive(false);
        Timer.SetActive(false);
        minimap.SetActive(false);
        HealthBar.SetActive(false);
       // Destroy(gameObject);
    }

    void Explode()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        CameraFollow.instance.startShake(3f, 1.5f);
        Instantiate(ParticleExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
    }

    IEnumerator FlashLight()
    {
        yield return new WaitForSeconds(deathDelay / 1.5f);
        deathLight.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        deathLight.SetActive(false);
    }

    public bool getHealCheck()
    {
        return healCheck;
    }

    public void setMaxHP(int amount)
    {
        maxHealth = amount;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void increaseHP(int amount)
    {
        health += amount;

        if(health > maxHealth)
        {
            int diff = health - maxHealth;
            health = maxHealth;
         
           // if(OverHealDamage.instance.level > 0)
           // {
           //     Transform target;
            //    if(KnedlikLib.FindRandomEnemy(out target))
           //     {
            //        Health targetHealth = target.GetComponent<Health>();
            //        if(targetHealth != null)
            //        {
            //            float temp = amount * OverHealDamage.instance.multiplier;
             //           targetHealth.TakeDamage((int)temp);
            //        }
             //   }
           // }
        }
        healthBar.SetHealth(health);
    }

   void flashHealthBar()
   {
        if(flash != null)
        {
            StopCoroutine(flash);
            PlayerHfill.color = BaseBarColor;
        }
        flash = StartCoroutine(flashHealthBarRutine(BaseBarColor));
   }

    IEnumerator flashHealthBarRutine(Color c)
    {
        PlayerHfill.color = Color.white;
        yield return new WaitForSeconds(healthBarFlashDuration);
        PlayerHfill.color = c; 
    }

    




}
