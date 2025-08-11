using System.Collections.Generic;
using UnityEngine;

public class TrafficIntersection : MonoBehaviour
{
    public List<Transform> exitPaths; // Different paths: forward, left, right
    public TrafficLightController trafficLight;

    public Transform GetRandomExitPath()
    {
        if (exitPaths.Count == 0) return null;
        return exitPaths[Random.Range(0, exitPaths.Count)];
    }

    public bool CanGo()
    {
        return trafficLight != null && trafficLight.CanGo();
    }

    private void OnDrawGizmos()
    {
        if (exitPaths == null || exitPaths.Count == 0) return;
        Gizmos.color = Color.yellow;
        foreach (var path in exitPaths)
        {
            if (path != null)
            {
                Gizmos.DrawLine(transform.position, path.position);
                Gizmos.DrawSphere(path.position, 0.5f);
            }
        }
    }
}
