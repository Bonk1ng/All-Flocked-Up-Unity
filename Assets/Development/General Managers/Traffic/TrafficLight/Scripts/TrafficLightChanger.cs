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

    public ETrafficLightState state = new();
    private float detectionRange;
    [SerializeField] private LayerMask trafficLayer;
    private bool redLightStop;
    private bool carInLane => CheckForCarsInLane();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trafficManager = FindFirstObjectByType<TrafficManager>();
        greenLight.SetActive(false);
        yellowLight.SetActive(false);
        redLight.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        CheckForCarsInLane();
        state = currentLightState;
        
        currentState?.UpdateTrafficState();
        if (!redLightStop)
        {
            trafficManager.stopCarsAtLight = false;
        }


    }

    public void ChangeLightState(ITrafficInterface state, ETrafficLightState lightState)
    {
        currentState?.ExitTrafficState();
        currentState = state;
        currentLightState = lightState;
        currentState.EnterTrafficState();
        SetState();
    }

    private void ChangeLightColor(string color)
    {
        var lightColor = color;
        if (lightColor == "Green")
        {
            greenLight.SetActive(true);
            yellowLight.SetActive(false);
            redLight.SetActive(false);
        }
        else if (lightColor == "Yellow")
        {
            greenLight.SetActive(false);
            yellowLight.SetActive(true);
            redLight.SetActive(false);
        }
        else if (lightColor == "Red")
        {
            greenLight.SetActive(false);
            yellowLight.SetActive(false);
            redLight.SetActive(true);
        }
        Debug.Log("LightColorChanged");

    }

    public void SetState()
    {
        switch (state)
        {
            case ETrafficLightState.Green:
                currentState = new GreenState(this);
                ChangeLightColor("Green");
                Debug.Log("LightColorChangedGreen");
                redLightStop = false;
                break;
            case ETrafficLightState.Yellow:
                currentState = new YellowState(this);
                ChangeLightColor("Yellow");
                Debug.Log("LightColorChangedYellow");
                break;
            case ETrafficLightState.Red:
                currentState = new RedState(this);
                ChangeLightColor("Red");
                Debug.Log("LightColorChangedRed");
                redLightStop = true;
                break;
        }
        currentState.EnterTrafficState();
    }

    public bool CheckForCarsInLane()
    {
        bool hasHit;
        if (Physics.Raycast(transform.up * -5f, transform.forward * detectionRange, detectionRange, trafficLayer))
        {
            hasHit = true;
            return hasHit;
        }
        hasHit = false;
        return hasHit;
    }


}
