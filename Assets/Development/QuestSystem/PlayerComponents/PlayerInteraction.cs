using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public LayerMask npcLayer;
    public LayerMask questLayer;
    public QuestLog questLog; // assign in Inspector
    public UI_CanvasController canvasController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // test input. Change later
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * interactionRange, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, npcLayer))
            {
                var questNPC = hit.collider.GetComponentInParent<QuestInteraction>();
                if (questNPC != null)
                {
                    canvasController.ShowQuestGiver(hit.collider.GetComponentInParent<QuestGiver>());
                }
            }

            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, questLayer))
            {
                var questInteractable = hit.collider.GetComponentInParent<Q_InteractComponent>();
                if (questInteractable != null)
                {
                    questInteractable.InteractWithObjective();
                    Debug.Log(hit.ToString());
                }
            }
        }
        //TAB for quest log...will change this later to new input system
       else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (canvasController.activeLogInstance == null)
            {
                Debug.Log("QuestLog Opened?");
                canvasController.ShowQuestLog();
            }
            else canvasController.DestroyQuestLog();
        }

        
        RaycastHit lookHit;
        if (Physics.Raycast(transform.position, transform.forward, out lookHit, 6f, npcLayer))
        {
            var questNPC = lookHit.collider.GetComponentInParent<QuestInteraction>();
            questNPC?.LookAtNPC();
        }
    }
}
