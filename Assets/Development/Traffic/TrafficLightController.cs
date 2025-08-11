using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public enum LightState { Red, Green }
    public LightState currentState;
    public float greenDuration = 10f;
    public float redDuration = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (currentState == LightState.Green && timer >= greenDuration)
        {
            currentState = LightState.Red;
            timer = 0;
        }
        else if (currentState == LightState.Red && timer >= redDuration)
        {
            currentState = LightState.Green;
            timer = 0;
        }
    }

    public bool CanGo() => currentState == LightState.Green;
}
