using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrail : upgrade
{
    public GameObject PoisonTrailObject;
    Transform player;
   

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Type = type.green;
        setColor();


    }

   // public override bool requirmentsMet()
   // {
   //     if (poisonOnHit.instance.level >= 3)
   //     {
   //         return true;
   //     }
   //     else return false;
   // }


    public override void function()
    {
        if(level == 0)
        {
            GameObject T = Instantiate(PoisonTrailObject, new Vector3(player.position.x - 5,player.position.y,player.position.z),Quaternion.Euler(0,-90,0));
            player.rotation = Quaternion.Euler(0, 0, 0);
            T.transform.SetParent(player,true);
         
        }

        level++;
    }

    
}
