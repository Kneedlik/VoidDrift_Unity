using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class TracerSummon : MonoBehaviour
{
    [SerializeField] float PrepTime;
    //[SerializeField] float RandomDelay;
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] float Force;
    public float ForceMultiplier = 1;
    float TimeStamp;

    // Start is called before the first frame update
    void Start()
    {
        TimeStamp = PrepTime + Random.Range(PrepTime * -1, PrepTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if(TimeStamp <= 0)
        {
            Shoot();
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Rigidbody2D RbBullet = Instantiate(ProjectilePrefab, transform.position,Quaternion.Euler(0,0,0)).GetComponent<Rigidbody2D>();
        
        Vector3 Temp = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(Temp.y, Temp.x) * Mathf.Rad2Deg - 90;
        RbBullet.rotation = angle;
        RbBullet.velocity = Temp * (Force * ForceMultiplier);

        BulletScript Bullet = RbBullet.GetComponent<BulletScript>();
        float Damage = KnedlikLib.ScaleDamage(Bullet.damage,true,true);
        Damage = Damage * MasterManager.Instance.PlayerInformation.SummonDamageMultiplier;
        Damage = Damage * (PlayerStats.sharedInstance.SummonDamage / 100f);

        Bullet.setDamage((int)Damage);
        KnedlikLib.ScaleParticleByFloat(Bullet.gameObject, 1f, true);
        
    }
}
