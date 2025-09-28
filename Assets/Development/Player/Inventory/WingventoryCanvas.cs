using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WingventoryCanvas : MonoBehaviour
{
    [SerializeField] private GameObject centerCanvas;
    [SerializeField] private GameObject leftCanvas;
    [SerializeField] private GameObject rightCanvas;


    [SerializeField] private Button leftPageButton;
    [SerializeField] private Button rightPageButton;
    [SerializeField] private Button leftBackPageButton;
    [SerializeField] private Button rightBackPageButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button accessoryPanelButton;
    [SerializeField] private Button mapButton;

    [SerializeField] private PlayerWingventory playerWingventory;
    [SerializeField] private UI_CanvasController canvasController;
    [SerializeField] private AccessoryPanelCanvas accessoryPanelPrefab;

    [SerializeField] private int currentTrinketCount=>GetTrinketCount();
    [SerializeField] private TextMeshProUGUI trinketCountText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasController = FindFirstObjectByType<UI_CanvasController>();
        GetTrinketCount();
        leftBackPageButton.onClick.AddListener(GoCenterPage);
        rightBackPageButton.onClick.AddListener(GoCenterPage);
        leftPageButton.onClick.AddListener(GoLeftPage);
        rightPageButton.onClick.AddListener(GoRightPage);
        closeButton.onClick.AddListener(CloseWingventory);
        accessoryPanelButton.onClick.AddListener(OpenAccessoryPanel);
        mapButton.onClick.AddListener(OpenMap);
        leftCanvas.SetActive(false);
        rightCanvas.SetActive(false);
        SetTrinketText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GoLeftPage()
    {

        leftCanvas.SetActive(true);
        rightCanvas.SetActive(false);
    }

    private void GoRightPage()
    {

        leftCanvas.SetActive(false);
        rightCanvas.SetActive(true);
    }

    private void GoCenterPage()
    {

        leftCanvas.SetActive(false);
        rightCanvas.SetActive(false);
    }

    private void CloseWingventory()
    {
        canvasController.CloseWingventory();
    }

    private int GetTrinketCount()
    {
        int trinketCount= playerWingventory.playerTrinketQuantity;
            return trinketCount;
    }

    private void SetTrinketText()
    {
        trinketCountText.SetText(currentTrinketCount.ToString());
    }

    private void OpenAccessoryPanel()
    {
        Instantiate(accessoryPanelPrefab);
    }


    private void GetCurrentQuestInfo()
    {

    }

    private void OpenMap()
    {

    }

    private void UpdateMapLocation()
    {

    }

    private void CloseMap()
    {

    }
}
