using UnityEngine;

public class RedState : ITrafficInterface
{

    [SerializeField] private TrafficLightChanger lightChanger;
    [SerializeField] private ETrafficLightState light;
    [SerializeField] private float timer;
    [SerializeField] private float duration = 10f;

    public RedState(TrafficLightChanger lightChanger) { this.lightChanger = lightChanger; }

    public void EnterTrafficState()
    {
        timer = 0f;

    }

    public void UpdateTrafficState() 
    {

        timer += Time.deltaTime;
        if (timer >= duration)
        {
            lightChanger.ChangeLightState(new GreenState(lightChanger),ETrafficLightState.Green);

        }

    }

    public void ExitTrafficState()
    {

    }
}
