using System;

[Serializable]
public class Parameters
{
    public float scale;
    public float brightness;
    public float timeScale;
    public float c;
    public float intensityDecayPower;
    public bool isGrayscale;

    public bool GetIntensityDecay()
    {
        return intensityDecayPower > 0f;
    }

    public void SetIntensityDecay(bool doesIntensityDecay)
    {
        intensityDecayPower = doesIntensityDecay ? 2f : 0f;
    }
}
