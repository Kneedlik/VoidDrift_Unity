using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamIntegration : MonoBehaviour
{
    public static SteamIntegration instance;


    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        try
        {
            if (Constants.Demo)
            {
                Steamworks.SteamClient.Init(3961670);
                Debug.Log(Steamworks.SteamClient.Name);
            }
            else
            {
                Steamworks.SteamClient.Init(3491090);
                Debug.Log(Steamworks.SteamClient.Name);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
