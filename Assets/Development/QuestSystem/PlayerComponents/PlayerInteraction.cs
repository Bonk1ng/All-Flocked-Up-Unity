using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public LayerMask npcLayer;
    public QuestLog questLog; // assign in Inspector

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
                    questNPC.InteractWithNPC(questLog);
                }
            }
        }

        
        RaycastHit lookHit;
        if (Physics.Raycast(transform.position, transform.forward, out lookHit, 6f, npcLayer))
        {
            var questNPC = lookHit.collider.GetComponentInParent<QuestInteraction>();
            questNPC?.LookAtNPC();
        }
    }
}
