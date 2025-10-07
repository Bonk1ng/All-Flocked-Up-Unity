using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;

    public float width = 1f;

    public Vector3 GetPosition()
    {
        Vector3 min = transform.position + transform.right * width / 2f;
        Vector3 max = transform.position - transform.right * width / 2f;
        return Vector3.Lerp(min,max,Random.Range(0,1f));
    }
}
