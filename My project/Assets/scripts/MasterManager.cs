using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
    public PlayerInformation PlayerInformation;
    public ProgressionState progressionState;
    public static MasterManager Instance;
    public bool MapBossKill;
    public bool MapBoss2Kill;
    [SerializeField] GameObject DamageNumber;
    public int GoldEarned;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusicId(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddGold(int Gold, bool MapMultiplier = false)
    {
        float GoldTemp = Gold;
        if (MapMultiplier == false)
        {
            progressionState.Gold += Gold;
            GoldEarned += Gold;
        }else
        {
            GoldTemp = Gold * PlayerInformation.MapGoldMultiplier;
            progressionState.Gold += (int)GoldTemp;
            GoldEarned += (int)GoldTemp;
            Gold = (int)GoldTemp;
        }

        Transform pos = GameObject.FindWithTag("Player").transform;
        if(pos != null)
        {
            TMP_Text Text = Instantiate(DamageNumber,pos.position,Quaternion.Euler(0,0,0)).GetComponent<TMP_Text>();
            Text.text = Gold.ToString();
            Text.color = new Color32(255,175,0,255);
            Text.fontSize = 21;
            DestroyTime DTime = Text.GetComponent<DestroyTime>();
            DTime.destroyTime = DTime.destroyTime * 1.5f;
        }
        Debug.Log(progressionState.Gold);
    }


}
