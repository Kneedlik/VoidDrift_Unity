using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrillSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] List<int> Amounts = new List<int>();

    [SerializeField] float ListClearTimer = 3f;
    float timeStamp;

    public int ExtraDamage;
    public float DamageMultiplier;
    public int MaxAmount;

    // Update is called once per frame
    void Update()
    {
        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (timeStamp <= 0)
        {
            ClearList();
            timeStamp = ListClearTimer;
        }
    }

    public void ClearList()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i] == null)
            {
                Enemies.RemoveAt(i);
                Amounts.RemoveAt(i);
            }
        }
    }

    public void DrillProc(GameObject target, int damage, ref int scaledDamage)
    {
        if(Enemies.Contains(target))
        {
            int i = Enemies.IndexOf(target);
            float damageTemp = ExtraDamage + (damage * DamageMultiplier);
            damageTemp *= Amounts[i];
            scaledDamage = damage + (int)damageTemp;

            if (Amounts[i] < MaxAmount)
            {
                Amounts[i] += 1;
            }
        }else
        {
            scaledDamage = damage;
            Enemies.Add(target);
            Amounts.Add(1);
        }

    }

}
