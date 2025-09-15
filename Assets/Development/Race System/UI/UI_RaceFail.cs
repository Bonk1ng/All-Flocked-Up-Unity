using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RaceFail : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private TextMeshProUGUI raceNameText;
    [SerializeField] private TextMeshProUGUI raceTimeText;
    [SerializeField] private TextMeshProUGUI raceRequiredTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        retryButton.onClick.AddListener(RetryRace);
        cancelButton.onClick.AddListener(CloseRace);

    }
    private void GetRequiredTime()
    {

    }

    private void RetryRace()
    {

    }

    private void CloseRace()
    {

    }
}
