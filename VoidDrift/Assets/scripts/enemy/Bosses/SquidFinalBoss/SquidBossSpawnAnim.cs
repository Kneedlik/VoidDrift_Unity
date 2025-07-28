using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBossSpawnAnim : MonoBehaviour
{
    [SerializeField] float StartDelay;
    [SerializeField] float FlashDuration;
    [SerializeField] GameObject SpawnAnim;
    [SerializeField] Material flashMaterial;
    [SerializeField] Transform Point;
    Material OldMaterial;
    SquidFinalBoss SquidAI;
    SpriteRenderer spriteRenderer;
    float TimeStamp;
    bool Flashing;

    // Start is called before the first frame update

    private void Awake()
    {
        SquidAI = GetComponent<SquidFinalBoss>();
        SquidAI.enabled = false;
        Flashing = true;
        TimeStamp = FlashDuration;

        //spriteRenderer = GetComponent<SpriteRenderer>();
        //OldMaterial = spriteRenderer.material;
        //spriteRenderer.material = flashMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        } else
        {
            if (Flashing == false)
            {
                SquidAI.enabled = true;
                this.enabled = false;
            }else
            {
                Flashing = false;
                //spriteRenderer.material = OldMaterial;
                Instantiate(SpawnAnim,Point.transform.position,Quaternion.Euler(0,0,0));
                TimeStamp = StartDelay;
            }
        }
    }
}
