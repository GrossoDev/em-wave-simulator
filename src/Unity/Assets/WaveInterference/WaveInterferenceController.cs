using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveInterferenceController : MonoBehaviour
{
    public float scale;
    public float brightness;
    public float timeScale;
    public float c;
    public float intensityDecayPower;
    public bool isGrayscale;

    public List<PointSource> sources = new List<PointSource>();
    public Material targetMaterial;

    private const int MAX_SOURCES = 100;

    private void Start()
    {
        if (targetMaterial == null)
        {
            Debug.LogError("No material found as a target. Remember to assign the variable 'targetMaterial'.");
            this.enabled = false;
            return;
        }
    }

    private void Update()
    {
        var sourceCount = sources.Count;
        var sourcePositions = sources.Select(p => new Vector4(p.position.x, p.position.y, 0, 0)).ToArray();
        var sourceAmplitudes = sources.Select(p => p.amplitude).ToArray();
        var sourceFrequencies = sources.Select(p => p.frequency).ToArray();
        var sourcePhases = sources.Select(p => p.phase).ToArray();

        // Make sure to reserve 10 positions for each array so that the GPU can also reserve them
        Array.Resize(ref sourcePositions, MAX_SOURCES);
        Array.Resize(ref sourceAmplitudes, MAX_SOURCES);
        Array.Resize(ref sourceFrequencies, MAX_SOURCES);
        Array.Resize(ref sourcePhases, MAX_SOURCES);

        targetMaterial.SetFloat("_Scale", scale);
        targetMaterial.SetFloat("_Brightness", brightness);
        targetMaterial.SetFloat("_TimeScale", timeScale);
        targetMaterial.SetFloat("_C", c);
        targetMaterial.SetFloat("_IntensityDecayPower", intensityDecayPower);
        targetMaterial.SetFloat("_IsGrayscale", isGrayscale ? 1f : 0f);

        targetMaterial.SetInt("_SourceCount", sourceCount);
        targetMaterial.SetVectorArray("_SourcePositions", Array.ConvertAll(sourcePositions, p => new Vector4(p.x, p.y, 0, 0)));
        targetMaterial.SetFloatArray("_SourceAmplitudes", sourceAmplitudes);
        targetMaterial.SetFloatArray("_SourceFrequencies", sourceFrequencies);
        targetMaterial.SetFloatArray("_SourcePhases", sourcePhases);
    }

    public void SetScale(float value)
    {
        scale = value;
    }

    public void SetBrightness(float value)
    {
        brightness = value;
    }

    public void SetTimeScale(float value)
    {
        //timeScale = value;
        Time.timeScale = value;
    }

    public void SetC(float value)
    {
        c = value;
    }

    public void TrySetC(string value)
    {
        var result = 0f;
        var parsed = float.TryParse(value, out result);

        if (parsed) c = result;
    }

    public void SetIntensityDecayPower(float value)
    {
        intensityDecayPower = value;
    }

    public void SetIntensityDecayPower(bool value)
    {
        intensityDecayPower = value ? 2f : 0f;
    }

    public void SetIsGrayscale(bool value)
    {
        isGrayscale = value;
    }
}
