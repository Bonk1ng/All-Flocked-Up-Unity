using UnityEngine;

public class TrafficLightChanger : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private GameObject greenLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject redLight;

    [Header("SystemRefs")]
    [SerializeField] private ETrafficLightState currentLightState;
    [SerializeField] private ITrafficInterface currentState;
    [SerializeField] private TrafficManager trafficManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        greenLight.SetActive(false);
        yellowLight.SetActive(false);
        redLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateTrafficState();
    }

    public void ChangeLightState(ITrafficInterface state, ETrafficLightState lightState)
    {
        currentState?.ExitTrafficState();
        currentState = state;
        currentLightState = lightState;
        ChangeLightColor(state, lightState);
        currentState.EnterTrafficState();
    
    }

    private void ChangeLightColor(ITrafficInterface state, ETrafficLightState lightState)
    {

    }

    
}
