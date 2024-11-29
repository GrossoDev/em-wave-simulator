using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManySourcesPanel : MonoBehaviour
{
    public UIManager uiManager;

    public Slider amplitudeSlider;
    public InputField frequencyInputField;
    public Slider phaseSlider;
    public Toggle isPhasedArrayToggle;
    public Slider phasedArraySlider;
    public Dropdown alignmentDropdown;
    public Slider separationSlider;

    public List<PointSourceControl> pucks = new List<PointSourceControl>();

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
        if (pucks.Count == 0)
        {
            Debug.LogError("'pucks' was empty.");
            this.enabled = false;
            return;
        }

        LoadValues();
    }

    private void LoadValues()
    {
        var puck = pucks[0];

        Helpers.DoOrComplainIfNull(amplitudeSlider, "amplitudeSlider", () =>
            amplitudeSlider.value = puck.amplitude
        );
        Helpers.DoOrComplainIfNull(frequencyInputField, "frequencyInputField", () =>
            frequencyInputField.text = puck.frequency.ToString()
        );
        Helpers.DoOrComplainIfNull(phaseSlider, "phaseSlider", () =>
            phaseSlider.value = puck.phase
        );
        Helpers.DoOrComplainIfNull(isPhasedArrayToggle, "isPhasedArrayToggle", () =>
            SetIsPhasedArray(false)
        );
        Helpers.DoOrComplainIfNull(phasedArraySlider, "phasedArraySlider", () =>
            phasedArraySlider.value = 0f
        );
        Helpers.DoOrComplainIfNull(alignmentDropdown, "alignmentDropdown", () =>
            alignmentDropdown.value = (int)Alignment.None
        );
        Helpers.DoOrComplainIfNull(separationSlider, "separationSlider", () =>
            separationSlider.value = 0.25f
        );
    }

    #region Setters
    public void SetAmplitude(float value)
    {
        foreach (var puck in pucks)
            puck.amplitude = value;
    }

    public void SetFrequency(float value)
    {
        foreach (var puck in pucks)
            puck.frequency = value;
    }

    public void SetPhase(float value)
    {
        foreach (var puck in pucks)
            puck.phase = value;
    }

    public void TrySetFrequency(string value)
    {
        var result = 0f;
        var parsed = float.TryParse(value, out result);

        if (parsed)
            SetFrequency(result);
    }

    public void SetIsPhasedArray(bool value)
    {
        isPhasedArrayToggle.isOn = value;
        phaseSlider.interactable = !value;
        phasedArraySlider.interactable = value;

        if (value)
        {
            SetAmplitude(amplitudeSlider.value);
            TrySetFrequency(frequencyInputField.text);
            SetPhasedArray(phaseSlider.value);
        }
    }

    public void SetPhasedArray(float value)
    {
        for (int i = 0; i < pucks.Count; i++)
        {
            var puck = pucks[i];
            var phase = (value * i) % (2 * Mathf.PI);

            puck.phase = phase;
        }
    }

    public void SetAlignment(int value)
    {
        switch ((Alignment)value)
        {
            case Alignment.None:
                separationSlider.interactable = false;
                break;
            case Alignment.Vertical:
                separationSlider.interactable = true;
                AlignVertical();
                break;
            case Alignment.Horizontal:
                separationSlider.interactable = true;
                AlignHorizontal();
                break;
        }
    }

    public void SetSeparation()
    {
        switch ((Alignment)alignmentDropdown.value)
        {
            case Alignment.Vertical:
                AlignVertical();
                break;
            case Alignment.Horizontal:
                AlignHorizontal();
                break;
        }
    }
    #endregion

    private void AlignVertical()
    {
        var firstPuck = pucks[0];
        var firstPos = firstPuck.transform.position;

        var waveLength = uiManager.GetParameters().c / firstPuck.frequency;
        var separation = separationSlider.value * waveLength;

        for (int i = 1; i < pucks.Count; i++)
        {
            var puck = pucks[i];
            var puckPos = puck.transform.position;

            puck.transform.position = new Vector3(firstPos.x, firstPos.y + separation * i, firstPos.z);
        }
    }

    private void AlignHorizontal()
    {
        var firstPuck = pucks[0];
        var firstPos = firstPuck.transform.position;

        var waveLength = uiManager.GetParameters().c / firstPuck.frequency;
        var separation = separationSlider.value * waveLength;

        for (int i = 1; i < pucks.Count; i++)
        {
            var puck = pucks[i];
            var puckPos = puck.transform.position;

            puck.transform.position = new Vector3(firstPos.x + separation * i, firstPos.y, firstPos.z);
        }
    }

    public enum Alignment
    {
        None,
        Vertical,
        Horizontal
    }
}
