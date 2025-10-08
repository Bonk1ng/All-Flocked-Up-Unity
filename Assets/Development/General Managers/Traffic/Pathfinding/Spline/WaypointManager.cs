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
        if (GUILayout.Button("Branch"))
        {
            BranchLane();
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

    void BranchLane()
    {
        if(Selection.activeGameObject == null)
        {
            EditorUtility.DisplayDialog("Select a waypoint","Select ONE","NOW","OR NOT?");
            return;
        }

        Waypoint selectedPoint = Selection.activeGameObject.GetComponent<Waypoint>();
        if (selectedPoint == null)
        {
            EditorUtility.DisplayDialog("Select an actual waypoint", "a REAL ONE", "NOW", "OR NOT?");
            return;
        }

        GameObject branchObj = new GameObject("Branch" + waypointRoot.childCount,typeof(Waypoint));
        branchObj.transform.SetParent(waypointRoot, false);

        Waypoint branchWaypoint = branchObj.GetComponent<Waypoint>();
        branchWaypoint.transform.position = selectedPoint.transform.position + selectedPoint.transform.right *5f;
        branchWaypoint.transform.forward = selectedPoint.transform.forward;

        selectedPoint.branches.Add(branchWaypoint);
        Selection.activeObject = branchWaypoint.gameObject;
    }

}
