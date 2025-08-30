using UnityEngine;

public class Q_InteractComponent : MonoBehaviour, I_QuestMechanicInterface
{
    public string objectiveID;
    public QuestLog questLog;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetQuestLog();
    }



    public void GetQuestLog()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        questLog = player.GetComponent<QuestLog>();
    }

    public void InteractWithObjective()
    {
        questLog.UpdateQuestObjective(objectiveID, 1);
    }

    //Link to PlayerInteraction script.
}
