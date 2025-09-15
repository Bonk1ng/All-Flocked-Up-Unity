using Unity.VisualScripting;
using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{
    [SerializeField] private RaceBase raceBase;
    public string raceID;
    public int checkpointNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TriggerCheckpoint()
    {
        if (raceBase != null && raceBase.checkpointIndex == checkpointNumber)
        {
            raceBase.UpdateCheckpoints(checkpointNumber);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerCheckpoint();
            Destroy(gameObject);
        }
    }
}
