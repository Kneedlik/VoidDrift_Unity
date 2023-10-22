using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeSorting : MonoBehaviour
{
    public List<upgradeDisplay> cards = new List<upgradeDisplay>();
    public List<upgrade> list = new List<upgrade>();
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
                total += list[i].rarity;
                pom[i + 1] = total;
            }

            rand = Random.Range(0, total);
          //  Debug.Log(rand);
          //  Debug.Log(total);

            for (int i = 0; i < list.Count; i++)
            {
                if (rand > pom[i] && rand <= pom[i+1])
                {
                    cards[j].Upgrade = list[i];
                    
                    list.Remove(list[i]);
                    //Debug.Log("vyslo");
                }                                       
            }
            // cards[j].Upgrade = list[0];
            Button button = cards[j].GetComponent<Button>();
            button.interactable = false;
            button.interactable = true;
            StartCoroutine(DissAbleCards());

        }

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
            amount = 3;
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
        list = new List<upgrade>();
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
                if (Base.list[i].Type == type.special && Base.list[i].requirmentsMet())
                {
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
                        if (Base.list[i].Type == PlayerStats.sharedInstance.ownedColours[j])
                        {
                            colourRequirmentMet = true;
                        }
                    }
                }
                else colourRequirmentMet = true;

                if (Base.list[i].level < Base.list[i].maxLevel || Base.list[i].maxLevel == 0)
                {
                    if (Base.list[i].requirmentsMet() && colourRequirmentMet && Base.list[i].Type != type.special)
                    {
                        list.Add(Base.list[i]);
                    }
                }
            }
        }
    }
}
