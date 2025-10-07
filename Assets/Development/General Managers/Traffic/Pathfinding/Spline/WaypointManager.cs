using UnityEditor;
using UnityEngine;

public class WaypointManager : EditorWindow
{
    [MenuItem("Window/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointManager>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));
        if(waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Select/Assign root transform", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }
        obj.ApplyModifiedProperties();
    }

    void DrawButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
    }

    void CreateWaypoint()
    {
        GameObject waypointObj = new GameObject("waypoint"+waypointRoot.childCount,typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        if (waypointRoot.childCount > 1)
        {
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;

            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }

        Selection.activeObject = waypoint.gameObject;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
