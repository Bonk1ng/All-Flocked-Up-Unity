using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class NPCBase: MonoBehaviour, I_NPCInterface
{

    public Transform targetLocation;
    [SerializeField] private NavMeshAgent navAgentComponent;
    public bool isMoving=false;
    //on load
    public void Awake()
    {
        Debug.Log("Loading");
    }
    //on start
    public void Start()
    {
        navAgentComponent = GetComponent<NavMeshAgent>();
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
