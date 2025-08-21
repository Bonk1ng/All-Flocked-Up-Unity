using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class QuestDetails : ScriptableObject
{
    //Store the QUEST variables and refs here.

    public string questName;
    [TextArea] public string questLogDescription;
    [TextArea] public string trackerDescription;

    public bool isMainQuest;
    public int stagesToComplete;

    public bool autoAcceptQuest;
    public bool autoCompleteQuest;

    public StageDetails[] stages;
}
