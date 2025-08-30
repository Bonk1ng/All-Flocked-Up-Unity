using System.Threading.Tasks;
using UnityEngine;

public class Q_LocationComponent : MonoBehaviour, I_QuestMechanicInterface
{
    public string objectiveID;
    public UI_QuestLocationNotif questLocationCanvas;
    public QuestLog questLog;

    private bool triggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetQuestLog();
        
        
    }

    // Update is called once per frame

    public void GetQuestLog()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        questLog = player.GetComponent<QuestLog>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) { return; }
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = true;
            //questRuntimeInstance.UpdateObjective(objectiveID, 1);
            questLog.UpdateQuestObjective(objectiveID, 1);
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
