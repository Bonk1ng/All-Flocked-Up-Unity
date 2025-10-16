using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour, I_EnemyBase
{
    public Transform[] patrolPoints;
    public GameObject player;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public float detectionRange = 5f;
    public float loseSightRange = 8f;
    [SerializeField] private List<Waypoint> waypoints;
    [SerializeField] private List<WaypointConnection> connections = new();
    public Waypoint currentNode;
    [SerializeField] private Waypoint previousNode;
    [SerializeField] protected NavMeshAgent navAgent;

    private int currentPointIndex = 0;
    private enum EnemyState { Patrolling, Chasing }
    private EnemyState currentState = EnemyState.Patrolling;

    public bool IsDead = false;

    void Start()
    {
        player = FindFirstObjectByType<PlayerGroundMovement>().gameObject;
        FindWaypoints();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (currentState == EnemyState.Patrolling && distanceToPlayer < detectionRange)
        {
            currentState = EnemyState.Chasing;
        }
        else if (currentState == EnemyState.Chasing && distanceToPlayer > loseSightRange)
        {
            currentState = EnemyState.Patrolling;
        }

        if (currentState == EnemyState.Patrolling)
        {
            MoveVehicleToLocation();
            if (navAgent.remainingDistance < 5f)
            {
                ChooseNextDirection(currentNode);
            }
        }
        else if (currentState == EnemyState.Chasing)
        {
            ChasePlayer();
        }
    }

    private void FindWaypoints()
    {
        var waypointsArray = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        foreach (var waypoint in waypointsArray)
        {
            if (waypoint.CompareTag("Human"))
            {
                waypoints.Add(waypoint);
            }

        }
        FindRandomWaypoint();
        Debug.Log("CheckforWaypoints");
    }


    private void FindRandomWaypoint()
    {       
        var randomIndex = Random.Range(0, waypoints.Count);
        var transform = waypoints[randomIndex].transform.position;
        this.transform.position = transform;
        this.currentNode = waypoints[randomIndex];     
        Debug.Log("H");
    }




    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Vector3 targetPos = patrolPoints[currentPointIndex].position;
        targetPos.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, patrolSpeed * Time.deltaTime);

        Vector3 dir = (targetPos - transform.position).normalized;
        dir.y = 0;
        if (dir != Vector3.zero)
            transform.forward = dir;

        if (Vector3.Distance(transform.position, targetPos) < 0.2f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        Vector3 targetPos = player.transform.position;
        targetPos.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, chaseSpeed * Time.deltaTime);

        Vector3 dir = (targetPos - transform.position).normalized;
        dir.y = 0;
        if (dir != Vector3.zero)
            transform.forward = dir;
    }

    public void TakeDamage(int damage)
    {


    }

    public void OnDeath(bool IsDead)
    {

    }

    protected virtual void SetMoveToLocation(Waypoint location)
    {
        currentNode = location;
    }

    //call this to run like wind
    public virtual void MoveVehicleToLocation()
    {
        if (currentNode == null || navAgent == null)
            return;

        navAgent.isStopped = false;
        navAgent.SetDestination(currentNode.transform.position);

    }

    public virtual void StopVehicle()
    {
        navAgent.isStopped = true;
        //Debug.Log("Stopping");
    }

    //protected virtual void CheckForCollisions()
    //{
    //    RaycastHit hit;
    //    int combinedMask = trafficLayer | playerLayer | enemyLayer;

    //    if (Physics.Raycast(transform.position, transform.forward, out hit, detectObjectRange, combinedMask))
    //    {
    //        StopVehicle();
    //        Debug.DrawLine(transform.position, hit.point, Color.yellow);
    //    }
    //    else
    //    {
    //        if (!navAgent.isStopped)
    //            MoveVehicleToLocation();
    //    }
    //}



    protected void ChooseNextDirection(Waypoint node)
    {
        connections.Clear();

        foreach (var connection in node.connections)
            connections.Add(connection);

        if (connections.Count == 0 && node.nextWaypoint != null)
        {
            connections.Add(new WaypointConnection { node = node.nextWaypoint });

        }
        else Destroy(this.gameObject);

        int randomIndex = Random.Range(0, connections.Count);
        Waypoint nextNode = connections[randomIndex].node;
        if (nextNode == null)
            return;
        previousNode = currentNode;
        SetMoveToLocation(nextNode);
        MoveVehicleToLocation();

    }
}
