using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCounter : MonoBehaviour
{
    [SerializeField] TMP_Text Text;
    [SerializeField] ProgressionState Progress;
    public bool ShowGoldGained;
    // Start is called before the first frame update

    private void Awake()
    {
        ProgressionState TempP = SaveManager.LoadPlayerProgress();
        if (TempP != null)
        {
            Progress = TempP;
        }

        UpdateCount();
    }

    private void OnEnable()
    {
        UpdateCount();
    }

    public void UpdateCount()
    {
        if (ShowGoldGained == false)
        {
            Text.text = Progress.Gold.ToString();
        }
        else
        {
            Text.text = MasterManager.Instance.progressionState.Gold.ToString();
        }
    }

    public void SetCounter(int Count)
    {
        Text.text = Count.ToString();
    }
}
