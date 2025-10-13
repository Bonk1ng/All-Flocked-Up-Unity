using UnityEngine;
using UnityEngine.UI;

public class UI_SettingsMenu : UI_PauseMenu
{
    [Header("Settings")]
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private Button videoButton;
    [SerializeField] private Button audioButton;
    [SerializeField] private Button accessButton;

    [SerializeField] private GameObject videoParent;
    [SerializeField] private GameObject audioParent;
    [SerializeField] private GameObject accessParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeSettingsButton.onClick.AddListener(OnSettingsOpen);
        videoButton.onClick.AddListener(OpenVideoOptions);
        audioButton.onClick.AddListener(OpenAudioOptions);
        accessButton.onClick.AddListener(OpenAccessOptions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected new void OnSettingsOpen()
    {
        base.OnSettingsOpen();
    }

    protected void OpenVideoOptions()
    {

    }

    protected void OpenAudioOptions()
    {

    }

    protected void OpenAccessOptions()
    {

    }
}
