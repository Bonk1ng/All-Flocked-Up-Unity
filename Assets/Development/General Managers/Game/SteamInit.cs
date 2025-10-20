using Steamworks;
using UnityEngine;

public class SteamInit : MonoBehaviour
{
    private static SteamInit instance;
    private static bool isInitialized = false;

    public static bool Initialized => isInitialized;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        try
        {
            if (SteamAPI.RestartAppIfNecessary((AppId_t)480))
            {
                Application.Quit();
                return;
            }
        }
        catch (System.DllNotFoundException e)
        {
            Debug.LogError("[Steamworks.NET] Could not find steam_api.dll/so/dylib: " + e);
            Application.Quit();
            return;
        }

        isInitialized = SteamAPI.Init();
        if (!isInitialized)
        {
            Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Is Steam running?");
            Application.Quit();
        }
        else
        {
            Debug.Log("[Steamworks.NET] Initialized successfully!");
        }
    }

    private void OnEnable()
    {
        if (isInitialized)
            SteamAPI.RunCallbacks();
    }

    private void Update()
    {
        if (isInitialized)
            SteamAPI.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        if (isInitialized)
        {
            SteamAPI.Shutdown();
            isInitialized = false;
            Debug.Log("[Steamworks.NET] Shutdown complete.");
        }
    }
}
