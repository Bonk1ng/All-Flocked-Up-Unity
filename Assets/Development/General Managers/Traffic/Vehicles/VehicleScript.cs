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

    protected override void SetMoveToLocation(WaypointNode location)
    {
        base.SetMoveToLocation(location);
    }

    //call this to run like wind
    protected override void MoveVehicleToLocation()
    {
        base.MoveVehicleToLocation();
    }

    protected override void StopVehicle()
    {
        base.navAgent.isStopped = true;
    }

    protected override void CheckForCollisions()
    {
        base.CheckForCollisions();
    }

    protected override void CheckNextTrafficLight()
    {
        base.CheckNextTrafficLight();
    }
    protected override void HonkHorn()
    {
        //add horn SFX/possible headlight VFX? 
    }
}
