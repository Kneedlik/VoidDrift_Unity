using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenedDroneWeapeons : MonoBehaviour
{
    [SerializeField] gunShipAI AI;
    [SerializeField] float ChangeTime;
    [SerializeField] GameObject ProjectileObj;
    [SerializeField] Transform FirePoint;
    [SerializeField] float CoolDown;
    float TimeStamp;
    //float TimeStamp2;
    bool Attacking;
    gunShipAI ShipAI;
    Transform Player;
    GameObject TempObj;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        ShipAI = GetComponent<gunShipAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TempObj != null && Attacking)
        {
            TempObj.transform.position = FirePoint.position;
        }

        if (AI.Ready == false)
        {
            return;
        }

        if (TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        } else if (Attacking == false)
        {
            Attacking = true;
            ShipAI.Active = false;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        Vector3 dir = Player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        TempObj = Instantiate(ProjectileObj, FirePoint.position, Quaternion.Euler(0, 0, angle));
        yield return new WaitForSeconds(ChangeTime);

        ShipAI.Active = true;
        Attacking = false;
        TimeStamp = CoolDown;
    }
}
