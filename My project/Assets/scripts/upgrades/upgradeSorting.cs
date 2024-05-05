using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeSorting : MonoBehaviour
{
    public List<upgradeDisplay> cards = new List<upgradeDisplay>();
    public List<UpgradePlus> list = new List<UpgradePlus> ();
    public upgradeList Base;
     
    int rand;
    int total;
    public int[] pom = new int[100];
    int j;
   
    public void setUpCards()
    {
        upgradeFilter();
        int numberOfCards = decideCards();

        for (j = 0; j < numberOfCards; j++)
        {
            total = 0;
            pom[0] = 0;
            cards[j].gameObject.SetActive(true);

            if(levelingSystem.instance.level % 10 == 0)
            {
                cards[j].Insignia.enabled = true;
            }else cards[j].Insignia.enabled = false;

            for (int i = 0; i < list.Count; i++)
            {
                total += list[i].TrueRarity;
                pom[i + 1] = total;
            }

            rand = Random.Range(0, total);

            for (int i = 0; i < list.Count; i++)
            {
                if (rand > pom[i] && rand <= pom[i+1])
                {
                    cards[j].Upgrade = list[i].upgrade;
                    int Temp = Base.list.IndexOf(list[i]);
                    if (Base.list[Temp].ApearedInRow < 3)
                    {
                        Base.list[Temp].ApearedInRow++;
                    }
                    list.Remove(list[i]);
                    //Debug.Log("vyslo");
                }                                       
            }
            Button button = cards[j].GetComponent<Button>();
            button.interactable = false;
            button.interactable = true;
            StartCoroutine(DissAbleCards());

        }

        //for (int i = 0; i < Base.list.Count; i++)
        //{
        //    for (int j = 0; j < list.Count; j++)
        //    {
        //        if (Base.list[i].upgrade == list[j].upgrade)
        //        {
        //            Base.list[i].ApearedInRow = 0;
        //           if (Base.list[i].NotApeared < 4)
        //            {
        //                Base.list[i].NotApeared += 1;
        //            }
        //        }
        //    }
        //}

    }

    IEnumerator DissAbleCards()
    {
        Button button;
        for (int i = 0; i < cards.Count; i++)
        {
            button = cards[i].GetComponent<Button>();
            button.interactable = false;
        }

        yield return new WaitForSecondsRealtime(0.4f);

        for (int i = 0; i < cards.Count; i++)
        {
            button = cards[i].GetComponent<Button>();
            button.interactable = true;
        }

    }

    int decideCards()
    {
        int amount;

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].gameObject.SetActive(false);
        }

        if (levelingSystem.instance.level % 10 == 0)
        {
            amount = 4;
        }
        else
        {
            amount = 4;
        }

        if(amount % 2 == 1)
        {
            cards[0].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 1);
            cards[1].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, 0,1);
            cards[2].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-200, 0, 1);
            cards[3].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(400, 0, 1);
            cards[4].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-400, 0, 1);  
        }else
        {
            cards[0].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(100, 0, 1);
            cards[1].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-100, 0, 1);
            cards[2].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(300, 0, 1);
            cards[3].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300, 0, 1);
        }
        return amount;
    }

    void upgradeFilter()
    {
        list = new List<UpgradePlus>();
        bool coloursfull = true;
        bool colourRequirmentMet;

        if(PlayerStats.sharedInstance.ownedColours.Count < 3)
        {
            coloursfull = false;
        }

        for (int i = 0; i < Base.list.Count; i++)
        {
            if (levelingSystem.instance.level % 10 == 0)
            {
                if (Base.list[i].upgrade.Type == type.special && Base.list[i].upgrade.requirmentsMet())
                {
                    Base.list[i].TrueRarity = Base.list[i].upgrade.rarity;
                    list.Add(Base.list[i]);
                }
            }
            else
            {
                colourRequirmentMet = false;
                if (coloursfull)
                {
                    for (int j = 0; j < PlayerStats.sharedInstance.ownedColours.Count; j++)
                    {
                        if (Base.list[i].upgrade.Type == PlayerStats.sharedInstance.ownedColours[j])
                        {
                            colourRequirmentMet = true;
                        }
                    }
                }
                else colourRequirmentMet = true;

                if (Base.list[i].upgrade.level < Base.list[i].upgrade.maxLevel || Base.list[i].upgrade.maxLevel == 0)
                {
                    if (Base.list[i].upgrade.requirmentsMet() && colourRequirmentMet && Base.list[i].upgrade.Type != type.special)
                    {
                        Base.list[i] = KnedlikLib.CalculateTrueRarity(Base.list[i]);
                        list.Add(Base.list[i]);
                    }
                }
            }
        }
    }

    public void PunishNotChoosen(upgrade Upgrade)
    {
        for(int i = 0;i < cards.Count; i++)
        {
            if (cards[i] != null)
            {
                for (int j = 0; j < Base.list.Count; j++)
                {
                    if ((cards[i].Upgrade == Base.list[j].upgrade))
                    {
                        if (cards[i].Upgrade != Upgrade)
                        {
                            if (Base.list[j].NotChoosen < 2)
                            {
                                Base.list[j].NotChoosen++;
                                break;
                            }
                        }else
                        {
                            Base.list[j].NotChoosen = 0;
                        }
                        Base.list[j].NotApeared = 0;
                    }
                }
            }
              
        }
    }
}
