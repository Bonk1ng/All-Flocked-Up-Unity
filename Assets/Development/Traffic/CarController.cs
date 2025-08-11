using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class CarController : MonoBehaviour
{
    public GameObject currentSpline;
    public GameObject nextSpline;
    public bool isStopped = false;
    public bool isStopping = false;
    public float currentSpeed = 5f;
    public float maxSpeed = 10f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    public float currentSplinePosition = 0f;  

    void Start()
    {
        
    }
    void Update()
    {
        if (currentSpline != null && !isStopped)
        {
            MoveAlongSpline(Time.deltaTime);
        }
        if (!isStopped && !isStopping && currentSpeed < maxSpeed)
        {
            Accelerate(Time.deltaTime);
        }
        else if (isStopping || (isStopped && currentSpeed > 0f))
        {
            Decelerate(Time.deltaTime);
        }
    }

    public void SetCurrentSpline(GameObject spline)
    {
        currentSpline = spline;
        if (currentSpline != null)
        {
            SplineContainer splineContainer = currentSpline.GetComponent<SplineContainer>();
            if (splineContainer != null)
            {
                // Initialize the car's position on the spline
                transform.position = splineContainer.EvaluatePosition(0f);

                // Fix: Spline does not have EvaluateOrientation. Use tangent and up vector to set rotation.
                Vector3 tangent = (Vector3)splineContainer.EvaluateTangent(0f);
                Vector3 up = (Vector3)splineContainer.EvaluateUpVector(0f);
                if (tangent != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(tangent, up);
                }
            }
        }
    }
    public void SetNextSpline(GameObject spline)
    {
        nextSpline = spline;
    }

    public void MoveAlongSpline(float deltaTime)
    {
        if (currentSpline == null || isStopped) return;
        SplineContainer splineContainer = currentSpline.GetComponent<SplineContainer>();
        if (splineContainer == null) return;
        // Calculate the new position along the spline
        currentSplinePosition += currentSpeed * deltaTime;
        float splineLength = splineContainer.Spline.GetLength();
        if (currentSplinePosition > splineLength)
        {
            currentSplinePosition = 0f; // Reset to start of spline
            SetCurrentSpline(nextSpline); // Switch to next spline if available
            splineContainer = currentSpline != null ? currentSpline.GetComponent<SplineContainer>() : null;
            if (splineContainer == null) return;
            splineLength = splineContainer.Spline.GetLength();
        }
        // Update the car's position and rotation
        float t = currentSplinePosition / splineLength;
        transform.position = (Vector3)splineContainer.EvaluatePosition(t);
        Vector3 tangent = (Vector3)splineContainer.Spline.EvaluateTangent(t);
        Vector3 up = (Vector3)splineContainer.Spline.EvaluateUpVector(t);
        if (tangent != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(tangent, up);
        }
    }
    public void Accelerate(float deltaTime)
    {
        if (isStopped) return;
        currentSpeed += acceleration * deltaTime;
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed; // Cap speed at maxSpeed
        }
    }
    public void Decelerate(float deltaTime)
    {
        if (isStopped) return;
        currentSpeed -= deceleration * deltaTime;
        if (currentSpeed < 0f)
        {
            currentSpeed = 0f; // Stop the car
            isStopped = true; // Set stopped state
        }
    }
    public void Stop()
    {
        isStopped = true;
        currentSpeed = 0f; // Immediately stop the car
    }
    public void Resume()
    {
        isStopped = false; // Resume movement
    }
}
