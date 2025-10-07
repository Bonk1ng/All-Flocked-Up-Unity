using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

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
    [SerializeField]private float detectionRange = 2f;
    [SerializeField] private Vector3 detectRayOffset = new Vector3(0,4,0);
    [SerializeField] private LayerMask trafficLayer;
    [SerializeField]private bool redLightStop;

    private TrafficLightTrigger trigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trafficManager = FindFirstObjectByType<TrafficManager>();
        greenLight.SetActive(false);
        yellowLight.SetActive(false);
        redLight.SetActive(false);
        trigger = GetComponentInChildren<TrafficLightTrigger>();
    }

    // Update is called once per frame
    void Update()
    {

        state = currentLightState;
        
        currentState?.UpdateTrafficState();
        if (redLightStop)
        {

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
                trigger.redLightBox.enabled = false;
                break;
            case ETrafficLightState.Yellow:
                currentState = new YellowState(this);
                ChangeLightColor("Yellow");
                Debug.Log("LightColorChangedYellow");
                trigger.redLightBox.enabled = false;
                break;
            case ETrafficLightState.Red:
                currentState = new RedState(this);
                ChangeLightColor("Red");
                Debug.Log("LightColorChangedRed");
                redLightStop = true;
                trigger.redLightBox.enabled = true;
                break;
        }
        currentState.EnterTrafficState();
    }


}
