using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalProjectile : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] float ProjectileFroce;
    [SerializeField] float ProjectileDelay;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Fire", ProjectileDelay);
    }

    void Fire()
    {
        Instantiate(Projectile,transform.position,Quaternion.Euler(0,0,0));
    }
}
