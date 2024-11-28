using UnityEngine;
using UnityEngine.UI;

public class ParametersPanel : MonoBehaviour
{
    public UIManager uiManager;

    public Slider brightnessSlider;
    public Slider timescaleSlider;
    public InputField cInputField;
    public Toggle isGrayscaleToggle;
    public Toggle intensityDecayToggle;

    private Parameters parameters;

    private void Awake()
    {
        if (uiManager == null)
        {
            Debug.LogError("The variable 'uiManager' is not set.");
            this.enabled = false;
            return;
        }
    }

    private void Start()
    {
        parameters = uiManager.GetParameters();
        LoadValues();
    }

    private void LoadValues()
    {
        Helpers.DoOrComplainIfNull(brightnessSlider, "brightnessSlider", () =>
            brightnessSlider.value = parameters.brightness
        );
        Helpers.DoOrComplainIfNull(timescaleSlider, "timescaleSlider", () =>
            timescaleSlider.value = parameters.timeScale
        );
        Helpers.DoOrComplainIfNull(cInputField, "cInputField", () =>
            cInputField.text = parameters.c.ToString()
        );
        Helpers.DoOrComplainIfNull(isGrayscaleToggle, "isGrayscaleToggle", () =>
            isGrayscaleToggle.isOn = parameters.isGrayscale
        );
        Helpers.DoOrComplainIfNull(intensityDecayToggle, "intensityDecayToggle", () =>
            intensityDecayToggle.isOn = parameters.GetIntensityDecay()
        );
    }

    #region Setters
    public void SetScale(float value)
    {
        if (parameters != null)
            parameters.scale = value;
    }

    public void SetBrightness(float value)
    {
        if (parameters != null)
            parameters.brightness = value;
    }

    public void SetTimeScale(float value)
    {
        if (parameters != null)
            parameters.timeScale = value;
    }

    public void SetC(float value)
    {
        if (parameters != null)
            parameters.c = value;
    }

    public void TrySetC(string value)
    {
        var result = 0f;
        var parsed = float.TryParse(value, out result);

        if (parsed)
            SetC(result);
    }

    public void SetIntensityDecay(bool value)
    {
        if (parameters != null)
            parameters.SetIntensityDecay(value);
    }

    public void SetGrayscale(bool value)
    {
        if (parameters != null)
            parameters.isGrayscale = value;
    }
    #endregion
}
