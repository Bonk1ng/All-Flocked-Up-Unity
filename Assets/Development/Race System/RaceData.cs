using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "RaceData", menuName = "Scriptable Objects/RaceData")]
public class RaceData : ScriptableObject
{
    public string raceID;
    public float raceTime;
    public bool raceActive;
    public string raceName;
    public string raceDescription;
    public int raceRewards;
    public List<Transform> checkpointLocations = new();
    public List< RaceCheckpoint>  checkpointSpawns = new();

    public void GetCheckPoints()
    {
        checkpointSpawns.Clear();
        RaceCheckpoint[] checkpoints = Object.FindObjectsByType<RaceCheckpoint>(FindObjectsSortMode.InstanceID);
        foreach (var point in checkpoints)
        {
            if(point.raceID == raceID)
            {
                checkpointSpawns.Add(point);
            }
            else continue;
        }

    }
    
}
