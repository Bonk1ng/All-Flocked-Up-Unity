using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class CPURacer : MonoBehaviour 
{
    public List<RaceCheckpoint> targetLocation;
    [SerializeField] private RaceCheckpoint currentLocation;
    [SerializeField] private RaceBase raceBase;
    [SerializeField] private NavMeshAgent navAgentComponent;
    public bool isMoving = false;
    [SerializeField] private float detectObjectRange;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRange;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Rigidbody body;
    [SerializeField] private RacerStats racerStats;

    [SerializeField] private float speed;
    [SerializeField] private float accel;
    [SerializeField] private float weight;
    [SerializeField] private float stamina;


    //on load
    public void Awake()
    {
        
        Debug.Log("Loading");
    }
    //on start
    public void Start()
    {
        body = GetComponent<Rigidbody>();
        navAgentComponent = GetComponent<NavMeshAgent>();
        raceBase = FindFirstObjectByType<RaceBase>();
        SetRacerStats();
        

    }
    //raycasts for groundcheck and obstacle detection... if has targetlocation and isMoving then it moves
    public void Update()
    {
        GroundCheck();
        CheckForObstacles();
        if (targetLocation != null && isMoving)
        {
            MoveToLocation();
        }
    }

    //sets the location to move to
    public void SetMoveToLocation(List<Transform> locations)
    {
        for(int i = 0; i < locations.Count; i++)
        {
            targetLocation[i] = currentLocation;
        }
        
    }
    //moves the nav agent to the location given
    public void MoveToLocation()
    {

        navAgentComponent.SetDestination(currentLocation.transform.position);
    }
    //raycast to check for obstacles
    private void CheckForObstacles()
    {
        float turnDegree = 30f;
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * detectObjectRange, Color.red);
        if(Physics.Raycast(transform.position, transform.forward*detectObjectRange, out hit,detectLayer))
        {
            TurnRacer(turnDegree);
        }

        //needs to angle down
        if (Physics.Raycast(transform.position, transform.forward * detectObjectRange, out hit, detectLayer))
        {
            Jump();
        }
        if (Physics.Raycast(transform.position, transform.right * detectObjectRange, out hit, detectLayer))
        {
            TurnRacer(turnDegree);
        }
        if (Physics.Raycast(transform.position, -transform.right* detectObjectRange, out hit, detectLayer))
        {
            TurnRacer(-turnDegree);
        }
    }
    //raycast for groundcheck bool 
    private bool GroundCheck()
    {
        RaycastHit ground;
        if(Physics.Raycast(transform.position,Vector3.down*groundRange, out ground, groundLayer))
        {
            isGrounded = true;
        }
        return isGrounded;
    }
    //turns the racer when raycast detected in obstacle detection
    private void TurnRacer(float turnDeg)
    {
        Quaternion startRot = Quaternion.LookRotation(Vector3.forward);
        Quaternion endRot = Quaternion.LookRotation(Vector3.right);
        Quaternion.RotateTowards(startRot, endRot, turnDeg);
    }
    
    //YUMP-ING
    private void Jump()
    {
        if (GroundCheck() && !isJumping)
        {
            isJumping = true;
            // add verticle force to make the player jump
            body.AddForce(transform.up * jumpHeight);
        }
        else
        {
            RacerFly();
        }
    }

    //... they believe they can touch the sky
    private void RacerFly()
    {

    }
    //gets the checkpoints for the race and orders them
    private void GetCheckpoints()
    {
        raceBase.activeCheckpoints.CopyTo(targetLocation.ToArray());
    }
    //Sets the racer stats to global current variables
    private void SetRacerStats()
    {
        speed = GetRacerSpeed();
        accel = GetRacerAcceleration();
        weight = GetRacerWeight();
        stamina = GetRacerStamina();
    }

    //gets speed from racer stats and +- a random float
    private float GetRacerSpeed()
    {
        var speed = racerStats.speed;
        speed += Random.Range(-5f, 5f);
        return speed;
    }

    //gets accel from racer stats and +- a random float
    private float GetRacerAcceleration()
    {
        var accel = racerStats.acceleration;
        accel += Random.Range(-5f, 5f);
        return accel;
    }

    //gets weight from racer stats and +- a random float
    private float GetRacerWeight()
    {
        var weight = racerStats.weight;
        weight += Random.Range(-5f, 5f);
        return weight;
    }

    //gets stamina from racer stats and +- a raandom float
    private float GetRacerStamina()
    {
        var stamina = racerStats.stamina;
        stamina += Random.Range(-5f, 5f);
        return stamina;
    }

    private void SetRacerSpeed()
    {
        navAgentComponent.speed = speed;
    }

    private void SetRacerWeight()
    {
        body.mass = weight;
    }
    private void SetRacerStamina()
    {

    }

    private void SetRacerAcceleration()
    {
        navAgentComponent.acceleration = accel;
    }
}
