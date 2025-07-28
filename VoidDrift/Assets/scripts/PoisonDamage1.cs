using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage1 : MonoBehaviour
{
    public int damage;
    public int Rdamage;
    public float coolDown;
    public float distanceMultiplier = 50;

    private float coolDownTime;
    public GameObject Player;

    void Start()
    {
        coolDownTime = 0f;
    }

    private void Update()
    {
        if (Mathf.Abs(Player.transform.position.y) > Mathf.Abs(this.transform.position.y))
        {
            if (coolDownTime > 0)
            {
                coolDownTime -= Time.deltaTime;
            }

            float distance = Mathf.Abs(Player.transform.position.y) - Mathf.Abs(this.transform.position.y);
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
