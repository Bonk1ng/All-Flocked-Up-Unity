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

    [Header("QuestLogCanvas")]
    [SerializeField] private UI_QuestLog questLogCanvas;
    public UI_QuestLog activeLogInstance;

    [Header("DialogueCanvas")]
    [SerializeField] private UI_DialogueCanvas dialogueCanvas;
    public UI_DialogueCanvas activeDialogueInstance;


    public void ShowPlayerCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("Cursor Toggle ON");
    }

    public void HidePlayerCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Cursor Toggle OFF");
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
        ShowPlayerCursor();
        activeGiverInstance = Instantiate(questGiverCanvas);
        activeGiverInstance.currentquestGiver = questGiver;
        
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

    public void ShowQuestLog()
    {

            activeLogInstance = Instantiate(questLogCanvas);
            ShowPlayerCursor();

    }

    public void DestroyQuestLog()
    {
        if (activeLogInstance != null)
        {
            activeLogInstance.CloseQuestLog();
            activeLogInstance=null;
            HidePlayerCursor();
        }
    }

    public void OpenDialogue()
    {
        //activeDialogueInstance = Instantiate(dialogueCanvas);
        activeDialogueInstance.gameObject.SetActive(true);

    }

    public void CloseDialogue()
    {
        if(activeDialogueInstance != null || dialogueCanvas.isActiveAndEnabled)
        {
            //activeDialogueInstance.DestroyDialogue();
            activeDialogueInstance.gameObject.SetActive(false);
            activeDialogueInstance=null;
        }
    }

}
