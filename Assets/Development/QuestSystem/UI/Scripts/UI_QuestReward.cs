using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestReward : MonoBehaviour
{

    [SerializeField] GameObject questRewardCanvas;
    [SerializeField] Button acceptReward;
    [SerializeField] TextMeshProUGUI questName;
    [SerializeField] TextMeshProUGUI rewardText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceptReward.onClick.AddListener(AcceptReward);
        questRewardCanvas.SetActive(false);
    }

    public void OpenQuestRewardsUI()
    {
        questRewardCanvas.SetActive(true);
    }
    private void AcceptReward()
    {
        questRewardCanvas.SetActive(false);
    }

    public void SetQuestNameText(string name)
    {
        questName.SetText(name);
    }

    public void SetRewardText(string reward)
    {
        rewardText.SetText(reward);
    }
}
