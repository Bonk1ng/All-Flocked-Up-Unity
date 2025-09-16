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
    public UI_DialogueCanvas dialogueCanvas;
    public UI_DialogueCanvas activeDialogueInstance;
    public string[] dialogueResponses;

    [Header("TrashCanvas")]
    [SerializeField] private UI_TrashCanvas trashCanvas;
    public UI_TrashCanvas activeTrashInstance;

    [Header("RaceCanvas")]
    [SerializeField] private UI_RaceGiver raceGiverCanvas;
    public UI_RaceGiver raceGiverInstance;

    [SerializeField] private UI_RaceReward raceRewardCanvas;
    public UI_RaceReward raceRewardInstance;

    [SerializeField] private UI_RaceFail raceFailCanvas;
    public UI_RaceFail raceFailInstance;


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

        dialogueCanvas.gameObject.SetActive(true);

    }

    public void SendResponseOptions(string[] responses)
    {
        activeDialogueInstance.responses = responses;
    }

    public string[] GetCurrentResponseOptions()
    {
        return dialogueResponses;
    }

    public void CloseDialogue()
    {
        if(dialogueCanvas != null || dialogueCanvas.isActiveAndEnabled)
        {
            //activeDialogueInstance.DestroyDialogue();
            dialogueCanvas.gameObject.SetActive(false);
        }
    }

    public void ShowTrashPrompt()
    {
       activeTrashInstance= Instantiate(trashCanvas);
        activeTrashInstance.InitCanvas();
        if(activeTrashInstance != null )
        {
            activeTrashInstance.DestroyCanvas();
            activeTrashInstance = null;
        }
    }

    public void OpenRaceGiver()
    {
        raceGiverInstance = Instantiate(raceGiverCanvas);
    }

    public void CloseRaceGiver()
    {
        if(raceGiverInstance != null)
        {
            raceGiverInstance.CloseRaceGiver();
            raceGiverInstance = null;
        }
    }

    public void OpenRaceRewards()
    {
        raceRewardInstance = Instantiate(raceRewardCanvas);
    }

    public void CloseRaceRewards()
    {
        if(raceRewardInstance != null)
        {
            Destroy(raceRewardInstance);
            raceRewardInstance = null;
        }
    }

    public void OpenRaceFail()
    {
        raceFailInstance = Instantiate(raceFailCanvas);
    }

    public void CloseRaceFail()
    {
        if(raceFailInstance != null)
        {
            Destroy(raceFailInstance);
            raceFailInstance = null;
        }
    }

}
