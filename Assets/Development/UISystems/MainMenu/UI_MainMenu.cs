using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private UI_CanvasController canvasController;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private Button startButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button quitButton;
    private bool settingsOpen;
    private bool controlsOpen;
    private GameObject playerRef;
    [SerializeField] private Transform playerSpawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        startButton.onClick.AddListener(StartNewGame);
        loadButton.onClick.AddListener(CheckForSavedGame);
        settingsButton.onClick.AddListener(OnSettingsOpen);
        controlsButton.onClick.AddListener(OnControlsOpen);
        quitButton.onClick.AddListener(QuitGame);
        canvasController = FindFirstObjectByType<UI_CanvasController>();
        playerRef = FindFirstObjectByType<PlayerFlightMovement>().gameObject;
        playerRef.transform.position = playerSpawnPoint.transform.position;
        playerRef.transform.rotation  = playerSpawnPoint.transform.rotation;
    }

    protected void OnSettingsOpen()
    {
        if (settingsCanvas == null)
        {
            mainCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
            settingsOpen = true;
  
        }
        else
        {
            mainCanvas.SetActive(!settingsOpen);
            settingsCanvas.SetActive(false);
            settingsOpen = false;
        }
    }

    protected void OnControlsOpen()
    {
        if (controlsCanvas == null)
        {
            mainCanvas.SetActive(false);
            controlsCanvas.SetActive(true);
            controlsOpen = true;
        }
        else
        {
            mainCanvas.SetActive(!controlsOpen);
            controlsCanvas.SetActive(false);
            controlsOpen= false;
        }
    }

    protected void StartNewGame()
    {
        Destroy(this.gameObject);
        canvasController.DestroyMainMenu();
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
