using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;
    public List<Waypoint> branches = new List<Waypoint>();
    public List<WaypointConnection> connections = new List<WaypointConnection>();

    public float width = 1f;

    public Vector3 GetPosition()
    {
        Vector3 min = transform.position + transform.right * width / 2f;
        Vector3 max = transform.position - transform.right * width / 2f;
        return Vector3.Lerp(min,max,Random.Range(0,1f));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (nextWaypoint != null)
            Gizmos.DrawLine(transform.position, nextWaypoint.transform.position);

        Gizmos.color = Color.cyan;
        foreach (var branch in branches)
        {
            if (branch != null)
                Gizmos.DrawLine(transform.position, branch.transform.position);
        }
    }
}
