using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCounter : MonoBehaviour
{
    [SerializeField] TMP_Text Text;
    [SerializeField] ProgressionState Progress;
    // Start is called before the first frame update

    private void Awake()
    {
        Text.text = Progress.Gold.ToString();
    }
}
