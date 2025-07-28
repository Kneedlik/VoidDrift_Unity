using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PortalProjectile : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] float ProjectileFroce;
    [SerializeField] float ProjectileDelay;
    Transform Player;
    Rigidbody2D PlayerRb;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        PlayerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        Invoke("Fire", ProjectileDelay);
    }

    void Fire()
    {
        GameObject Obj = Instantiate(Projectile,transform.position,Quaternion.Euler(0,0,0));
        Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();
        if(KnedlikLib.InterceptionPoint(Player.position,transform.position,PlayerRb.velocity,ProjectileFroce, out var direction))
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
            rb.velocity = direction * ProjectileFroce;
        }
        else
        {
            rb.velocity = (Player.position - transform.position).normalized * ProjectileFroce;
        }
        Destroy(gameObject);

    }
}
