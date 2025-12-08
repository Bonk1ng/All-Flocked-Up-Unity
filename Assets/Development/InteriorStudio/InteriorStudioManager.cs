#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class InteriorStudioManager : EditorWindow
{
    Vector3 roomSize = new Vector3();
    string interiorName = "";
    [SerializeField]ShellSpawner shellSpawner;
    [SerializeField] EntranceSpawner entranceSpawner;
    [SerializeField] ExitSpawner exitSpawner;
    Vector3 entranceLocation = new();
    Vector3 exitLocation = new();
    bool isPlacingEntrance = false;
    bool isPlacingExit = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacingEntrance)
            {
                SetEntranceLocation();
            }
            else if (isPlacingExit)
            {
                SetExitLocation();
            }

        }
    }

    [MenuItem("Window/InteriorStudio Window")]
    public static void Open()
    {
        GetWindow<InteriorStudioManager>();
    }

    private void OnGUI()
    {
        //SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.HelpBox("This will help create a basic interior ready to be decorated and used.", MessageType.Info);
        EditorGUILayout.BeginVertical("box");
        roomSize = EditorGUILayout.Vector3Field("Room Size: ", roomSize);
        interiorName = EditorGUILayout.TextField("Name of Interior: ", interiorName);
        DrawButtons();
        EditorGUILayout.EndVertical();
    }

    void DrawButtons()
    {
        if (GUILayout.Button("Spawn Interior Shell"))
        {
            SpawnInteriorShell();
        }
        if (GUILayout.Button("Place Entrance Location"))
        {
            isPlacingEntrance = true;
            EditorGUILayout.HelpBox("Click where the entrance should be.", MessageType.Warning);
        }
        if (GUILayout.Button("Place Exit Location"))
        {
            isPlacingExit = true;
            EditorGUILayout.HelpBox("Click where the exit should be.", MessageType.Warning);
        }
        if (GUILayout.Button("Set Interior Size"))
        {
            SetInteriorSize();
        }
    }

    void SpawnInteriorShell()
    {

    }

    void SetEntranceLocation()
    {
        entranceLocation = Input.mousePosition;
        isPlacingEntrance = false;
    }

    void SetExitLocation()
    {
        exitLocation = Input.mousePosition;
        isPlacingExit= false;
    }

    void SetInteriorSize()
    {
        shellSpawner.roomSize = roomSize;
    }
}
#endif