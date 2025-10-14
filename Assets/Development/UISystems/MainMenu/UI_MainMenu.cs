using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject controlsCanvas;
    private bool settingsOpen=>OnSettingsOpen();
    private bool controlsOpen=>OnControlsOpen();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
    }

    protected bool OnSettingsOpen()
    {
        if (settingsCanvas == null)
        {
            mainCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
            return true;
        }
        else
        {
            mainCanvas.SetActive(!settingsOpen);
            settingsCanvas.SetActive(false);
            return false;
        }
    }

    protected bool OnControlsOpen()
    {
        if (controlsCanvas == null)
        {
            mainCanvas.SetActive(false);
            controlsCanvas.SetActive(true);
            return true;
        }
        else
        {
            mainCanvas.SetActive(!controlsOpen);
            controlsCanvas.SetActive(false);
            return false;
        }
    }
    //maybe link to a game manager/event manager... could help for starting
    protected void StartNewGame()
    {
        Destroy(this.gameObject);
    }

    protected void CheckForSavedGame()
    {

    }

    protected void LoadGame()
    {

    }

    protected void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else        
Application.Quit();
#endif
    }
}
