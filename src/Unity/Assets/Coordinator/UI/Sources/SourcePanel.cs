using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SourcePanel : MonoBehaviour
{
    public UIManager uiManager;

    public Slider amplitudeSlider;
    public InputField frequencyInputField;
    public Slider phaseSlider;

    public PointSourceControl puck;

    private void Awake()
    {
        if (uiManager == null)
        {
            Debug.LogError("The variable 'uiManager' is not set.");
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (puck == null)
        {
            Debug.LogError("You must set 'puck' before enabling.");
            this.enabled = false;
            return;
        }

        LoadValues();
    }

    private void OnDisable()
    {
        puck = null;
    }

    private void LoadValues()
    {
        Helpers.DoOrComplainIfNull(amplitudeSlider, "amplitudeSlider", () =>
            amplitudeSlider.value = puck.amplitude
        );
        Helpers.DoOrComplainIfNull(frequencyInputField, "frequencyInputField", () =>
            frequencyInputField.text = puck.frequency.ToString()
        );
        Helpers.DoOrComplainIfNull(phaseSlider, "phaseSlider", () =>
            phaseSlider.value = puck.phase
        );
    }

    #region Setters
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

    public void TrySetFrequency(string value)
    {
        var result = 0f;
        var parsed = float.TryParse(value, out result);

        if (parsed)
            SetFrequency(result);
    }
    #endregion
}
