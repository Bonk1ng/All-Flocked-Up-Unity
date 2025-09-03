using UnityEngine;

public class UI_CanvasController : MonoBehaviour
{
    [Header("TimerCanvas")]
    [SerializeField] private UI_QuestTimer timerCanvas;
    public UI_QuestTimer activeTimerInstance;

    [Header("QuestGiverCanvas")]
    [SerializeField] private UI_QuestGiver questGiverCanvas;
    public UI_QuestGiver activeGiverInstance;

    [Header("QuestRewardsCanvas")]
    [SerializeField] private UI_QuestReward questRewardsCanvas;
    public UI_QuestReward activeRewardInstance;

    [Header("QuestTrackerCanvas")]
    [SerializeField] private UI_QuestTracker questTrackerCanvas;
    public UI_QuestTracker activeTrackerInstance;

    [Header("QuestNotifCanvas")]
    [SerializeField] private UI_QuestNotif questNotifCanvas;
    public UI_QuestNotif activeNotifInstance;

    [Header("QuestLocationNotifCanvas")]
    [SerializeField] private UI_QuestLocationNotif questLocationNotifCanvas;
    public UI_QuestLocationNotif activeLocationNotifInstance;


    public void ShowPlayerCursor()
    {
        Cursor.visible = true;
    }

    public void HidePlayerCursor()
    {
        Cursor.visible = false;
    }


    public void ShowTimer()
    {
        activeTimerInstance = Instantiate(timerCanvas);

    }

    public void EndTimer()
    {
        if (activeTimerInstance != null)
        {
            activeTimerInstance.DestroyTimer();
            activeTimerInstance = null;

        }

    }

    public void ShowQuestGiver(QuestGiver questGiver)
    {
        activeGiverInstance = Instantiate(questGiverCanvas);
        activeGiverInstance.currentquestGiver = questGiver;
        ShowPlayerCursor() ;
    }

    public void DestroyQuestGiver()
    {
        if (questGiverCanvas != null)
        {
            activeGiverInstance.CloseQuestGiverUI();
            activeGiverInstance = null;
            HidePlayerCursor();
        }
    }

    public void ShowQuestReward()
    {
        activeRewardInstance=Instantiate(questRewardsCanvas);
        ShowPlayerCursor();
    }

    public void DestroyQuestReward()
    {
        if (activeRewardInstance != null)
        {
            activeRewardInstance.AcceptReward();
            activeRewardInstance = null;
            HidePlayerCursor();
        }
    }

    public void ShowTracker()
    {
        activeTrackerInstance = Instantiate(questTrackerCanvas);

    }

    public void DestroyTracker()
    {
        if(activeTrackerInstance != null)
        {
            activeTrackerInstance.RemoveTracker();
            activeTrackerInstance = null;

        }
    }

    //Timed Destroy
    public void ShowQuestNotif(string text)
    {
        if  (activeNotifInstance != null)
        {
            activeNotifInstance.SetNotifText(text);
            return;
        }
        activeNotifInstance = Instantiate(questNotifCanvas);
        activeNotifInstance.SetNotifText(text);
        activeNotifInstance.ShowQuestNotif();
        
    }

    

    public bool OnQuestNotifShown()
    {
        return activeNotifInstance != null && activeNotifInstance.isActiveAndEnabled;
    }

    //Timed Destroy
    public void ShowQuestLocationNotif()
    {
        activeLocationNotifInstance = Instantiate(questLocationNotifCanvas);
    }

    public bool OnQuestLocationNotifShown()
    {
        return activeLocationNotifInstance!=null && activeLocationNotifInstance.isActiveAndEnabled;
    }

}
