using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusatMovement : EnemyFollow
{
    [SerializeField] float TargetDistance;
    [SerializeField] float MaxDistance;
    [SerializeField] float IncreasedSpeed;
    [SerializeField] float IncreasedMaxSpeed;
    [SerializeField] float StrafeSpeed;
    [SerializeField] float FlipStrafeDirTime;
    [SerializeField] float MaxDeviation;
    [SerializeField] float FlipTime;
    float MaxSpeedTemp;
    public bool Ready;

    int StrafeDirection;
    float TrueStrafeSpeed;
    float TimeStamp;


    // Start is called before the first frame update
    void Start()
    {
        StrafeDirection = Random.Range(0,2);
        if(StrafeDirection == 1)
        {
            TrueStrafeSpeed = StrafeSpeed -1;
        }else
        {
            TrueStrafeSpeed = StrafeSpeed;
        }
        SetVars();
    }

    private void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }else
        {
            TimeStamp = FlipStrafeDirTime;
            FlipStrafeDirection();
        }
    }

    private void FixedUpdate()
    {
        float SpeedTemp;
        Vector3 dir;
        
        if(Vector3.Distance(target.position,transform.position) > MaxDistance)
        {
            SpeedTemp = IncreasedSpeed;
            MaxSpeedTemp = IncreasedMaxSpeed;
            Ready = false;
        }else
        {
            SpeedTemp = speed;
            MaxSpeedTemp = maxSpeed;
            if (Ready == false)
            {
                Invoke("Flip", FlipTime);
            }
        }

        if(Vector3.Distance(target.position,transform.position) < TargetDistance)
        {
            dir = target.position - transform.position;
            dir = dir * -1;
        }
        else
        {
            dir = target.position - transform.position;
        }

        if (Vector3.Distance(target.position, transform.position) < TargetDistance + MaxDeviation && Vector3.Distance(target.position, transform.position) > TargetDistance - MaxDeviation)
        {
            //Debug.Log("Doing stuff");
            rb.AddForce(transform.right * TrueStrafeSpeed * Multiplier);
            SpeedTemp = 0;
        }

        rb.AddForce(dir.normalized * SpeedTemp * Multiplier);
        KnedlikLib.SetMaxSpeed(MaxSpeedTemp * Multiplier, rb);
    }

    private void LateUpdate()
    {
        KnedlikLib.SetMaxSpeed(MaxSpeedTemp * Multiplier, rb);
    }

    void Flip()
    {
        Ready = true;
    }

    void FlipStrafeDirection()
    {
        TrueStrafeSpeed = TrueStrafeSpeed * -1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, TargetDistance);
        Gizmos.DrawWireSphere(transform.position, TargetDistance + MaxDeviation);
        Gizmos.DrawWireSphere(transform.position, TargetDistance - MaxDeviation);
        Gizmos.DrawWireSphere(transform.position, MaxDistance);
    }
}
