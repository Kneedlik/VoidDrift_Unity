using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHolesSystem : MonoBehaviour
{
    public static BlackHolesSystem instance;
    public int BulletsInBurst;
    public float CoolDown;
    public GameObject BlackHoleObj;
    public float MaxX;
    public float MaxY;
    public float MinX;
    public float MinY;
    float TimeStamp;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        enabled = false;
    }

    private void Start()
    {
        PlayerStats.OnLevel += Scale;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if(TimeStamp <= 0)
        {
            Fire();
            TimeStamp = CoolDown;
        }
    }

    public Vector3 GenerateRandomPos()
    {
        float XTemp = Random.Range(MaxX * -1, MaxX);
        float YTemp = Random.Range(MaxY * -1, MaxY);
        int index = 0;
        while(XTemp < MinX && XTemp > MinX * -1)
        {
            if(index > 100)
            {
                break;
            }
            XTemp = Random.Range(MaxX * -1, MaxX);
        }

        while (YTemp < MinY && YTemp > MinY * -1)
        {
            if (index > 100)
            {
                break;
            }
            YTemp = Random.Range(MaxY * -1, MaxY);
        }

        Vector3 pos = new Vector3(XTemp,YTemp,0);
        return pos;
    }

    public void Fire()
    {
        for (int i = 0; i < BulletsInBurst; i++)
        {
            Vector3 pos = GenerateRandomPos();
            GameObject G = Instantiate(BlackHoleObj, pos, Quaternion.Euler(0, 0, 0));

            float size = 1f * (PlayerStats.sharedInstance.areaMultiplier / 100f);
            size = size * MasterManager.Instance.PlayerInformation.SizeMultiplier;
            G.transform.localScale = new Vector3(size, size, 1f);
        }
    }

    public void Scale()
    {
        if(levelingSystem.instance.level == 20)
        {
            BulletsInBurst = 3;
            CoolDown = CoolDown * 0.75f;
        }else if(levelingSystem.instance.level == 40)
        {
            BulletsInBurst = 5;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(MaxX * 2,MaxY * 2,0));
        Gizmos.DrawWireCube(transform.position, new Vector3(MinX * 2, MinY * 2, 0));
    }


}
