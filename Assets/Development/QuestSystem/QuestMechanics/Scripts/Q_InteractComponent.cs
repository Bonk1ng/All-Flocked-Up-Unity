using UnityEngine;

public class Q_InteractComponent : MonoBehaviour
{
    public string objectiveID;
    QuestRuntimeInstance questRuntimeInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questRuntimeInstance = GetComponent<QuestRuntimeInstance>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InteractWithObjective()
    {
        questRuntimeInstance.UpdateObjective(objectiveID, 1);
    }

    //Link to PlayerInteraction script.
}
