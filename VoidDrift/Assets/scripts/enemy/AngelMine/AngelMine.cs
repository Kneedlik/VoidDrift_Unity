using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMine : SimpleVirtual
{
    [SerializeField] float KillDistance;
    [SerializeField] GameObject Explosion;
    [SerializeField] LineRenderer Line;
    Transform Player;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        health = GetComponent<Health>();
        health.DeathFunc.Add(this);
        KnedlikLib.DrawCircle(Line, KillDistance, 200);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,Player.position) < KillDistance)
        {
            health.Die();
        }
    }

    public override void function()
    {
        Instantiate(Explosion, transform.position, Quaternion.Euler(0, 0, 0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,KillDistance);
    }

}
