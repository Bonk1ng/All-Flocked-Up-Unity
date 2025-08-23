using UnityEngine;
using UnityEngine.UI;

public class UI_QuestReward : MonoBehaviour
{

    [SerializeField] GameObject questRewardCanvas;
    [SerializeField] Button acceptReward;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceptReward.onClick.AddListener(AcceptReward);
        questRewardCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenQuestRewardsUI()
    {
        questRewardCanvas.SetActive(true);
    }
    private void AcceptReward()
    {
        questRewardCanvas.SetActive(false);
    }
}
