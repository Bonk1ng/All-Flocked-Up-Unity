using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class QuestRuntimeInstance
{

    public QuestDetails questData; //Quest Details Struct
    public string questID;
    public int currentStageIndex = 0; //Stage index; Each Quest has 1+ stages with 1+ Objectives.
    public Dictionary<string, int> objectiveProgress = new(); //Dictionary stores the objectives from the current Stage Index

    public bool IsComplete => currentStageIndex >= questData.stages.Length;
    public QuestLog questLog;
    public float currentTime;

    public bool isQuestFailed = false;
    public bool isRetrySelected = false;

    public List<GameObject> questMechanicsObjects = new List<GameObject>();

    public void Start()
    {

    }

    //Gets objectives and for each sets an objectiveID
    public void StartQuest()
    {
        questLog = GameObject.Find("Player").GetComponent<QuestLog>();

        var objectives = GetCurrentObjectives();
        foreach (var obj in objectives)
        {
            objectiveProgress[obj.objectiveID] = 0;
        }

        // finds quest mechanics
        GetQuestObjects();


    }

    public void GetQuestObjects()
    {

        questMechanicsObjects.Clear();
        I_QuestMechanicInterface[] mechanics = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
    .OfType<I_QuestMechanicInterface>()
    .ToArray();
        foreach (var mechanic in mechanics)
        {
            if (mechanic is MonoBehaviour monoBehaviour && objectiveProgress.Keys.Contains<string>(mechanic.GetObjectiveID()))
            {
                GameObject mechanicObject = monoBehaviour.gameObject;
                questMechanicsObjects.Add(mechanicObject);
            }
            else continue;
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
        questLog.OnObjectiveUpdated(this, objectiveID, objectiveProgress[objectiveID]);

        Debug.Log("Objective Increments?");
        if (CheckStageComplete())
            AdvanceStage();
        Debug.Log("Stage Completed");
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
        GetQuestObjects();
        if (!IsComplete)
        {
            StartQuest();
        }
        if (currentStageIndex >= questData.stages.Length)
        {
            CompleteQuest();
        }
    }

    public void CompleteQuest()
    {
        questLog.CheckForCompletedQuests();
    }

    
    public void QuestFailed()
    {
        questLog.OnQuestFailed(this);
        isQuestFailed = true;
        if (isRetrySelected)
        {
            objectiveProgress.First();
        }
        else if(isRetrySelected && isQuestFailed)
        {
            questLog.OnQuestFailed(this);
            
        }
   
        Debug.Log("Call Quest Failed");
    }
}
