using UnityEngine;

public class Q_ItemToLocation : MonoBehaviour, I_QuestMechanicInterface
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
        questLog=player.GetComponent<QuestLog>();
    }

    void ItemAtLocation()
    {
        Destroy(gameObject);
        questLog.UpdateQuestObjective(objectiveID, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("QuestItemToLocation"))
        {
            ItemAtLocation();
            Destroy(collision.gameObject);
        }
    }
}
