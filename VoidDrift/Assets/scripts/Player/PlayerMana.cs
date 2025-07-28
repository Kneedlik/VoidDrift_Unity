using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public int maxMana;
    public int mana;
    public healthBar manaBar;
    public float regenTime;
    //delay
    private float timestamp = 0f;
    public float regenDelay;
    

    
    // Start is called before the first frame update
    void Start()
    {
        mana = maxMana;
        manaBar.SetMaxHealth(maxMana);
        InvokeRepeating("Regeneration", 0.0f, 1.0f / regenTime);
    }

    public void LowerMana(int damage)
    {
        mana -= damage;
        manaBar.SetHealth(mana);
        timestamp = Time.time;


        if(mana < 0)
        {
            mana = 0;
        }
    }

    void Regeneration()
    {
        if (mana < maxMana && Time.time > (timestamp + regenDelay) )
        {
            mana++;
            manaBar.SetHealth(mana);
        }

    }
}
