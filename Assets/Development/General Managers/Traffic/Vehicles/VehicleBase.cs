using NUnit.Framework;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class VehicleBase :MonoBehaviour
{
    public WaypointNode currentNode;
    [SerializeField] private WaypointNode previousNode;
    [SerializeField] protected NavMeshAgent navAgent;
    [SerializeField] private float vehicleSpeed;
    [SerializeField] protected float detectRadius=2f;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected LayerMask trafficLayer;
    [SerializeField] private bool isStopped;
    [SerializeField] private bool isMoving;
   [SerializeField] private List<WaypointConnection> connections = new();


    [SerializeField] protected float detectObjectRange=2f;
    [SerializeField]private Vector3 offset = new Vector3(20,0,0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        MoveVehicleToLocation();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckForCollisions();

        if (currentNode == null)
        {
            StopVehicle();
            return;
        }

        if (!navAgent.pathPending && navAgent.remainingDistance < 1f)
        {
            ChooseNextDirection(currentNode);
        }
    }

    protected virtual void SetMoveToLocation(WaypointNode location)
    {
        currentNode = location;
    }

    //call this to run like wind
    public virtual void MoveVehicleToLocation()
    {
        if (currentNode == null || navAgent == null)
            return;

        navAgent.isStopped = false;
        navAgent.SetDestination(currentNode.transform.position + offset);

    }

    public virtual void StopVehicle()
    {
        navAgent.isStopped = true;
        Debug.Log("Stopping");
    }

    protected virtual void CheckForCollisions()
    {
        RaycastHit hit;
        int combinedMask = trafficLayer | playerLayer | enemyLayer;

        if (Physics.SphereCast(transform.position, detectRadius, transform.forward, out hit, detectObjectRange, combinedMask))
        {
            StopVehicle();
            HonkHorn();
            Debug.DrawLine(transform.position, hit.point, Color.yellow);
        }
        else
        {
            if (!navAgent.isStopped)
                MoveVehicleToLocation();
        }
    }



    protected void ChooseNextDirection(WaypointNode node)
    {
        connections.Clear();

        foreach (var connection in node.connections)
            connections.Add(connection);

        if (previousNode != null)
            connections.RemoveAll(c => c.node == previousNode);

        if (connections.Count == 0)
            return;

        var randomIndex = Random.Range(0, connections.Count);
        var nextNode = connections[randomIndex].node;
        previousNode = currentNode;
        SetMoveToLocation(nextNode);
            MoveVehicleToLocation();
        
    }

    protected virtual void HonkHorn()
    {
        //add horn SFX/possible headlight VFX? 
    }
}
