using System.Collections.Generic;

[System.Serializable]
public class QuestRuntimeInstance
{

    public QuestDetails questData; //Quest Details Struct
    public int currentStageIndex = 0; //Stage index; Each Quest has 1+ stages with 1+ Objectives.
    public Dictionary<string, int> objectiveProgress = new(); //Dictionary stores the objectives from the current Stage Index

    public bool IsComplete => currentStageIndex >= questData.stages.Length; 

    //Gets objectives and for each sets an objectiveID
    public void StartQuest()
    {
        var objectives = GetCurrentObjectives();
        foreach (var obj in objectives)
        {
            objectiveProgress[obj.objectiveID] = 0;
        }
    }

    //Checks if Objective Complete and sets & returns ObjectiveDetails array 0 (empty array).Returns current stage objectives to complete (next objective)
    public ObjectiveDetails[] GetCurrentObjectives()
    {
        if (IsComplete) return new ObjectiveDetails[0];
        return questData.stages[currentStageIndex].objectivesToComplete;
    }

    //Takes ObjectiveID and amount and increments. Checks if stage is complete and advances if true.
    public void UpdateObjective(string objectiveID, int amount)
    {
        if (!objectiveProgress.ContainsKey(objectiveID)) return;

        objectiveProgress[objectiveID] += amount;
        if (CheckStageComplete())
            AdvanceStage();
    }

    
    public bool CheckStageComplete()
    {
        var objectives = GetCurrentObjectives();
        foreach (var obj in objectives)
        {
            if (obj.isOptional) continue;
            if (!objectiveProgress.ContainsKey(obj.objectiveID)) return false;
            if (objectiveProgress[obj.objectiveID] < obj.quantityToComplete)
                return false;
        }
        return true;
    }

    //Advances Stage index. If not complete, Start quest.
    public void AdvanceStage()
    {
        currentStageIndex++;
        if (!IsComplete)
        {
            StartQuest();
        }
    }
}
