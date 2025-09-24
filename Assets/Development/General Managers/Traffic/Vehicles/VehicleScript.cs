using UnityEngine;
using UnityEngine.AI;

public class VehicleScript :  VehicleBase
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void SetMoveToLocation(Transform location)
    {
        currentLocation = location;
    }

    //call this to run like wind
    protected override void MoveVehicleToLocation()
    {
        base.navAgent.SetDestination(currentLocation.position);
    }

    protected override void StopVehicle()
    {
        base.navAgent.isStopped = true;
    }

    protected override void CheckForCollisions()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 5f, transform.forward * detectObjectRange, out hit, trafficLayer))
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

    protected override void HonkHorn()
    {
        //add horn SFX/possible headlight VFX? 
    }
}
