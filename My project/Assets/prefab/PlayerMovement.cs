
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //mouse
    private Rigidbody2D rb;
    private Vector2 mousePos;
    public Camera cam;

    //combat movement
    private Vector2 movement;
    public float baseMoveSpeed;
    public float baseMaxSpeed;
   public float moveSpeed;
   public float maxSpeed;

    //travel movement
    public float moveSpeed_1;
    public float maxSpeed_1;
    private bool Wcheck;
    private bool Scheck;
    public float rotationLerp;
    private bool movementState;
    public float speed;

    //dash
   // public float dashVelocity;
   // public float startDashTime;
   // private float dashTime;
   // private bool spaceCheck;
   // private bool spaceDown;
   // private bool dashStart;
   // public float LoweredMass;

    //empoweredDash
   // public float empoweredDashTime1 = 1;
   // public float empoweredDashTime2 = 1.75f;
   // public float empoweredDashPush1;
   // public float empoweredDashPush2;
   // public float empoweredDashBlowBack1;
   // public float empoweredDashBlowBack2;
   // public float DashBlowBack;
   // private int dashType;

    //private float spaceDownTime;

    //mana
   // public PlayerMana mana;
    //public int dashCost;

    private float pom;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        //dashTime = startDashTime;
        movementState = false;
        pom = rb.mass;
       // spaceDownTime = 0;
        updateMS(100);
    }
    void Update()
    {
        speed = rb.velocity.magnitude;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        //activeVerticalSpeed = Mathf.Lerp(activeVerticalSpeed, Input.GetAxisRaw("Vertical") * VerticalMoveSpeed, verticalAcceleration * Time.deltaTime);
        //activeHorizontalSpeed = Mathf.Lerp(activeHorizontalSpeed, Input.GetAxisRaw("Horizontal") * HorizontalMoveSpeed, horizontalAcceleration * Time.deltaTime);


     /*   if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceDown = true;
        } 
    
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spaceCheck = true;
            spaceDown = false;
        }
     

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (movementState)
            {
                movementState = false;
            }
            else movementState = true;
        }
     */
        if (Input.GetKey(KeyCode.W))
        {
            Wcheck = true;
        } else Wcheck = false;

        if (Input.GetKey(KeyCode.S))
        {
            Scheck = true;
        } else Scheck = false;

       /*
        if (spaceDown)
        {
            spaceDownTime += Time.deltaTime;
        }
       */
    }
    private void FixedUpdate()
    {
        //mouse
        Vector2 lookDir = (mousePos - rb.position);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

     
            rb.rotation = Mathf.LerpAngle(rb.rotation, angle, rotationLerp);
            lookDir.Normalize();

        //movement
        if (movementState)
        {
            if (Wcheck)
            {
                moveCharacter(lookDir, moveSpeed_1);
            }
            else if (Scheck)
            {
                moveCharacter(-lookDir, moveSpeed_1);
            }

            //   if (Acheck)
            //   {
            // Vector3 va1 = new Vector3(lookDir.x,lookDir.y,0);
            //transform.rotation =  Quaternion.AngleAxis(90, Vector3.forward) ;
            //       rb.AddForce( transform.up * moveSpeed_1, ForceMode2D.Force);
            //   }
            //   else if(Dcheck)
            //   {
            //       rb.AddForce(-transform.up * moveSpeed_1, ForceMode2D.Force);
            //   }
            setMaxSpeed(maxSpeed_1);
        }
        else
        {
            moveCharacter(movement, moveSpeed);
            setMaxSpeed(maxSpeed);

        }

        //dash
        /*
        if (spaceCheck)
        {
            if (spaceDownTime >= empoweredDashTime1 && spaceDownTime < empoweredDashTime2)
            {
                startDash(dashCost, 250, lookDir);
                Debug.Log(250);
                dashType = 2;
            }
            else if (spaceDownTime >= empoweredDashTime2)
            {
                startDash(dashCost, 350, lookDir);
                Debug.Log(350);
                dashType = 3;
            }
            else if (mana.mana >= 20)
            {
                startDash(dashCost, dashVelocity, lookDir);
                Debug.Log(150);
                dashType = 1;
            }

        }
        

        if (dashStart)
        {

            dashTime -= Time.deltaTime;

            if (dashTime <= 0)
            {
                dashTime = startDashTime;
                rb.velocity = rb.velocity.normalized * maxSpeed;

                rb.mass = pom;
                dashStart = false;
            }
        }
        */
    }

    //dash collision bounce

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (dashStart)
        {
            Rigidbody2D RB = collision.gameObject.GetComponent<Rigidbody2D>(); ;
            Vector2 lookDir = (mousePos - rb.position).normalized;
            Vector2 neglookDir = lookDir * -1;
            lookDir = (mousePos - rb.position).normalized;

            if(dashType == 1)
            {
                rb.AddForce(neglookDir * DashBlowBack, ForceMode2D.Impulse);
            }
            else if (dashType == 2 )
            {
                RB.AddForce(lookDir * empoweredDashPush1, ForceMode2D.Impulse);
                rb.AddForce(neglookDir * empoweredDashBlowBack1, ForceMode2D.Impulse);
            }
            else if (dashType == 3)
            {
                RB.AddForce(lookDir * empoweredDashPush2, ForceMode2D.Impulse);
                rb.AddForce(neglookDir * empoweredDashBlowBack2, ForceMode2D.Impulse);
            } 

        }
    }

    */

    private void setMaxSpeed(float maxSpeed)
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
           // if (dashStart == false)
           // {
                rb.velocity = rb.velocity.normalized * maxSpeed;
           // }
        }

    }
/*
    void startDash(int cost, float velocity, Vector2 lookDir)
    {
        spaceCheck = false;
        rb.mass = LoweredMass;

        rb.velocity = lookDir * velocity;
        dashStart = true;
        mana.LowerMana(dashCost);
        spaceDownTime = 0;
    }
*/
    void moveCharacter(Vector2 lookdir, float speed)
    {
        rb.AddForce(lookdir * speed, ForceMode2D.Force);
    }

    public float GiveSpeed()
    {
        return speed;
    }

    public void updateMS(int multiplier)
    {
        float realMultiplier = (float)multiplier / 100f ;
        
        moveSpeed = baseMoveSpeed * realMultiplier;
        maxSpeed = baseMaxSpeed * realMultiplier;
    }
}
