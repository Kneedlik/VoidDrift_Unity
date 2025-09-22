using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class VirtualMouseManager : MonoBehaviour
{
    public static VirtualMouseManager instance;
    [SerializeField] GameObject VirtualMouse;
    [SerializeField] SettingsValues Settings;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SettingsValues TempValues = SaveManager.LoadSettings();
        if(TempValues != null)
        {
            Settings = TempValues;
        }

        Debug.Log(Gamepad.all.Count);
        ControlsChange(Settings.UseKeyboard);
    }

    private void Start()
    {
        //SettingsValues TempValues = SaveManager.LoadSettings();
        //Debug.Log(Gamepad.all.Count);
        //ControlsChange(TempValues.UseKeyboard);
    }

    public void ControlsChange(bool UseKeyboard)
    {
        if (UseKeyboard || Gamepad.all.Count == 0)
        {
            VirtualMouse.SetActive(false);
        }
    }
}
