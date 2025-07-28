using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class CarController : MonoBehaviour
{
    [Header("Car Components")]
    [SerializeField] private MeshCollider MC;
    [SerializeField] private Rigidbody RB;

    [Header("Car Navigation")]
    public Spline targetSpline;
    public Spline nextSpline;
    public LayerMask obstacleLayer;

    [Header("Car Settings")]
    [SerializeField] public bool isStopped = false;
    [SerializeField] public bool isStopping = false;
    [SerializeField] public float maxSpeed = 10f;
    [SerializeField] public float acceleration = 2f;
    [SerializeField] public float currentSpeed = 0f;
    [SerializeField] public float distanceTraveled = 0f;
    [SerializeField] public float detectionRange = 5f;
    [SerializeField] public float stopDistance = 1.5f;

    void Start()
    {
        if (MC == null)
        {
            MC = GetComponent<MeshCollider>();
        }
        if (RB == null)
        {
            RB = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isStopped) return;
        if (targetSpline == null)
            return;

        float splineLength = targetSpline.GetLength();

        // Check if near end of spline
        if (distanceTraveled >= splineLength - 0.5f)
        {
            if (nextSpline != null)
            {
                targetSpline = nextSpline;
                nextSpline = null;
                distanceTraveled = 0f;
                return;
            }
            else
            {
                isStopping = true;
            }
        }

        // Speed control
        bool obstacleDetected = DetectObstacle();

        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.fixedDeltaTime);
        distanceTraveled += currentSpeed * Time.fixedDeltaTime;

        // Move along spline
        float t = Mathf.Clamp01(distanceTraveled / splineLength);

        Vector3 position = targetSpline.EvaluatePosition(t);
        Vector3 tangent = targetSpline.EvaluateTangent(t);

        transform.position = position;
        transform.forward = Vector3.Lerp(transform.forward, tangent.normalized, Time.fixedDeltaTime * 5f);
    }
    bool DetectObstacle()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, detectionRange, obstacleLayer))
        {
            return hit.distance <= stopDistance;
        }

        return false;
    }
    public void SetNewSpline(Spline newSpline)
    {
        nextSpline = newSpline;
    }

    public void SetTargetSpline()
    {
        targetSpline = nextSpline;
    }

    public void StartCar()
    {
        isStopped = false;
    }

    public void Accelerate(float deltaTime)
    {
        if (isStopped) return;
        if (currentSpeed >= maxSpeed) return;
        currentSpeed = Mathf.Min(currentSpeed + acceleration * deltaTime, maxSpeed);
    }
    public void Decelerate(float deltaTime)
    {
        if (isStopped) return;
        currentSpeed = Mathf.Max(currentSpeed - acceleration * deltaTime, 0f);

        if (currentSpeed == 0f)
        {
            isStopped = true;
        }
    }
}
