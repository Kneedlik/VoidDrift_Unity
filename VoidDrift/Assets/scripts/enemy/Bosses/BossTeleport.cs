using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{
    public bool UseConst = true;
    [SerializeField] float OffsetX;
    [SerializeField] float OffsetY;
    [SerializeField] float Distance;
    float distance;
    Transform Player;
    float TrueOffsetX;
    float TrueOffsetY;
    float TrueTPDistance;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if(UseConst)
        {
            TrueOffsetX = Constants.BossTPOffsetX;
            TrueOffsetY = Constants.BossTPOffsetY;
            TrueTPDistance = Constants.BossTPDistance;
        }else
        {
            TrueOffsetX = OffsetX;
            TrueOffsetY = OffsetY;
            TrueTPDistance = distance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.position, transform.position);

        if(distance > TrueTPDistance)
        {
            Vector3 pos = KnedlikLib.GenerateRandPosition(Player.position, Constants.BossTPOffsetX, Constants.BossTPOffsetY);
            transform.position = pos;
        }
    }
}
