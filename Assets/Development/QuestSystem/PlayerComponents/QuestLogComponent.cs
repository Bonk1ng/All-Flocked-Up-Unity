using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public List<QuestRuntimeInstance> activeQuests = new();
    public List<QuestDetails> completedQuests = new();
    [SerializeField] private UI_QuestNotif questNotif;
    [SerializeField] private UI_QuestReward questRewardUI;
    public bool hasQuest=false;

    public void AcceptQuest(QuestDetails questData)
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
