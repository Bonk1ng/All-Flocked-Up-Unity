using Unity.VisualScripting;
using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{
    [SerializeField] private RaceBase raceBase;
    [SerializeField] private string raceID;
    [SerializeField] private int checkpointNumber;
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
        if (raceBase != null)
        {

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
