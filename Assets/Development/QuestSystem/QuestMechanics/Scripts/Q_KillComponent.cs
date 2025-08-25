using UnityEngine;

public class Q_KillComponent : MonoBehaviour, I_QuestMechanicInterface
{
    public string objectiveID;
    QuestRuntimeInstance questRuntimeInstance;
    private readonly string questID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questRuntimeInstance = GetComponent<QuestRuntimeInstance>();
    }


    public void SetRuntimeInstance(QuestRuntimeInstance instance)
    {
        questRuntimeInstance = instance;
    }

    public string GetQuestID() => questID;

    void KillComplete()
    {
        questRuntimeInstance.UpdateObjective(objectiveID, 1);
    }
}
