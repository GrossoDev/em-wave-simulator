using UnityEngine;

public class PointSourceControl : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    public float phase;

	public WaveInterferenceController waveController;

    private PointSource source;

	private void Start()
	{
		if (waveController == null)
		{
			Debug.LogError("'waveController' can't be null.");
			this.enabled = false;
			return;
		}

		source = new PointSource();
		waveController.sources.Add(source);
	}

    private void OnDestroy()
    {
        if (waveController != null && source != null)
        {            
			waveController.sources.Remove(source);
        }
    }

    private void Update()
	{
		source.position = transform.position;
		source.frequency = frequency;
		source.phase = phase;
		source.amplitude = amplitude;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
