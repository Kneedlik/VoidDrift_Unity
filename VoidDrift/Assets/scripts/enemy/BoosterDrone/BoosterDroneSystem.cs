using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BoosterDroneSystem : MonoBehaviour
{
    class BoostedUnit
    {
        public Health health;
        public EnemyFollow Follow;

        public BoostedUnit(Health Ahealth, EnemyFollow AFollow)
        {
            health = Ahealth;
            Follow = AFollow;
        }
    }

    public static BoosterDroneSystem instance;

    Dictionary<GameObject,BoostedUnit> BoostedUnits = new Dictionary<GameObject, BoostedUnit>();
    [SerializeField] float SpeedIncrease;
    [SerializeField] float HealthIncrease;

    private void Start()
    {
        instance = this;
    }

    public void BoostUnit(GameObject Target)
    {
        if (Target == null || Target.tag != "Enemy") return;

        if (BoostedUnits.ContainsKey(Target) == false)
        {
            EnemyFollow Follow = Target.GetComponent<EnemyFollow>();
            Health health = Target.GetComponent<Health>();
            if (Follow != null && health != null)
            {
                BoostedUnits.Add(Target,new BoostedUnit(health,Follow));
                Follow.Multiplier += SpeedIncrease;
                health.multiplier -= HealthIncrease;
                
            }
        }
    }

    public void RemoveUnit(GameObject Target)
    {
        if (Target == null || Target.tag != "Enemy") return;

        if (BoostedUnits.ContainsKey(Target))
        {
            BoostedUnits[Target].Follow.Multiplier -= SpeedIncrease;
            BoostedUnits[Target].health.multiplier += HealthIncrease;
            BoostedUnits.Remove(Target);
            
        }
    }
}
