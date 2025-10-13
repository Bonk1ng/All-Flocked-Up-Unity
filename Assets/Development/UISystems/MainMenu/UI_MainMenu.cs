using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private bool settingsOpen=>OnSettingsOpen();
    [SerializeField] private bool controlsOpen=>OnControlsOpen();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
