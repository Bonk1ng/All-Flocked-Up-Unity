using UnityEngine;
using UnityEngine.UI;

public class UI_PauseMenu : MonoBehaviour
{
    [SerializeField] protected GameObject mainCanvas;
    [SerializeField] protected GameObject settingsCanvas;
    [SerializeField] protected GameObject controlsCanvas;
    [SerializeField] protected Button settingsButton;
    [SerializeField] protected Button controlsButton;
    [SerializeField] protected Button saveQuitButton;
    [SerializeField] protected bool settingsOpen;
    [SerializeField] protected bool controlsOpen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        settingsButton.onClick.AddListener(OnSettingsOpen);
        controlsButton.onClick.AddListener(OnControlsOpen);
        saveQuitButton.onClick.AddListener(OnSaveAndQuit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnSettingsOpen()
    {
        if (!settingsOpen)
        {
            settingsOpen = true;
            mainCanvas.SetActive(false);
            settingsCanvas.SetActive(true);

        }
        else
        {
            settingsOpen = false;
            mainCanvas.SetActive(true);
            settingsCanvas.SetActive(false);

        }
    }

    protected virtual void OnControlsOpen()
    {
        if (!controlsOpen)
        {
            controlsOpen = true;
            mainCanvas.SetActive(false);
            controlsCanvas.SetActive(true);

        }
        else
        {
            controlsOpen= false;
            mainCanvas.SetActive(true);
            controlsCanvas.SetActive(false);

        }
    }


    public void ClosePauseUI()
    {
        Destroy(this.gameObject);
    }

    protected void OnSaveAndQuit()
    {

    }
}
