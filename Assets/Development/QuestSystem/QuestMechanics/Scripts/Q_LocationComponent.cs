using System.Threading.Tasks;
using UnityEngine;

public class Q_LocationComponent : MonoBehaviour
{
    public string objectiveID;
    public QuestRuntimeInstance questRuntimeInstance;
    public UI_QuestLocationNotif questLocationCanvas;

    private bool triggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questRuntimeInstance = GetComponent<QuestRuntimeInstance>();
        questLocationCanvas = GetComponent<UI_QuestLocationNotif>();
    }

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        if (triggered) { return; }
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = true;
            //questRuntimeInstance.UpdateObjective(objectiveID, 1);
            questLocationCanvas.ShowQuestLocationNotif();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
