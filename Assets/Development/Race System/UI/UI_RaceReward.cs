using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RaceReward: MonoBehaviour
{
    [SerializeField] private Button acceptRewardButton;


    [SerializeField] private TextMeshProUGUI raceNameText;
    [SerializeField] private TextMeshProUGUI raceTimeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceptRewardButton.onClick.AddListener(AcceptReward);

    }

    // Update is called once per frame
    private void GetReward()
    {

    }

    private void UpdateRaceBestTime()
    {

    }
    private void AcceptReward()
    {

    }


}
