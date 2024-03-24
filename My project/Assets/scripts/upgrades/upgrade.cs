using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum type
{
    green,
    red,
    yellow,
    purple,
    blue,
    black,
    special,
    iron,
    currupted,
}

//[CreateAssetMenu(fileName = "new upgrade", menuName = "upgrades")]
public abstract class upgrade: MonoBehaviour
{
    public int Id;
    public type Type;
    public Color32 color;
    public Sprite icon;
    public new string name;
    [TextArea(15,20)]
    public string description;
    public int level;
    public int maxLevel;
    public int rarity;

    public virtual void function()
    {
        
    }

    public virtual bool requirmentsMet()
    {
        return true;
    }

     public void setColor()
    {
        if(Type == type.green)
        {
            color = new Color32(0, 255, 45,255);
        }else if(Type == type.red)
        {
            color = new Color32(255,20,0, 255);
        }else if(Type==type.purple)
        {
            color = new Color32(140,0,255, 255);
        }else if(Type == type.yellow)
        {
            color = new Color32(255,255,0,255);
        }else if(Type == type.blue)
        {
            color = new Color32(65, 255, 255, 255);
        }else if(Type == type.special || Type == type.iron)
        {
            color = new Color32(110, 110, 100, 255);
        }else if(Type == type.currupted)
        {
            color = new Color32(255, 0, 85, 255);
        }
    }

    public void cloneSelf()
    {
       GameObject pom = Instantiate(gameObject);
        pom.transform.SetParent(gameObject.transform.parent);
        upgradeList UList = gameObject.transform.GetComponentInParent<upgradeList>();
        if( UList != null )
        {
            UList.addNewUpgrade(pom.GetComponent<upgrade>());
        }   
    }

}


