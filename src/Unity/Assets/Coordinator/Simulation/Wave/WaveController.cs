using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public Parameters parameters = new Parameters();

    public List<PointSource> sources = new List<PointSource>();
    public Material targetMaterial;

    public SimulationManager simulationManager;

    private const int MAX_SOURCES = 100;

    private void Awake()
    {
        if (targetMaterial == null)
        {
            Debug.LogError("No material found as a target. Remember to assign the variable 'targetMaterial'.");
            this.enabled = false;
        }

        if (targetMaterial == null)
        {
            Debug.LogError("'simulationManager' is not set.");
            this.enabled = false;
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

        targetMaterial.SetFloat("_Scale", parameters.scale);
        targetMaterial.SetFloat("_Brightness", parameters.brightness);
        targetMaterial.SetFloat("_TimeScale", 1f);
        targetMaterial.SetFloat("_C", parameters.c);
        targetMaterial.SetFloat("_IntensityDecayPower", parameters.intensityDecayPower);
        targetMaterial.SetFloat("_IsGrayscale", parameters.isGrayscale ? 1f : 0f);

        targetMaterial.SetInt("_SourceCount", sourceCount);
        targetMaterial.SetVectorArray("_SourcePositions", Array.ConvertAll(sourcePositions, p => new Vector4(p.x, p.y, 0, 0)));
        targetMaterial.SetFloatArray("_SourceAmplitudes", sourceAmplitudes);
        targetMaterial.SetFloatArray("_SourceFrequencies", sourceFrequencies);
        targetMaterial.SetFloatArray("_SourcePhases", sourcePhases);

        // Use Unity's time system
        Time.timeScale = parameters.timeScale;
    }

    private void OnMouseDown()
    {
        simulationManager.StartSelection();
    }

    private void OnMouseUp()
    {
        simulationManager.StopSelection();
    }
}
