using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class RuneBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int Id;
    public bool Unlocked;
    public bool IsUsed;
    public bool KeyStone;
    [SerializeField] GameObject LockedObj;
    [SerializeField] GameObject DescriptionObj;
    [SerializeField] Image UsedImage;
    public TMP_Text Description;
    public Image Icon;

    // Start is called before the first frame update
    void Start()
    {
        UsedImage.sprite = Icon.sprite;
        //LoadFromProggression();
    }

    public void LoadFromProggression()
    {
        LoadDescription();
        if(PlayerMenuManager.instance.progressionState.UnlockedRunes.Contains(Id) || PlayerMenuManager.instance.progressionState.UnlockedKeyStones.Contains(Id))
        {
            Unlock();
        }else Lock();
    }

    public void Unlock()
    {
        LockedObj.SetActive(false);
        Unlocked = true;
    }

    public void Lock()
    {
        LockedObj.SetActive(true);
        Unlocked = false;
    }

    public void LoadDescription()
    {
        switch(Id)
        {
            case RuneConst.CritChance1: Description.text = string.Format("Crit chance + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.CritChance2: Description.text = string.Format("Crit chance + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.CritDamage1: Description.text = string.Format("Crit damage + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.CritDamage2: Description.text = string.Format("Crit damage + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.DivineShield: Description.text = string.Format("You start game with Divine Shield upgrade");
                break;
            case RuneConst.HealthDamage: Description.text = string.Format("+ {0}% damage for every bonus health", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.OneSummonBonus: Description.text = string.Format("Summon Damage + {0}% if you have only 1 Summon", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.DroneFireRate: Description.text = string.Format("Summon Fire rate + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.StatusDamage1: Description.text = string.Format("Damage of status effects + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.StatusDamage2: Description.text = string.Format("Damage of status effects + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.StatusTick: Description.text = string.Format("Cooldown of status effects + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.SummonAndDamage: Description.text = string.Format("Damage + {0}% Summon Damage + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.KillSpeed: Description.text = string.Format("After killing an enemy gain + {0}% movement soeed for 0.5 seconds", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.ExtraDamage: Description.text = string.Format("Extra Damage + {0}", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.OrbDropRate: Description.text = string.Format("Chance enemies drop Healing Orb + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.Omega: Description.text = string.Format("Damage + {0}% Area + {0}% Attack speed + {0}% Max health + {0}%", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.Phoenix: Description.text = string.Format("After reviving gain + {0}% damage + {1}% max health + {0}% fireRate Revives + 1",RuneConst.GetRuneIncrease(Id),RuneConst.GetRuneIncrease(Id) * 2);
                break;
            case RuneConst.LethalTempo: Description.text = string.Format("First time you strike an enemy fire projectile dealing {0}% of the initial damage",RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.NearDamage: Description.text = string.Format("Ememies near you take + {0}% more Damage from all sources",RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.NewStatus: Description.text = string.Format("Apply deadly poison on hit Poisoned enemies take {0}% max health damage periodicaly", RuneConst.GetRuneIncrease(Id));
                break;
            case RuneConst.BlackHoles: Description.text = string.Format("Summon multiple black holes pulling in enemies");
                break;
            case RuneConst.Lasers: Description.text = string.Format("Periodicaly fire lasers dealing damage and applying Brittle");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Unlocked)
        {
            DescriptionObj.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionObj.SetActive(false);
    }

    public void Pick()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        if (IsUsed)
        {
            return;
        }

        if (Unlocked == false)
        {
            return;
        }

        if (PlayerMenuManager.instance.SelectedBox == 1 && KeyStone == false)
        {
            return;
        }

        if(PlayerMenuManager.instance.SelectedBox > 1 && KeyStone)
        {
            return;
        }

        int DesiredBox = 0;
        if(PlayerMenuManager.instance.SelectedBox != 0)
        {
            DesiredBox = PlayerMenuManager.instance.SelectedBox;
        }else
        {
            for(int i = 0; i < PlayerMenuManager.instance.EquipedRuneBoxes.Count; i++)
            {
                if (PlayerMenuManager.instance.EquipedRuneBoxes[i].Filled == false && PlayerMenuManager.instance.EquipedRuneBoxes[i].Unlocked)
                {
                    if(KeyStone && PlayerMenuManager.instance.EquipedRuneBoxes[i].KeyStone)
                    {
                        DesiredBox = PlayerMenuManager.instance.EquipedRuneBoxes[i].SlotId;
                        break;
                    }else if (KeyStone == false && PlayerMenuManager.instance.EquipedRuneBoxes[i].KeyStone == false)
                    {
                        DesiredBox = PlayerMenuManager.instance.EquipedRuneBoxes[i].SlotId;
                        break;
                    }
                }
            }
        }

        for(int i = 0; i < PlayerMenuManager.instance.EquipedRuneBoxes.Count;i++)
        {
            if (PlayerMenuManager.instance.EquipedRuneBoxes[i].SlotId == DesiredBox)
            {
                StartUsing();
                PlayerMenuManager.instance.EquipedRuneBoxes[i].Icon.gameObject.SetActive(true);
                PlayerMenuManager.instance.EquipedRuneBoxes[i].Icon.sprite = Icon.sprite;
                PlayerMenuManager.instance.EquipedRuneBoxes[i].Icon.color = Icon.color;
                PlayerMenuManager.instance.EquipedRuneBoxes[i].DescriptionText.text = Description.text;

                if (PlayerMenuManager.instance.EquipedRuneBoxes[i].EquipedId != 0 && PlayerMenuManager.instance.playerInformation.EquippedRunes.Contains(PlayerMenuManager.instance.EquipedRuneBoxes[i].EquipedId))
                {
                    PlayerMenuManager.instance.playerInformation.EquippedRunes.Remove(PlayerMenuManager.instance.EquipedRuneBoxes[i].EquipedId);
                }

                for (int j = 0; j < PlayerMenuManager.instance.RuneBoxes.Count; j++)
                {
                    if (PlayerMenuManager.instance.RuneBoxes[j].Id == PlayerMenuManager.instance.EquipedRuneBoxes[i].EquipedId)
                    {
                        PlayerMenuManager.instance.RuneBoxes[j].StopUsing();
                    }
                }

                //nahradit

                PlayerMenuManager.instance.EquipedRuneBoxes[i].EquipedId = Id;
                PlayerMenuManager.instance.EquipedRuneBoxes[i].Filled = true;
                if (PlayerMenuManager.instance.playerInformation.EquippedRunes.Contains(Id) == false)
                {
                    if (KeyStone)
                    {
                        PlayerMenuManager.instance.playerInformation.Keystone = Id;
                    }
                    else
                    {
                        PlayerMenuManager.instance.playerInformation.EquippedRunes.Add(Id);
                    }
                }
            }
        }
    }

    public void StartUsing()
    {
        IsUsed = true;
        UsedImage.gameObject.SetActive(true);
    }

    public void StopUsing()
    {
        IsUsed = false;
        UsedImage.gameObject.SetActive(false);
    }
}
