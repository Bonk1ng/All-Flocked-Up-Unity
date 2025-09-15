using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RaceReward: MonoBehaviour
{
    [SerializeField] private Button acceptRewardButton;


    [SerializeField] private TextMeshProUGUI raceNameText;
    [SerializeField] private TextMeshProUGUI raceTimeText;

    [SerializeField] private RaceBase raceBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceptRewardButton.onClick.AddListener(AcceptReward);
        raceBase = FindFirstObjectByType<RaceBase>();
        GetReward();

    }

    // Update is called once per frame
    public void GetReward()
    {

            Debug.Log("RewardInfo");
            raceNameText.SetText(raceBase.raceData.raceName);
            raceTimeText.SetText(raceBase.raceData.raceTime.ToString());
        
    }

    private void UpdateRaceBestTime()
    {

    }
    private void AcceptReward()
    {
        raceBase.completedRaces.Add(raceBase.raceData);
        Destroy(raceBase.currentRaceGiver.GetComponent<RaceGiver>());
        Destroy(this.gameObject);
    }


}
