using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ControlsMenu : UI_PauseMenu
{
    [Header("Controls")]
    [SerializeField] private Button closeControlsButton;
    [SerializeField] private Slider mouseSensSlider;
    [SerializeField] private TextMeshProUGUI meshSensText;
    [SerializeField] private Slider controllerSensSlider;
    [SerializeField] private TextMeshProUGUI controlSensText;
    [SerializeField] private GameObject remapParent;
    [SerializeField] private RectTransform remapBox;
    [SerializeField] private Button confirmRemapButton;
    [SerializeField] private Button cancelRemapButton;
    [SerializeField] private TMP_Dropdown ControlImageDropdown;
    [SerializeField] private Button rebindButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeControlsButton.onClick.AddListener(OnControlsOpen);
        confirmRemapButton.onClick.AddListener(ConfirmRemap);
        cancelRemapButton.onClick.AddListener(CancelRemap);
        rebindButton.onClick.AddListener(OpenRemap);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected new void OnControlsOpen()
    {
        base.OnControlsOpen();
    }

    protected void ConfirmRemap()
    {

    }

    protected void CancelRemap()
    {

    }

    protected void OpenRemap()
    {

    }
}
