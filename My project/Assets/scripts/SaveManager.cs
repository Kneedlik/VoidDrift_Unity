using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    //C:\Users\kryst\AppData\LocalLow\DefaultCompany\My project

    public static void SaveState(string Name, StateItem State, bool Encrypt)
    {
        Debug.Log("saving");
        string path = Application.persistentDataPath + "/Profile" + Name + ".json";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using FileStream stream = File.Create(path);
        stream.Close();

        //StateItem data = State;

        File.WriteAllText(path, JsonConvert.SerializeObject(State));
    }

    public static StateItem LoadState(string Name, bool Encrypted)
    {
        string path = Application.persistentDataPath + "/Profile" + Name + "​.json";

        if (File.Exists(path) == false)
        {
            Debug.Log("File doesnt exists");
            return null;
        }

        StateItem data = JsonConvert.DeserializeObject<StateItem>(File.ReadAllText(path));
        return data;
    }

    public static void SaveSettings(SettingsValues Values)
    {
        string path = Application.persistentDataPath + "/Settings.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using FileStream stream = File.Create(path);
        stream.Close();

        GameSettings Settings = new GameSettings();
        Settings.FullScreen = Values.IsFullScreen;
        Settings.VSync = Values.VSync;
        Settings.MasterVolume = Values.MasterVolume;

        File.WriteAllText(path, JsonConvert.SerializeObject(Settings));
    }

    public static SettingsValues LoadSettings()
    {
        string path = Application.persistentDataPath + "/Settings.json";

        if (File.Exists(path) == false)
        {
            Debug.Log("File doesnt exists");
            return null;
        }

        SettingsValues data = JsonConvert.DeserializeObject<SettingsValues>(File.ReadAllText(path));
        return data;
    }
}
