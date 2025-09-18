using Unity.VisualScripting;
using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{
    [SerializeField] private RaceBase raceBase;
    public string raceID;
    public int checkpointNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //calls UpdateCheckpoints on raceBase
    private void TriggerCheckpoint()
    {
        if (raceBase != null && raceBase.checkpointIndex == checkpointNumber)
        {
            raceBase.UpdateCheckpoints(checkpointNumber);
        }

    }

    public void DestroyCheckpoint()
    {
        Destroy(this.gameObject);
    }

    //trigger enter will call above function and destroy checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerCheckpoint();
        }

        if (other.gameObject.CompareTag("Race"))
        {
            CPURacer racer = other.gameObject.GetComponent<CPURacer>();
            racer.NextCheckpoint(); 

        }
    }
}
