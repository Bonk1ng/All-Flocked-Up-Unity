using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class NPCBase: MonoBehaviour, I_NPCInterface
{

    public Transform targetLocation;
    [SerializeField] private NavMeshAgent navAgentComponent;
    public bool isMoving=false;
    private UI_CanvasController canvasController;
    //on load
    public void Awake()
    {
        Debug.Log("Loading");
    }
    //on start
    public void Start()
    {
        navAgentComponent = GetComponent<NavMeshAgent>();
        canvasController = FindFirstObjectByType<UI_CanvasController>();
        Debug.Log("NPC LOADED");
    }

    public void Update()
    {
        if (targetLocation!=null && isMoving)
        {
            MoveToLocation();
        }
    }    
    public void LookAtNPC()
    {

    }

    public void InteractWithNPCDialogue()
    {
        canvasController.OpenDialogue();
    }

    public void SetMoveToLocation(Transform location)
    {
        targetLocation = location;
    }

    public void MoveToLocation()
    {
        navAgentComponent.SetDestination(targetLocation.position);
    }


}
