using UnityEngine;

public class YellowState : ITrafficInterface
{
    [SerializeField]private TrafficLightChanger lightChanger;
    [SerializeField] private ETrafficLightState light;
    [SerializeField] private float timer;
    [SerializeField] private float duration = 3f;

    public YellowState(TrafficLightChanger lightChanger) { this.lightChanger = lightChanger; }
    public void EnterTrafficState()
    {

        timer = 0f;

    }

    public void UpdateTrafficState()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            lightChanger.ChangeLightState(new RedState(lightChanger), ETrafficLightState.Red);

        }
    }

    public void ExitTrafficState()
    {

    }
}
