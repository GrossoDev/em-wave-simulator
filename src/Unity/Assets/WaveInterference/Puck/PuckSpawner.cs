using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuckSpawner : MonoBehaviour
{
    [Serializable]
    public class SelectedEvent : UnityEvent<PointSourceControl> { }
    [Serializable]
    public class UnselectedEvent : UnityEvent { }

    public Transform parent;

    public PointSourceControl selectedPuck;
    public List<PointSourceControl> pucks = new List<PointSourceControl>();

    public PointSourceControl puckPrefab;
    public WaveInterferenceController waveController;

    public SelectedEvent onSelected;
    public UnselectedEvent onUnselected;

    public void Spawn(Vector2 position)
    {
        if (puckPrefab == null)
        {
            Debug.LogError("Tried to spawn a puck but 'puckPrefab' is null.");
            return;
        }

        if (waveController == null)
        {
            Debug.LogError("Tried to spawn a puck but 'waveController' is null.");
            return;
        }

        if (parent == null)
        {
            Debug.LogError("Tried to spawn a puck but 'parent' is null.");
            return;
        }

        var puckGO = Instantiate(puckPrefab.gameObject);
        var puck = puckGO.GetComponent<PointSourceControl>();

        puckGO.transform.parent = parent;
        puck.waveController = waveController;

        puckGO.transform.position = position;

        var localPos = puckGO.transform.localPosition;
        puckGO.transform.localPosition = new Vector3(localPos.x, localPos.y, 0);

        puckGO.name = "Puck";
        puckGO.SetActive(true);

        pucks.Add(puck);
    }

    public void DestroySelected()
    {
        if (selectedPuck != null)
        {
            selectedPuck.Destroy();
            selectedPuck = null;

            onUnselected.Invoke();
        }
    }

    public void SelectPuck(PointSourceControl puck)
    {
        if (selectedPuck != null)
        {
            selectedPuck.GetComponent<WorldSelectable>().Unselect();
            onUnselected.Invoke();
        }
        
        selectedPuck = puck;
        onSelected.Invoke(selectedPuck);
    }

    public void UnselectPuck(bool selfNotified = false)
    {
        if (selectedPuck != null && !selfNotified)
        {
            selectedPuck.GetComponent<WorldSelectable>().Unselect();
        }

        selectedPuck = null;
        onUnselected.Invoke();
    }
}
