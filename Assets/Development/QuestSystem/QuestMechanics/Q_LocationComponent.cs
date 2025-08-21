using UnityEngine;

public class Q_LocationComponent : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("QuestPlayerLocation"))
        {
            questRuntimeInstance.UpdateObjective(objectiveID, 1);
        }
    }
}
