using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    public List<QuestRuntimeInstance> activeQuests = new();
    public List<QuestDetails> completedQuests = new();
    [SerializeField] private UI_QuestNotif questNotif;
    [SerializeField] private UI_QuestReward questRewardUI;
    public bool hasQuest = false;
    public QuestGiver currentQuestGiver;
    public QuestLogMenuEvents logMenuEvents;
    private bool questTimerStarted;
    public float currentTime;
    [SerializeField] private UI_CanvasController canvasController;


    private void GetIsQuestTimed(QuestDetails quest, QuestRuntimeInstance instance)
    {
        if (quest.isQuestTimed)
        {
            StartQuestTimer(quest.questTime, instance);
            canvasController.ShowTimer();
        }
    }

    private void StartQuestTimer(float questTime, QuestRuntimeInstance instance)
    {
        currentTime = questTime;
        questTimerStarted = true;

    }

    private void CheckTimerState(QuestRuntimeInstance quest)
    {

            if (currentTime == 0f)
            {
                activeQuests.Remove(quest);

                canvasController.EndTimer();
                //quest.QuestFailed();
                //OnQuestFailed(quest);
                //doesn't remove when timer is done?
        }
            else
            {
                Debug.Log("Quest Not Timed");
            }
        } 

    private void Update()
    {
        //needs to check for if quest is completed before timer done
        if (questTimerStarted == true&& currentTime>0) { 
        currentTime -= Time.deltaTime;
            if(currentTime == 0f)
            {
                //chnage later...this can only check against first index.... probably will add timer variables to questdetails so multiple quests can be timed at once or make an enum for quest states
                //Active/Completed/Failed...
                CheckTimerState(activeQuests[0]);
                canvasController.EndTimer();
                
                
            }
            else
            {
                //Debug.Log(currentTime);
            }
    }
    }



    public void AcceptQuest(QuestDetails questData,QuestGiver questGiver)
    {
        if (hasQuest) { return; }
        if (HasQuestOrCompleted(questData))
        {
            Debug.LogWarning($"Quest '{questData.questName}' already accepted or completed.");
            return;
        }

        QuestRuntimeInstance instance = new()
        {
            questData = questData
            
        };
        instance.StartQuest();
        activeQuests.Add(instance);
        hasQuest = true;
        currentQuestGiver = questGiver;
        GetIsQuestTimed(questData, instance);
        
    }

    public void UpdateQuestObjective(string objectiveID, int amount)
    {
        foreach (var quest in activeQuests)
        {
            //throws an error when a quest is complete...stupid
                quest.UpdateObjective(objectiveID, amount);
            
        }

        CheckForCompletedQuests();
    }

    public void OnObjectiveUpdated(QuestRuntimeInstance quest, string objectiveID, int newValue)
    {
        // purely update UI / notify player
        canvasController.ShowQuestNotif("Objective Complete");
        
        Debug.Log($"Quest {quest.questData.questName} objective {objectiveID} progress: {newValue}");
    }

    public void CheckForCompletedQuests()
    {
        for (int i = activeQuests.Count - 1; i >= 0; i--)
        {
            if (activeQuests[i].IsComplete)
            {
                completedQuests.Add(activeQuests[i].questData);
                activeQuests.RemoveAt(i);
                hasQuest = false;
                canvasController.ShowQuestReward();
                Destroy(currentQuestGiver);
                canvasController.EndTimer();

            }
        }
    }

    public bool HasQuest(QuestDetails quest)
    {
        return activeQuests.Exists(q => q.questData == quest);
    }

    public bool HasCompleted(QuestDetails quest)
    {
        return completedQuests.Contains(quest);
    }

    public bool HasQuestOrCompleted(QuestDetails quest)
    {
        return HasQuest(quest) || HasCompleted(quest);
    }

    public bool IsQuestCompleted(QuestDetails quest)
    {
        QuestRuntimeInstance instance = GetQuestInstance(quest);
        return instance != null && instance.IsComplete;
    }

    public QuestRuntimeInstance GetQuestInstance(QuestDetails quest)
    {
        return activeQuests.Find(q => q.questData == quest);
    }

    public void MarkQuestTurnedIn(QuestDetails quest)
    {
        var questInstance = GetQuestInstance(quest);
        if (questInstance != null)
        {
            activeQuests.Remove(questInstance);
            if (!completedQuests.Contains(quest))
                completedQuests.Add(quest);


        }
    }

    public void OnQuestFailed(QuestRuntimeInstance quest)
    {
        if (quest != null)
        {
            activeQuests.Remove(quest);
            
            
        }
    }
}
