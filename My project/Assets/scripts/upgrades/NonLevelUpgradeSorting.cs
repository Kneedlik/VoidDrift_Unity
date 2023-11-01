using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonLevelUpgradeSorting : MonoBehaviour
{
    public List<upgradeDisplay> cards = new List<upgradeDisplay>();
    public List<upgrade> list = new List<upgrade>();

    int rand;
    int total;
    public int[] pom = new int[100];
    int j;

    public int BaseAmount;

    public void setUpCards()
    {
        upgradeFilter();
        int numberOfCards = decideCards();

        for (j = 0; j < numberOfCards; j++)
        {
            total = 0;
            pom[0] = 0;
            cards[j].gameObject.SetActive(true);
            cards[j].Insignia.enabled = false;

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
                if (rand > pom[i] && rand <= pom[i + 1])
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
        int amount = BaseAmount;

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].gameObject.SetActive(false);
        }

        if (amount % 2 == 1)
        {
            cards[0].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 1);
            cards[1].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, 0, 1);
            cards[2].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-200, 0, 1);
            cards[3].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(400, 0, 1);
            cards[4].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-400, 0, 1);
        }
        else
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

        foreach (Transform asset in transform)
        {
            upgrade Upgrade = asset.GetComponent<upgrade>();
            if(Upgrade.requirmentsMet())
            {
                list.Add(Upgrade);
            }
        }
    }
}
