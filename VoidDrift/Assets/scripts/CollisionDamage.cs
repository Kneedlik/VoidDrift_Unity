
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage;
    public float coolDown;
    private float coolDownTime;

    void Start()
    {
        coolDownTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(coolDownTime > 0)
        {
            coolDownTime -= Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
             if (coolDownTime <= 0)
             {
            collision.gameObject.GetComponent<plaerHealth>().TakeDamage(damage);
             coolDownTime = coolDown;
             }
        }
    }

    

    
}
