using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] private List<TrafficLightChanger> trafficLights;
    [SerializeField] private List<TrafficLightChanger> groupALights;
    [SerializeField] private List<TrafficLightChanger> groupBLights;
    [SerializeField] private List<Waypoint> waypoints;
    [SerializeField] private int numberOfCars;
    [SerializeField] private List<VehicleBase> vehicleTypes = new();
    [SerializeField] private List<VehicleBase> vehicles;



    private void Awake()
    {
        
    }
    void Start()
    {
        InitLights();
        GroupTrafficLights();
        FindWaypoints();

        SpawnCarsAtWaypoints();
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

    private void FindWaypoints()
    {
        var waypointsArray = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        foreach (var waypoint in waypointsArray)
        {
            if (waypoint.CompareTag("Traffic"))
            {
                waypoints.Add(waypoint);
            }

        }
        Debug.Log("CheckforWaypoints");
    }

    private void GroupTrafficLights()
    {
        for (int i = 0; i < trafficLights.Count;)
        {
            if (trafficLights[i].name.Contains("TrafficLightCurved"))
            {
                groupALights.Add(trafficLights[i]);
                i++;
            }
            else
            {
                groupBLights.Add(trafficLights[i]);
                i++;
            }
        }
        trafficLights.Clear();
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

    private void SpawnCarsAtWaypoints()
    {

        for(int i = 0; i < numberOfCars; i++)
        {
            var car = Instantiate(vehicleTypes[Random.Range(0, vehicleTypes.Count)]);
            vehicles.Add(car);
            var randomIndex = Random.Range(0,waypoints.Count);
            var transform = waypoints[randomIndex].transform.position;
            car.transform.position = transform; 
            car.currentNode = waypoints[randomIndex];
        }
        Debug.Log("CarsSpawned");
    }




}
