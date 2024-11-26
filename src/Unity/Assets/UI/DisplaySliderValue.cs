using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplaySliderValue : MonoBehaviour {

	public string format = "{0}";
	public Slider targetSlider;

	private Text targetText;

	private void Start()
	{
		targetText = GetComponent<Text>();
	}

	private void Update()
	{
        var value = targetSlider.value;
        targetText.text = string.Format(format, value);
    }
}
