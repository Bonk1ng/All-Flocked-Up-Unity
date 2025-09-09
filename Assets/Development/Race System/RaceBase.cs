using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering;
using System.Drawing;

public class RaceBase : MonoBehaviour
{
    public RaceData raceData => GetRaceData(raceData);
    [SerializeField] private RaceCheckpoint checkpointPrefab;
    [SerializeField] private RaceGiver currentRaceGiver;
    public List<RaceCheckpoint> activeCheckpoints = new();
    public List<Transform> checkpointTransforms = new();
    [SerializeField] private int checkpointIndex;

    [SerializeField] private float raceTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private RaceData GetRaceData(RaceData data)
    {
        currentRaceGiver.raceData = data;
        return raceData;
    }

    private void GetCheckpointLocationAndClear()
    {
        foreach(var checkpoint in activeCheckpoints)
        {
            checkpointTransforms.Add(checkpoint.transform);
            activeCheckpoints.Remove(checkpoint);
        }
    }

    public void InteractWithRaceGiver()
    {
        GetRaceData(currentRaceGiver.raceData);
        StartRace();
    }

    public void StartRace()
    {
        SpawnCheckpoints();
    }

    private void SpawnCheckpoints()
    {
        foreach (var transform in checkpointTransforms)
        {
            var checkpoint = Instantiate(checkpointPrefab, transform.position, transform.rotation);

            activeCheckpoints.Add(checkpoint);
        }

    }

    private void UpdateCheckpoints()
    {
        for(int i = 0; i < activeCheckpoints.Count; i++)
        {
            checkpointIndex++;
            if(checkpointIndex >= activeCheckpoints.Count)
            {
                RaceCompleted();
            }
        }
    }

    private void RaceCompleted()
    {
        DestroyCheckpoints();
    }


    private void DestroyCheckpoints()
    {
        foreach( var checkpoint in activeCheckpoints)
        {
            activeCheckpoints.Remove(checkpoint);
            Destroy(checkpoint.gameObject);
        }
    }


}
