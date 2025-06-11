using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeapeonBox : MonoBehaviour
{
    public int WeapeonId;
    public GameObject SelectedObject;
    public bool Unlocked;
    [SerializeField] Image WeapeonImage;
    [SerializeField] GameObject LockedObj;
    [SerializeField] PlayerInformation playerInformation;
    [SerializeField] ProgressionState progressionState;
    [SerializeField] PlayerMenuManager playerMenuManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (progressionState.UnlockedWeapeons.Contains(WeapeonId))
        {
            UnLock();
        }
        else
        {
            Lock();
        }
    }

    public void SelectWeapeon()
    {
        Debug.Log("222");
        if (Unlocked)
        {
            playerMenuManager.DeselectAllWeapeons();
            playerInformation.WeapeonId = WeapeonId;
            Debug.Log(WeapeonId);
            SelectedObject.SetActive(true);
        }
    }

    public void DeSelectWeapeon()
    {
        SelectedObject.SetActive(false);
    }

    public void Lock()
    {
        Unlocked = false;
        LockedObj.SetActive(true);
        WeapeonImage.color = new Color32(255, 255, 255, 155);
    }

    public void UnLock()
    {
        Unlocked = true;
        LockedObj.SetActive(false);
        WeapeonImage.color = new Color32(255, 255, 255, 255);
    }
}
