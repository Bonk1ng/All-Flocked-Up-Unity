using UnityEngine;

public class AI_Hawk : MonoBehaviour,I_EnemyBase
{
    [SerializeField] private bool isCooper;
    private enum birdType { CooperHawk, RedTailHawk}
    private birdType currentType=birdType.CooperHawk;
    private enum EnemyState { Patrolling, Chasing, Dive, Roll, Stop, Hit, Retreat,Perch }
    private EnemyState currentState = EnemyState.Patrolling;
    private GameObject player;
    private Rigidbody birdRB;
    public Vector3 takeOffForce;
    [Header("Detection")]
    public float detectionRange = 5f;
    public float loseSightRange = 8f;
    private bool isHit;
    private bool isStopped;
    [Header("Dive")]
    public float diveRange = 1f;
    private float diveForce = 20f;
    public float diveCooldown = 3f;
    [SerializeField] protected SphereCollider diveCollider;
    [SerializeField] private GameObject diveColliderParent;
    [Header("Roll")]
    public float rollRange = 3f;
    public float rollForce = 100f;
    public float rollCooldown = 3f;
    [Header("Perch")]
    private float perchRange = 2f;
    private Transform perchLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        birdRB = GetComponent<Rigidbody>(); 
        player = FindFirstObjectByType<PlayerGroundMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentType)
        {
            case birdType.CooperHawk:
                currentType = birdType.CooperHawk;
                UpdateCooper();
                break;
            case birdType.RedTailHawk:
                currentType=birdType.RedTailHawk;
                UpdateRedTail();
                break;

        }
        
    }


    protected void UpdateCooper()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        switch (currentState)
        {
            case EnemyState.Patrolling:
                if (distanceToPlayer < detectionRange)
                    currentState = EnemyState.Chasing;
                else if (isHit)
                    currentState = EnemyState.Hit;
                break;

            case EnemyState.Chasing:
                if (distanceToPlayer > detectionRange)
                    currentState = EnemyState.Patrolling;
                else if (distanceToPlayer < diveRange)
                    currentState = EnemyState.Dive;
                else if (distanceToPlayer < rollRange)
                    currentState = EnemyState.Roll;
                else if (isHit)
                    currentState = EnemyState.Hit;
                break;

            case EnemyState.Dive:
                if (distanceToPlayer > diveRange)
                    currentState = EnemyState.Chasing;
                break;

            case EnemyState.Roll:
                if (distanceToPlayer > rollRange)
                    currentState = EnemyState.Chasing;
                break;

            case EnemyState.Stop:
                if (isHit)
                {
                    isHit = false;
                    isStopped = true;
                    currentState = EnemyState.Retreat;
                }
                break;

            case EnemyState.Retreat:
                if (isStopped)
                {
                    isStopped = false;
                    currentState = EnemyState.Patrolling;
                }
                else if (isHit)
                {
                    currentState = EnemyState.Hit;
                }
                break;

            case EnemyState.Hit:
                isHit = true;
                currentState = EnemyState.Stop;
                break;
        }

        switch (currentState)
        {
            case EnemyState.Patrolling:

                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
            case EnemyState.Dive:
                if (diveCooldown <= 0)
                {
                    Dive();
                    Debug.Log("DiveCalled");
                    diveCooldown = 3f;
                    currentState = EnemyState.Chasing;
                }
                break;
            case EnemyState.Roll:
                if (rollCooldown <= 0)
                {
                    Roll();
                    Debug.Log("PounceCalled");
                    rollCooldown = 3f;
                    currentState = EnemyState.Chasing;
                }
                break;
            case EnemyState.Stop:
                StopMove();
                Debug.Log("StopCalled");
                break;
            case EnemyState.Hit:
                HitReact();
                Debug.Log("HitCalled");
                break;
            case EnemyState.Retreat:
                Retreat();
                Debug.Log("RetreatCalled");
                break;
        }
    }

    protected void UpdateRedTail()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        switch (currentState)
        {
            case EnemyState.Patrolling:
                if (distanceToPlayer < detectionRange)
                    currentState = EnemyState.Chasing;
                else if (isHit)
                    currentState = EnemyState.Hit;
                break;

            case EnemyState.Chasing:
                if (distanceToPlayer > detectionRange)
                    currentState = EnemyState.Patrolling;
                else if (distanceToPlayer < diveRange)
                    currentState = EnemyState.Dive;
                else if (distanceToPlayer < rollRange)
                    currentState = EnemyState.Roll;
                else if (isHit)
                    currentState = EnemyState.Hit;
                break;

            case EnemyState.Dive:
                if (distanceToPlayer > diveRange)
                    currentState = EnemyState.Chasing;
                break;

            case EnemyState.Roll:
                if (distanceToPlayer > rollRange)
                    currentState = EnemyState.Chasing;
                break;

            case EnemyState.Stop:
                if (isHit)
                {
                    isHit = false;
                    isStopped = true;
                    currentState = EnemyState.Retreat;
                }
                break;

            case EnemyState.Retreat:
                if (isStopped)
                {
                    isStopped = false;
                    currentState = EnemyState.Patrolling;
                }
                else if (isHit)
                {
                    currentState = EnemyState.Hit;
                }
                break;

            case EnemyState.Hit:
                isHit = true;
                currentState = EnemyState.Stop;
                break;
        }

        switch (currentState)
        {
            case EnemyState.Patrolling:

                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
            case EnemyState.Dive:
                if (diveCooldown <= 0)
                {
                    Dive();
                    Debug.Log("DiveCalled");
                    diveCooldown = 3f;
                    currentState = EnemyState.Chasing;
                }
                break;
            case EnemyState.Roll:
                if (rollCooldown <= 0)
                {
                    Roll();
                    Debug.Log("PounceCalled");
                    rollCooldown = 3f;
                    currentState = EnemyState.Chasing;
                }
                break;
            case EnemyState.Stop:
                StopMove();
                Debug.Log("StopCalled");
                break;
            case EnemyState.Hit:
                HitReact();
                Debug.Log("HitCalled");
                break;
            case EnemyState.Retreat:
                Retreat();
                Debug.Log("RetreatCalled");
                break;
        }
    }

    protected void DetectPlayer()
    {

    }

    protected void TakeOff()
    {
        var rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
        dirToPlayer.y = 0;
        Vector3 force = dirToPlayer * takeOffForce.z + Vector3.up * takeOffForce.y;
        birdRB.AddForce(force, ForceMode.Impulse);
    }

    protected void Fly()
    {

    }

    protected void Patrol()
    {

    }

    protected void ChasePlayer()
    {
        Vector3 dirToPlayer = (player.transform.position-transform.position).normalized;
        birdRB.AddForce(dirToPlayer, ForceMode.Acceleration);
    }

    protected void Dive()
    {
        Vector3 diveDir = (player.transform.position- transform.position).normalized;
        birdRB.AddForce(diveDir * diveForce, ForceMode.Impulse);
    }

    protected void Roll()
    {
        birdRB.AddForce(Vector3.up*rollForce,ForceMode.Impulse);
    }

    protected void Perch()
    {

    }

    protected void StopMove()
    {

    }

    protected void HitReact()
    {

    }

    protected void Retreat()
    {

    }

    public void OnDeath(bool isDead)
    {

    }

    public void TakeDamage(int damage)
    {

    }
}
