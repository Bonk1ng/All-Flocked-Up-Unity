using UnityEngine;


public class RaceGiver : MonoBehaviour
{
    private RaceBase race;
    public RaceData raceData;
    public UI_CanvasController canvasController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractWithRaceGiver()
    {
        canvasController.OpenRaceGiver();
    }
}
