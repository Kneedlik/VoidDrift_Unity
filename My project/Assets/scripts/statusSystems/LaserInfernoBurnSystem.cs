using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInfernoBurnSystem : MonoBehaviour
{
    public Dictionary<GameObject,StackableStatus> BurningEnemies = new Dictionary<GameObject,StackableStatus>();
    List<GameObject> TempList = new List<GameObject>();
    [SerializeField] int MaxValue;
    [SerializeField] GameObject VfxPrefab;
    public float Duration;
    public float CoolDown;
    [SerializeField] int Damage;
    [SerializeField] float TrueDamage;
    //public bool active;

    public void Update()
    {
        foreach (KeyValuePair<GameObject,StackableStatus> item in BurningEnemies)
        {
            if (item.Value.TimeLeft > 0)
            {
                item.Value.TimeLeft -= Time.deltaTime;
            }

            if(item.Key == null)
            {
                TempList.Add(item.Key);
            }else if(item.Key.activeInHierarchy && item.Value.TimeLeft <= 0)
            {
                Destroy(item.Value.Vfx);
                TempList.Add(item.Key);
            }
        }

        for (int i = 0; i < TempList.Count; i++)
        {
            BurningEnemies.Remove(TempList[i]);
        }
        TempList.Clear();
    }

    public void Burn(GameObject target, int damage, ref int Damage)
    {
        if (target == null)
        {
            return;
        }

        if (BurningEnemies.ContainsKey(target))
        {
            if (BurningEnemies[target].Amount < MaxValue)
            {
                BurningEnemies[target].Amount = BurningEnemies[target].Amount + 1;
                BurningEnemies[target].TimeLeft = Duration;
            } 
        }else
        {
            GameObject Obj = Instantiate(VfxPrefab, target.transform.position, Quaternion.Euler(0, 0, 0));
            Obj.transform.SetParent(target.transform);
            BurningEnemies.Add(target, new StackableStatus(1,Duration,Obj));
            StartCoroutine(StarnBurn(target));
        }
    }

    IEnumerator StarnBurn(GameObject target)
    {
        //Debug.Log("111");
        while(target != null && target.activeInHierarchy && BurningEnemies.ContainsKey(target))
        {
            //Debug.Log("222");
            float TempDamage;
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                //Debug.Log("333");
                TempDamage = (BurningEnemies[target].Amount / 2) * Damage;
                if (BurningEnemies[target].Amount == MaxValue)
                {
                    TempDamage += target.GetComponent<Health>().maxHealth * TrueDamage;
                }
                health.TakeDamage((int)TempDamage);
            }
            yield return new WaitForSeconds(CoolDown);
        }
    }
}
