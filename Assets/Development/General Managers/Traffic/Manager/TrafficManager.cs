using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] private List<TrafficLightChanger> trafficLights;
    [SerializeField] private List<TrafficLightChanger> groupALights;
    [SerializeField] private List<TrafficLightChanger> groupBLights;
    [SerializeField] private List<VehicleBase> vehicles;



    private void Awake()
    {
        
    }
    void Start()
    {
        InitLights();
        FindCarsInScene();
        GroupTrafficLights();


        foreach (var light in groupALights)
        {
            light.ChangeLightState(new GreenState(light), ETrafficLightState.Green);
        }

        foreach (var light in groupBLights)
        {
            light.ChangeLightState(new RedState(light), ETrafficLightState.Red);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void InitLights()
    {
        trafficLights.Clear();
        trafficLights.AddRange(FindObjectsByType<TrafficLightChanger>(FindObjectsSortMode.None));
    }

    private void GroupTrafficLights()
    {
        for (int i = 0; i < trafficLights.Count;)
        {
            groupALights.Add(trafficLights[i]);
            i++;
            groupBLights.Add(trafficLights[i]);
            i++;
        }
        trafficLights.Clear();
    }

    private void FindCarsInScene()
    {
        vehicles.Clear();
        vehicles.AddRange(FindObjectsByType<VehicleBase>(FindObjectsSortMode.None));
    }

    public void ChangeGroupALightState(ITrafficInterface state, ETrafficLightState lightState) { 
        foreach(var light in groupALights)
        {
          
            light.ChangeLightState(state,lightState);

        }


    }

    public void ChangeGroupBLightState(ITrafficInterface state, ETrafficLightState lightState)
    {
        foreach (var light in groupBLights)
        {

            light.ChangeLightState( state, lightState);
        }
    }




}
