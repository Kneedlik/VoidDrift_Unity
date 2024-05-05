using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AC_Rockets : upgrade
{
    public static AC_Rockets instance;


    public GameObject Prefab;
    public int Amount;
    public int Force;
    public int AmountIncrease;

    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] Transform Parent;
    bool Switch;

    [SerializeField] GameObject Cube;
    [SerializeField] GameObject[] Cubes = new GameObject[100];

    [SerializeField] int CurrentShot;
    [SerializeField] int ShotsNeeded;

    [SerializeField] float BaseOffset;

   //  public override bool requirmentsMet()
   //  {
    //     if(levelingSystem.instance.level >= 10)
    //     {
    //         return true;
    //     }else return false;
    // }

    private void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        if (level == 0)
        {
            eventManager.OnFire += FireRockets;
            description = string.Format("Homing missles + 2");
            //Amount += AmountIncrease;
        }
        Amount += AmountIncrease;
        SetFirePoints();

        level++;
    }

    public void FireRockets(GameObject Weapeon)
    {
        CurrentShot++;
       // AutoCannon AC = GameObject.FindWithTag("Weapeon").GetComponent<AutoCannon>();

        if (CurrentShot >= ShotsNeeded )
        {
            for (int i = 0; i < Amount; i++)
            {
                GameObject Rocket;
               
                Rocket = Instantiate(Prefab, Cubes[i].transform.position, Cubes[i].transform.rotation);
                Rigidbody2D rb = Rocket.GetComponent<Rigidbody2D>();
                rb.AddForce(Cubes[i].transform.up * Force);
            }
            CurrentShot = 0;
        }

    }

    public void SetFirePoints()
    {
        Transform Player = GameObject.FindWithTag("Player").transform;
        Player.rotation = Quaternion.Euler(0, 0, 0);

        float Offset = BaseOffset / Amount;
        float Temp = Offset;

        for (int i = 0;i < Amount;i++)
        {
           // if (Cubes[i] != null)
           // {
                Destroy(Cubes[i]);
           // }
        }

        for (int i = 0;i < Amount ; )
        {
            Cubes[i] = Instantiate(Cube,pos1.position,Quaternion.Euler(0,0,Temp * -1 + 270));
            Cubes[i].transform.parent = Parent;
            i++;

            Cubes[i] = Instantiate(Cube, pos2.position, Quaternion.Euler(0,0,Temp + 270));
            Cubes[i].transform.parent = Parent;
            i++;

            Temp += Offset;
        }
    }

}
