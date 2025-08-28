using System.Threading.Tasks;
using UnityEngine;

public class Q_LocationComponent : MonoBehaviour, I_QuestMechanicInterface
{
    public string objectiveID;
    public string questID;
    public QuestRuntimeInstance questRuntimeInstance;
    public UI_QuestLocationNotif questLocationCanvas;

    private bool triggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //questLocationCanvas = GetComponent<UI_QuestLocationNotif>();
        
    }

    // Update is called once per frame

    public void SetRuntimeInstance(QuestRuntimeInstance instance)
    {
        questRuntimeInstance = instance;
    }

    public string GetQuestID() => questID;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) { return; }
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = true;
            questRuntimeInstance.UpdateObjective(objectiveID, 1);
            questLocationCanvas.ShowQuestLocationNotif();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = false;
            Destroy(this);
        }
    }
}
