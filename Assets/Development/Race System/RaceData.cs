using UnityEngine;

[CreateAssetMenu(fileName = "RaceData", menuName = "Scriptable Objects/RaceData")]
public class RaceData : ScriptableObject
{
    public string raceID;
    public float raceTime;
    public bool raceActive;
    public string raceName;
    public string raceDescription;
    public float raceRewards;
    public Transform[] checkpointLocations;
    public RaceCheckpoint[] checkpointSpawns;
    
}
