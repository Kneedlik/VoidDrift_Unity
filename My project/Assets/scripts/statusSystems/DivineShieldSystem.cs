using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivineShieldSystem : MonoBehaviour
{
    public static DivineShieldSystem instance;
    public bool ready;
    public float coolDown = 15;
    SpriteRenderer spriteRenderer;
    public int damage = 0;
   public GameObject explosion;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        instance = this;
        ready = true;
    }


    public IEnumerator deactivate()
    {
       float Delay = 1f;

        //GameObject E = new GameObject();
     GameObject E = Instantiate(explosion,transform.position,Quaternion.identity);
      shockwawe wawe = E.GetComponent<shockwawe>();
      wawe.damage = damage;
        yield return new WaitForSeconds(Delay); 

        ready = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(coolDown);

        spriteRenderer.enabled = true;
        ready = true;
    }
}
