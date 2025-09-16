using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class CPURacer : MonoBehaviour
{
    public List<RaceCheckpoint> targetLocation;
    private RaceCheckpoint currentLocation;
    [SerializeField] private RaceBase raceBase;
    [SerializeField] private NavMeshAgent navAgentComponent;
    public bool isMoving = false;
    private float detectObjectRange;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRange;
    private bool isGrounded;

    //on load
    public void Awake()
    {
        Debug.Log("Loading");
    }
    //on start
    public void Start()
    {
        navAgentComponent = GetComponent<NavMeshAgent>();
        raceBase = FindFirstObjectByType<RaceBase>();
        raceBase.activeCheckpoints = targetLocation;
        Debug.Log("NPC LOADED");
    }

    public void Update()
    {
        if (targetLocation != null && isMoving)
        {
            MoveToLocation();
        }
    }


    public void SetMoveToLocation(List<Transform> locations)
    {
        for(int i = 0; i < locations.Count; i++)
        {
            targetLocation[i] = currentLocation;
        }
        
    }

    public void MoveToLocation()
    {

        navAgentComponent.SetDestination(currentLocation.transform.position);
    }

    private void CheckForObstacles()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * detectObjectRange, Color.red);
        if(Physics.Raycast(transform.position, transform.forward*detectObjectRange, out hit,detectLayer))
        {

        }
        if (Physics.Raycast(transform.position, transform.right * detectObjectRange, out hit, detectLayer))
        {

        }
        if (Physics.Raycast(transform.position, -transform.right* detectObjectRange, out hit, detectLayer))
        {

        }
    }

    private void GroundCheck()
    {
        RaycastHit ground;
        if(Physics.Raycast(transform.position,Vector3.down*groundRange, out ground, groundLayer))
        {
            isGrounded = true;
        }
    }


}
