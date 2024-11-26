using System;
using UnityEngine;
using UnityEngine.Events;

public class PuckProxy : MonoBehaviour
{
    [Serializable]
    public class PuckChangedEvent : UnityEvent<PointSourceControl> { }
    [Serializable]
    public class ValueChangedEvent : UnityEvent<float> { }

    public PuckChangedEvent onPuckChanged;
    public ValueChangedEvent onAmplitudeChanged;
    public ValueChangedEvent onFrequencyChanged;
    public ValueChangedEvent onPhaseChanged;

    private PointSourceControl puck;

    public void SetAmplitude(float value)
    {
        if (puck != null)
            puck.amplitude = value;
    }

    public void SetFrequency(float value)
    {
        if (puck != null)
            puck.frequency = value;
    }

    public void SetPhase(float value)
    {
        if (puck != null)
            puck.phase = value;
    }

    public void SetProxy(PointSourceControl puck)
    {
        this.puck = puck;

        onPuckChanged.Invoke(puck);
        onAmplitudeChanged.Invoke(puck.amplitude);
        onFrequencyChanged.Invoke(puck.frequency);
        onPhaseChanged.Invoke(puck.phase);
    }
}
