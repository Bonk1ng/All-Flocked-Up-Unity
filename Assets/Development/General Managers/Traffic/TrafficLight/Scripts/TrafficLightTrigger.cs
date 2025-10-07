using UnityEngine;

public class TrafficLightTrigger : MonoBehaviour
{

    public BoxCollider redLightBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        redLightBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle")) { 
            var vehicle = other.gameObject.GetComponent<VehicleBase>();
            vehicle.StopVehicle();
            Debug.Log("triggerHit");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            var vehicle = other.gameObject.GetComponent<VehicleBase>();
            vehicle.MoveVehicleToLocation();
            Debug.Log("SetMove");
        }
    }
}
