using UnityEngine;

public class Q_ItemToLocation : MonoBehaviour, I_QuestMechanicInterface
{
    public string objectiveID;
    public string questID;
    public QuestRuntimeInstance questRuntimeInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void SetRuntimeInstance(QuestRuntimeInstance instance)
    {
        questRuntimeInstance = instance;
    }

    public string GetQuestID() => questID;

    void ItemAtLocation()
    {
        Destroy(gameObject);
        questRuntimeInstance.UpdateObjective(objectiveID, 1);
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
