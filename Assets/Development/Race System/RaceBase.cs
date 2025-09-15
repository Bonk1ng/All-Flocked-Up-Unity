using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering;
using System.Drawing;
using System.Linq;

public class RaceBase : MonoBehaviour
{
    public RaceData raceData => GetRaceData(currentRaceGiver.raceData);
    [SerializeField] private RaceCheckpoint checkpointPrefab;
    public RaceGiver currentRaceGiver;
    public List<RaceCheckpoint> activeCheckpoints = new();
    public List<Transform> checkpointTransforms = new();
    public int checkpointIndex = 1;

    [SerializeField] private float raceTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //raceData.checkpointSpawns = activeCheckpoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private RaceData GetRaceData(RaceData data)
    {
        var temp = data;
        return temp;
    }

    private void GetCheckpointLocationAndClear()
    {
        raceData.GetCheckPoints();
        //raceData.checkpointSpawns = activeCheckpoints;
        foreach (var checkpoint in raceData.checkpointSpawns)
        {
            //checkpointTransforms.Add(checkpoint.transform);
           activeCheckpoints.Add(checkpoint);
        }
        activeCheckpoints = activeCheckpoints.OrderBy(cpoint => cpoint.checkpointNumber).ToList();
        SpawnCheckpoints();
    }

    public void InteractWithRaceGiver()
    {
        GetRaceData(currentRaceGiver.raceData);
        StartRace();
    }

    public void StartRace()
    {
        GetCheckpointLocationAndClear();
        Debug.Log("Race Started");
        UI_CanvasController canvas = FindFirstObjectByType<UI_CanvasController>(); 
        canvas.CloseRaceGiver();
        Debug.Log("canvasClosed");
        checkpointIndex = 1;
    }

    private void SpawnCheckpoints()
    {
        foreach (var transform in checkpointTransforms)
        {
            var checkpoint = Instantiate(checkpointPrefab, transform.position, transform.rotation);

            activeCheckpoints.Add(checkpoint);
        }

    }

    public void UpdateCheckpoints(int hitPoint)
    {
        Debug.Log($"HitPoint: {hitPoint}, Expected: {checkpointIndex}");

        for (int i = 0; i < activeCheckpoints.Count; i++)
        {
            if (hitPoint != checkpointIndex) { Debug.Log("Wrong or Last Checkpoint Missed"); return; }
            checkpointIndex++;
            activeCheckpoints.RemoveAt(0);
            Debug.Log("Checkpoint Hit");
            if(activeCheckpoints.Count == 0)
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
