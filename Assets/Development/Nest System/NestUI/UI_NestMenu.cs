using UnityEngine;
using UnityEngine.UI;

public class UI_NestMenu : MonoBehaviour
{
    [SerializeField] private GameObject dayIcon;
    [SerializeField] private GameObject nightIcon;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button invButton;
    [SerializeField] private Button sleepButton;
    [SerializeField] private Button closeInvButton;
    public UI_CanvasController canvasController;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject invPanel;
    [SerializeField] private S_DayNightCycle dayNightSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        dayNightSystem = FindFirstObjectByType<S_DayNightCycle>();
        canvasController = FindFirstObjectByType<UI_CanvasController>();
    }
    void Start()
    {
        closeButton.onClick.AddListener(CloseNestMenu);
        invButton.onClick.AddListener(OpenNestInventory);
        sleepButton.onClick.AddListener(SleepAtNest);
        closeInvButton.onClick.AddListener(CloseNestInventory);
    }

    private void Update()
    {
        GetCurrentTime();
    }

    private void GetCurrentTime()
    {
        if(dayNightSystem != null&& dayNightSystem.isDay != true)
        {
            nightIcon.SetActive(true);
            dayIcon.SetActive(false);
        }
        else
        {
            nightIcon.SetActive(false);
            dayIcon.SetActive(true);
        }
    }


    private void CloseNestMenu()
    {
        canvasController.CloseNestMenu();
        Destroy(this.gameObject);
    }

    private void OpenNestInventory()
    {
        mainPanel.SetActive(false);
        invPanel.SetActive(true);
    }

    private void CloseNestInventory()
    {
        mainPanel?.SetActive(true);
        invPanel.SetActive(false);
    }

    private void SleepAtNest()
    {
        dayNightSystem.timeOfDay += 0.5f;
    }

}
