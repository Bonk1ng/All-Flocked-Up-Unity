using UnityEngine;
using UnityEngine.AI;

public class VehicleBase :MonoBehaviour
{
    [SerializeField]protected Transform currentLocation;
    [SerializeField] private Transform nextLocation;
    [SerializeField] protected NavMeshAgent navAgent;
    [SerializeField] private float vehicleSpeed;
    [SerializeField] protected float detectRadius;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected LayerMask trafficLayer;
    [SerializeField] private bool isStopped;
    [SerializeField] private bool isMoving;

    [SerializeField] protected float detectObjectRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckForCollisions();
        if(currentLocation!= null)
        {
            MoveVehicleToLocation();
        }
    }

    protected virtual void SetMoveToLocation(Transform location)
    {
        currentLocation = location;
    }

    //call this to run like wind
    protected virtual void MoveVehicleToLocation()
    {
        navAgent.SetDestination(currentLocation.position);
    }

    protected virtual void StopVehicle()
    {
        navAgent.isStopped = true;
    }

    protected virtual void CheckForCollisions()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position,5f, transform.forward * detectObjectRange, out hit, trafficLayer))
        {
            StopVehicle();
            HonkHorn();
        }
        if (Physics.SphereCast(transform.position, 5f, transform.forward * detectObjectRange, out hit, playerLayer))
        {
            StopVehicle();
            HonkHorn();
        }
        if (Physics.SphereCast(transform.position, 5f, transform.forward * detectObjectRange, out hit, enemyLayer))
        {
            StopVehicle();
            HonkHorn();
        }
    }

    protected virtual void HonkHorn()
    {
        //add horn SFX/possible headlight VFX? 
    }
}
