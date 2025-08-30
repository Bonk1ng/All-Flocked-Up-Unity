using System;
using System.Collections.Generic;
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
    private float currentTime;


    private void GetIsQuestTimed(QuestDetails quest, QuestRuntimeInstance instance)
    {
        if (quest.isQuestTimed)
        {
            StartQuestTimer(quest.questTime, instance);
        }
    }

    private void StartQuestTimer(float questTime, QuestRuntimeInstance instance)
    {
        currentTime = questTime;
        questTimerStarted = true;

    }

    private void CheckTimerState()
    {
        for (int i = activeQuests.Count - 1; i >= 0; i--)
        {
            var questInstance = activeQuests[i];
            if (currentTime == 0f)
            {
                activeQuests.Remove(questInstance);
                //doesn't remove when timer is done?
            }
            else
            {
                Debug.Log("Quest Not Timed");
            }
        } }

    private void Update()
    {
        if (questTimerStarted == true) { 
        currentTime -= Time.deltaTime;
            if(currentTime == 0f)
            {
                CheckTimerState();
            }
            else
            {
                Debug.Log(currentTime);
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

        QuestRuntimeInstance instance = new QuestRuntimeInstance
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
            quest.UpdateObjective(objectiveID, amount);
        }

        CheckForCompletedQuests();
    }

    public void OnObjectiveUpdated(QuestRuntimeInstance quest, string objectiveID, int newValue)
    {
        // purely update UI / notify player
        questNotif.SetNotifText("Objective Complete");
        questNotif.ShowQuestNotif();
        
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
                questRewardUI.OpenQuestRewardsUI();
                Destroy(currentQuestGiver);

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
}
