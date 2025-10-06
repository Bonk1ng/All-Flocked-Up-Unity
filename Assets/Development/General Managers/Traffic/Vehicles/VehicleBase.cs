using NUnit.Framework;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class VehicleBase :MonoBehaviour
{
    [SerializeField] private WaypointNode currentNode;
    [SerializeField]protected Transform currentLocation;
    [SerializeField] private Transform nextLocation;
    [SerializeField] protected NavMeshAgent navAgent;
    [SerializeField] private float vehicleSpeed;
    [SerializeField] protected float detectRadius=20f;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected LayerMask trafficLayer;
    [SerializeField] private bool isStopped;
    [SerializeField] private bool isMoving;
    private List<WaypointConnection> connections = new();

    [SerializeField] protected float detectObjectRange=1000f;
    [SerializeField] protected ETrafficLightState closestLightState;
    [SerializeField] protected LayerMask lightLayer;
    [SerializeField] protected bool lightHit;
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
        CheckNextTrafficLight();

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
    protected virtual void MoveVehicleToLocation()
    {
        if (currentNode == null || navAgent == null)
            return;

        navAgent.isStopped = false;
        navAgent.SetDestination(currentNode.transform.position);

    }

    protected virtual void StopVehicle()
    {
        navAgent.isStopped = true;
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
            if (navAgent.isStopped)
                MoveVehicleToLocation();
        }
    }

    protected virtual void CheckNextTrafficLight()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, detectRadius, transform.forward, out hit, detectObjectRange, lightLayer))
        {
            var hitLight = hit.collider.GetComponent<TrafficLightChanger>();
            if (hitLight)
            {
                closestLightState = hitLight.state;
                lightHit = true;
                Debug.DrawLine(transform.position, hit.point, Color.red);

                switch (hitLight.state)
                {
                    case ETrafficLightState.Red:
                        StopVehicle();
                        break;
                    case ETrafficLightState.Yellow:
                        navAgent.speed = vehicleSpeed * 0.5f;
                        break;
                    case ETrafficLightState.Green:
                        navAgent.speed = vehicleSpeed;
                        navAgent.isStopped = false;
                        break;
                }
            }
        }
    }

    protected void ChooseNextDirection(WaypointNode node)
    {
        connections.Clear();

        foreach (var connection in node.connections)
            connections.Add(connection);

        if (connections.Count == 0)
            return;

        var randomIndex = Random.Range(0, connections.Count);
        var nextNode = connections[randomIndex].node;
        SetMoveToLocation(nextNode);
        MoveVehicleToLocation();
    }

    protected virtual void HonkHorn()
    {
        //add horn SFX/possible headlight VFX? 
    }
}
