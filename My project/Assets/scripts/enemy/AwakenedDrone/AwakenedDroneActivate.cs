using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenedDroneActivate : SimpleVirtual
{
    [SerializeField] float ActivateDistance;
    //[SerializeField] Animation Anim;
    gunShipAI ShipAI;
    AwakenedDroneWeapeons Vape;
    Transform Player;
    Animator animator;
    Health health;

    private void Awake()
    {
        ShipAI = GetComponent<gunShipAI>();
        Vape = GetComponent<AwakenedDroneWeapeons>();
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        //animator = GetComponent<Animator>();
        //Anim.Stop();
        ShipAI.enabled = false;
        Vape.enabled = false;
        animator.enabled = false;
        health.OnDamageFunc.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,Player.position) <= ActivateDistance)
        {
            function();
        }
    }

    public override void function()
    {
        Debug.Log("yep");
        //Anim.Play();
        animator.enabled = true;
        Vape.enabled = true;
        ShipAI.enabled = true;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,ActivateDistance);
    }

}
