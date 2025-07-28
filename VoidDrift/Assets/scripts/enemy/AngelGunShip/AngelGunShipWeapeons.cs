using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelGunShipWeapeons : MonoBehaviour
{
    [SerializeField] gunShipAI Ship;
    [SerializeField] float CoolDownA1;
    [SerializeField] GameObject PortalPrefabA1;
    [SerializeField] int PortalAmountA1;
    [SerializeField] float PortalDelayA1;
    [SerializeField] float MaxDistanceA1;

    [SerializeField] float CoolDownA2;
    [SerializeField] Transform FirePoint1A2;
    [SerializeField] Transform FirePoint2A2;
    [SerializeField] GameObject ProjectilePrefabA2;
    [SerializeField] float ForceA2;
    [SerializeField] int WaweAmountA2;
    [SerializeField] int ProjectileAmountA2;
    [SerializeField] float WaweDelayA2;
    [SerializeField] float OffsetA2;

    float TimeStamp;
    bool Attacking;
    int OnCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        OnCoolDown = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if (Ship.Ready == false)
        {
            return;
        }

        if(TimeStamp <= 0 && Attacking == false)
        {
            Attacking = true;
            DecideAttack();
        }
    }

    void DecideAttack()
    {
        if(OnCoolDown == 1)
        {
            OnCoolDown = 2;
            StartCoroutine(Attack2());

        }else if(OnCoolDown == 2)
        {
            OnCoolDown = 1;
            StartCoroutine(Attack1());
        }
    }

    IEnumerator Attack1()
    {
        for (int i = 0; i < PortalAmountA1; i++)
        {
            Vector3 Offset = new Vector3(Random.Range(MaxDistanceA1 * -1, MaxDistanceA1), Random.Range(MaxDistanceA1 * -1, MaxDistanceA1), 0);

            Instantiate(PortalPrefabA1, transform.position + Offset, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(PortalDelayA1);
        }
        Attacking = false;
        TimeStamp = CoolDownA1;
    }

    IEnumerator Attack2()
    {
        for (int i = 0; i < WaweAmountA2; i++)
        {
            bool Flip = false;
            float OffsetTemp = OffsetA2 / 2;
            Rigidbody2D rb;
            for(int j = 0;j < ProjectileAmountA2; j++)
            {
                if(Flip == false)
                {
                    rb = Instantiate(ProjectilePrefabA2, FirePoint1A2.position, Quaternion.Euler(0, 0, FirePoint1A2.rotation.eulerAngles.z + OffsetTemp)).GetComponent<Rigidbody2D>();
                    rb.velocity = rb.transform.up * ForceA2;
                    rb = Instantiate(ProjectilePrefabA2, FirePoint2A2.position, Quaternion.Euler(0, 0, FirePoint2A2.rotation.eulerAngles.z + OffsetTemp)).GetComponent<Rigidbody2D>();
                    rb.velocity = rb.transform.up * ForceA2;
                    Flip = true;
                }else
                {
                    rb = Instantiate(ProjectilePrefabA2, FirePoint1A2.position, Quaternion.Euler(0, 0, FirePoint1A2.rotation.eulerAngles.z - OffsetTemp)).GetComponent<Rigidbody2D>();
                    rb.velocity = rb.transform.up * ForceA2;
                    rb = Instantiate(ProjectilePrefabA2, FirePoint2A2.position, Quaternion.Euler(0, 0, FirePoint2A2.rotation.eulerAngles.z - OffsetTemp)).GetComponent<Rigidbody2D>();
                    rb.velocity = rb.transform.up * ForceA2;
                    Flip = false;
                }
                OffsetTemp += OffsetA2;
                

            }
            yield return new WaitForSeconds(WaweDelayA2);

        }
        Attacking = false;
        TimeStamp = CoolDownA2;
    }
}
