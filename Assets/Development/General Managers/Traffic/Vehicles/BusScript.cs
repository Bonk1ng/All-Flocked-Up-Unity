using UnityEngine;

public class BusScript : VehicleBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void SetMoveToLocation(WaypointNode location)
    {

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
