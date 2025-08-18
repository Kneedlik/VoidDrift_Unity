using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    //C:\Users\kryst\AppData\LocalLow\DefaultCompany\My project

    public static void SaveLog(string Name, List<string> StringList)
    {
        string path = Application.persistentDataPath + "/" + Name + ".json";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using FileStream stream = File.Create(path);
        stream.Close();

        File.WriteAllText(path, JsonConvert.SerializeObject(StringList));
    }

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

        //Debug.Log("File exists");
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

        File.WriteAllText(path, JsonConvert.SerializeObject(Values));
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

    public static void SavePlayerProgress(ProgressionState Progress)
    {
        string path = Application.persistentDataPath + "/Progress.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using FileStream stream = File.Create(path);
        stream.Close();

        File.WriteAllText(path, JsonConvert.SerializeObject(Progress));
    }

    public static ProgressionState LoadPlayerProgress()
    {
        string path = Application.persistentDataPath + "/Progress.json";

        if (File.Exists(path) == false)
        {
            Debug.Log("File doesnt exists");
            return null;
        }

        ProgressionState data = JsonConvert.DeserializeObject<ProgressionState>(File.ReadAllText(path));
        return data;
    }

    public static void SavePlayerPrefs(PlayerPrefs Preffs)
    {
        string path = Application.persistentDataPath + "/Prefs.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using FileStream stream = File.Create(path);
        stream.Close();

        File.WriteAllText(path, JsonConvert.SerializeObject(Preffs));
    }

    public static PlayerPrefs LoadPlayerPrefs()
    {
        string path = Application.persistentDataPath + "/Prefs.json";

        if (File.Exists(path) == false)
        {
            Debug.Log("File doesnt exists");
            return null;
        }

        PlayerPrefs data = JsonConvert.DeserializeObject<PlayerPrefs>(File.ReadAllText(path));
        return data;
    }
}
