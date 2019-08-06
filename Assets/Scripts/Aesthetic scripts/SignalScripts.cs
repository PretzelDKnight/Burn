using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LightColor
{
    Red,
    Yellow,
    Green
}

enum LightState
{
    On,
    Stay,
    Off
}

public class SignalScripts : MonoBehaviour
{
    LightColor color;
    LightState state;

    [SerializeField] Light light;

    [SerializeField] Color yellow;
    [SerializeField] Color red;
    [SerializeField] Color green;

    [SerializeField] float duration;
    [SerializeField] float lightOnTime;
    [SerializeField] int maxTicks;

    int tick = 0;
    float time = 0;
    float maxVal;

    // Start is called before the first frame update
    void Start()
    {
        color = LightColor.Yellow;
        state = LightState.Off;
        light.color = yellow;
        maxVal = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * duration;

        switch (state)
        {
            case LightState.On:
                light.intensity = Mathf.Lerp(0, maxVal, time);
                if (time >= 1)
                {
                    time = 0;
                    state = LightState.Stay;
                }
                break;
            case LightState.Stay:
                if (time >= lightOnTime)
                {
                    time = 0;
                    state = LightState.Off;
                }
                break;
            case LightState.Off:
                light.intensity = Mathf.Lerp(maxVal, 0, time);
                if (time >= 1)
                {
                    time = 0;
                    tick++;
                    state = LightState.On;
                }
                break;
            default:
                break;
        }

        if (tick >= maxTicks)
        {
            switch (color)
            {
                case LightColor.Red:
                    light.color = yellow;
                    color = LightColor.Yellow;
                    break;
                case LightColor.Yellow:
                    light.color = green;
                    color = LightColor.Green;
                    break;
                case LightColor.Green:
                    light.color = red;
                    color = LightColor.Red;
                    break;
                default:
                    break;
            }

            tick = 0;
        }
    }
}
