using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;

public class RaceBase : MonoBehaviour
{
    [SerializeField] private GameObject playerRef=>GetPlayer();
    public RaceData raceData => GetRaceData(currentRaceGiver.raceData);
    [SerializeField] private RaceCheckpoint checkpointPrefab;
    public RaceGiver currentRaceGiver;
    public List<RaceCheckpoint> activeCheckpoints = new();
    public List<Transform> checkpointTransforms = new();
    public int checkpointIndex = 1;
    public List<RaceData> completedRaces = new();

    [SerializeField] private float raceTimer;
    [SerializeField] private float currentTime;
    [SerializeField] private bool timerStarted;
    [SerializeField] private StartingLine currentRaceStartingLine => raceData.GetStartLine();
    public StartingLine raceStartLine=>currentRaceStartingLine;
    [SerializeField] private List<CPURacer> currentRacerList = new();
    [SerializeField] private CPURacer racerPrefab;

    private void Update()
    {
        UpdateRaceTimer();
    }
    private float StartRaceTimer(float raceTime)
    {
        currentTime = raceTime;
        timerStarted = true;
        return currentTime;

    }

    private void UpdateRaceTimer()
    {
        if (timerStarted)
        {
            currentTime -= Time.deltaTime;
        }
        else return;
    }

    private GameObject GetPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        return player;
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
        SetStartLine();
        SpawnCPURacers();
        StartRaceTimer(raceData.raceTime);
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
            Debug.Log("Checkpoint Hit");
            Debug.Log(activeCheckpoints.Count);
            if (activeCheckpoints.Count == 1)
            {
                RaceCompleted();
            }
            if(currentTime == 0)
            {
                RaceFailed();
            }
            activeCheckpoints.RemoveAt(0);
        }
    }

    private void RaceCompleted()
    {
        UI_CanvasController canvas = FindFirstObjectByType<UI_CanvasController>();
        canvas.OpenRaceRewards();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DestroyCheckpoints();
        
    }

    private void RaceFailed()
    {
        Debug.Log("RaceFailed");
        UI_CanvasController canvas = FindFirstObjectByType<UI_CanvasController>();
        canvas.OpenRaceFail();

    }


    private void DestroyCheckpoints()
    {
        foreach (var checkpoint in activeCheckpoints)
        {
            activeCheckpoints.Remove(checkpoint);
            checkpoint.DestroyCheckpoint();
        }
    }

    public void GiveRewards()
    {
        var reward = raceData.raceRewards;
        FindFirstObjectByType<EXPSystem>().IncrementXP(reward);
    }
    private void SetStartLine()
    {
        Debug.Log(currentRaceStartingLine.name);

    }

    private void MovePlayerToStartLine()
    {
        playerRef.transform.position = raceStartLine.transform.position;
        playerRef.transform.rotation = raceStartLine.transform.rotation;    
    }

    private void SetStartingRacerLocation()
    {
        //this will need to be changed depending on where the start line is located
        Vector3 offset =  new();
        var gap = new Vector3(0, 0, 2);
        for(int i = 0;i<currentRacerList.Count;i++)
        {
            currentRacerList[i].transform.position = raceStartLine.transform.position+offset;
            currentRacerList[i].transform.rotation = raceStartLine.transform.rotation;
            offset += gap;
            currentRacerList[i].SetMoveToLocation(1);
        }
       
    }

    

    private void SpawnCPURacers()
    {
        var racers = raceData.numberOfCPURacers;
        for (int i = racers;i>0;i--)
        {
            CPURacer racer = Instantiate(racerPrefab);
            currentRacerList.Add(racer);
            SetStartingRacerLocation();
            //racer.SetMoveToLocation(0);
            
        }
        MovePlayerToStartLine();
    }

    private void SpawnRaceWalls()
    {

    }

}
