using System;
using UnityEngine;
using UnityEngine.Events;

public class PointSourceControl : MonoBehaviour
{
    [Serializable]
    public class SelectionEvent : UnityEvent { }

    public float amplitude;
    public float frequency;
    public float phase;

    public float minDistToMove = 5f;

	public SelectionEvent onSelected;
	public SelectionEvent onUnselected;

    public SimulationManager simulationManager;
    public WaveController waveController;

    private bool selected;
    private PointSource source;
    private bool mouseDown;
    private bool moving;
    private Vector3? initialMousePosition = null;
    private Vector3? lastMousePosition = null;

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

        if (mouseDown && HasBeenDraged())
            Move();
    }

    private void OnMouseDown()
    {
        initialMousePosition = GetMouseWorldPos();
        lastMousePosition = GetMouseWorldPos();
        mouseDown = true;
    }

    private void OnMouseUp()
    {
        if (!HasBeenDraged())
            ToggleSelect();

        mouseDown = false;
        moving = false;
    }

    public void Destroy()
	{
		Destroy(gameObject);
	}

    #region Selection
    public void Select()
    {
		selected = true;
        onSelected.Invoke();
    }

    public void Unselect()
    {
		selected = false;
        onUnselected.Invoke();
    }

    public void ToggleSelect()
    {
        if (selected)
            simulationManager.Unselect(this);
        else
            simulationManager.Select(this);
    }
    #endregion

    #region Translating
    private void Move()
    {
        if (lastMousePosition != null)
        {
            var difference = GetMouseWorldPos() - lastMousePosition.Value;

            //transform.position = transform.position + difference;
            simulationManager.MovePucks(this, difference);
        }

        lastMousePosition = GetMouseWorldPos();
    }

    private bool HasBeenDraged()
    {
        if (!moving && initialMousePosition.HasValue)
            if ((GetMouseWorldPos() - initialMousePosition).Value.magnitude > minDistToMove)
                moving = true;

        return moving;
    }

    private Vector3 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion
}
