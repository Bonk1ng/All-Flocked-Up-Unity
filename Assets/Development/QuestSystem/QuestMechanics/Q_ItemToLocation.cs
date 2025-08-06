using UnityEngine;

public class Q_ItemToLocation : MonoBehaviour
{
    public string objectiveID;
    public QuestRuntimeInstance questRuntimeInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questRuntimeInstance = GetComponent<QuestRuntimeInstance>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ItemAtLocation()
    {
        Destroy(gameObject);
        questRuntimeInstance.UpdateObjective(objectiveID, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ItemToLocation"))
        {
            ItemAtLocation();
            Destroy(collision.gameObject);
        }
    }
}
