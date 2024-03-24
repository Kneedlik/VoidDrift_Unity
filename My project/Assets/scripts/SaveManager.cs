using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager 
{
    public static void SaveState(string Name,StateItem State,bool Encrypt)
    {
        Debug.Log("saving");
        //BinaryFormatter formater = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + Name + ".json";
        //string path = "C:/Test" + "/" + Name + ".json";
        //FileStream stream = new FileStream(path, FileMode.Create);
        if(File.Exists(path))
        {
            File.Delete(path);
        }
        using FileStream stream = File.Create(path);
        stream.Close();

        //StateItem data = State;

        File.WriteAllText(path,JsonConvert.SerializeObject(State));
    }

    public static StateItem LoadState(string Name,bool Encrypted)
    {
        string path = Application.persistentDataPath + "/" + Name + ".json";

        if(File.Exists(path) == false)
        {
            Debug.Log("File doesnt exists");
        }

        StateItem data = JsonConvert.DeserializeObject<StateItem>(File.ReadAllText(path));
        return data;
        
    }
}
