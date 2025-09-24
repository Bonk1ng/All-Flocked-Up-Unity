using UnityEngine;
using UnityEngine.AI;

public class VehicleBase :MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float vehicleSpeed;
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool isStopped;
    [SerializeField] private bool isMoving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveVehicle()
    {

    }

    private void StopVehicle()
    {

    }

    private void CheckForCollisions()
    {

    }

    private void HonkHorn()
    {

    }
}
