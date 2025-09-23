using UnityEngine;

public class TrafficLightChanger : MonoBehaviour
{

    [SerializeField] private GameObject greenLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject redLight;
    [SerializeField] private ETrafficLightState trafficLightEnum;
    [SerializeField] private ITrafficInterace interaceInterace;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
