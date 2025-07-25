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
            Text.text = MasterManager.Instance.GoldEarned.ToString();
        }
    }
}
