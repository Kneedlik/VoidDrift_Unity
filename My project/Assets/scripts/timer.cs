
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class timer : MonoBehaviour
{
    public static timer instance;
    public float gameTime;
    public float winTime;

   [SerializeField] GameObject VictoryScreen;
    bool gameOver = false;

    Health health;

    [SerializeField] GameObject XPbar;
    [SerializeField] GameObject minimap;
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject FinalBoss;

    void Start()
    {
        gameOver = false;
        instance = this;   
    }

    private void FixedUpdate()
    {
        if (gameOver == false)
        {
            if (gameTime < winTime && winTime != 0)
            {
                gameTime += Time.fixedDeltaTime;
            } else
            {
                gameOver = true;
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
                
                for(int i = 0; i < gameObjects.Length; i++)
                {
                    health = gameObjects[i].GetComponent<Health>();
                    if (health != null)
                    {
                        GameObject DeathAnim = health.DeathAnim;
                        GameObject ParticleExplosion = health.ParticleExplosion;

                        if (ParticleExplosion != null)
                        {
                            Instantiate(ParticleExplosion, gameObjects[i].transform.position, Quaternion.Euler(-90, 0, 0));
                        }

                        if (DeathAnim != null)
                        {
                            Instantiate(DeathAnim, gameObjects[i].transform.position, Quaternion.identity);
                        }

                        Destroy(health.gameObject);

                    }
                }

                //Invoke("victory", Constants.VictoryDelay);
                Invoke("SummonFinalBoss", Constants.FinalBossSpawnDelay);
                gameOver = true;
            }
        }
        
    }

    public void SummonFinalBoss()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        Instantiate(FinalBoss, Player.transform.transform.position,Quaternion.Euler(0,0,0));
    }

    public void VictoryDelayed(float Delay)
    {
        Invoke("victory", Delay);
    }

    public void victory()
    {
        VictoryScreen.SetActive(true);
        Time.timeScale = 0;

        XPbar.SetActive(false);
        Timer.SetActive(false);
        minimap.SetActive(false);
        HealthBar.SetActive(false);
    }
}
