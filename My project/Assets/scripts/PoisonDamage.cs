using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{

    public int damage;
    public int Rdamage;
    public float coolDown;
    public float distanceMultiplier = 50;

    private float coolDownTime;
    private float distance;
    public Transform borderX;
    public Transform borderY;
    public GameObject Player;

    void Start()
    { 
        coolDownTime = 0f;
    }

   /* private void OnTriggerStay2D(Collider2D collision)
    {
        float distance = Player.position.x - bordrerX.position.x;

        if (distance < 0)
        {
            distance = distance * -1;
        }
        distance = distance / distanceMultiplier;
        Rdamage = damage + (int)distance;

        
        

        if (coolDownTime > 0)
        {
            coolDownTime -= Time.deltaTime;
        }

        if (collision.gameObject.tag == "Player")
        {
            if (coolDownTime <= 0)
            {
                collision.gameObject.GetComponent<plaerHealth>().TakeDamage(Rdamage);
                coolDownTime = coolDown;
            }
        }
    }
*/
    private void Update()
    {
       // float distance = Player.position.x - bordrerX.position.x;


        if (Mathf.Abs(Player.transform.position.x) > Mathf.Abs(borderX.position.x) || Mathf.Abs(Player.transform.position.y) > Mathf.Abs(borderY.position.y))
        {
            if (coolDownTime > 0)
            {
                coolDownTime -= Time.deltaTime;
            }

            float distance1 = Mathf.Abs(Player.transform.position.x) - Mathf.Abs(borderX.position.x);
            float distance2 = Mathf.Abs(Player.transform.position.y) - Mathf.Abs(borderY.position.y);

            if (distance1 > distance2)
            {
                distance = distance1;
            }
            else distance = distance2;

            distance = distance / distanceMultiplier;
            Rdamage = damage + (int)distance;

            if (coolDownTime <= 0)
            {
                Player.GetComponent<plaerHealth>().TakeDamage(Rdamage);
                coolDownTime = coolDown;
            }

        }
    }



}
