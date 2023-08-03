using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fodderRocket : MonoBehaviour
{
   
    public fodderCannon fodderCannon;
    public GameObject rocket;
    public Sprite half;
    public Sprite empty;
    public float delay;

    public Transform firePoint1;
    public Transform firePoint2;

    SpriteRenderer sRenderer;
    
    int rocketCount;
    

    void Start()
    {
        rocketCount = 2;  
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    
    {
        if(fodderCannon.isReady())
        {
            StartCoroutine(fireRockets());

           

            this.enabled = false;
        }
    }

    IEnumerator fireRockets()
    {
       
        
            Instantiate(rocket,firePoint1.position,firePoint1.rotation);
            sRenderer.sprite = half;

        yield return new WaitForSeconds(delay);

        Instantiate(rocket, firePoint2.position,firePoint2.rotation);
            sRenderer.sprite = empty;
            
        

    }
}
