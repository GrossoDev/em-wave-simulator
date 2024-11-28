using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public List<PointSourceControl> pucks = new List<PointSourceControl>();
    public List<PointSourceControl> selectedPucks = new List<PointSourceControl>();

    public Transform pucksParent;
    public PointSourceControl puckPrefab;

    public WaveController waveController;
    public Coordinator coordinator;

    private void Awake()
    {
        if (coordinator == null)
        {
            Debug.LogError("'coordinator' not set.");
            this.enabled = false;
        }
    }

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

        if (pucksParent == null)
        {
            Debug.LogError("Tried to spawn a puck but 'parent' is null.");
            return;
        }

        var puckGO = Instantiate(puckPrefab.gameObject);
        var puck = puckGO.GetComponent<PointSourceControl>();

        puckGO.transform.parent = pucksParent;
        puck.waveController = waveController;

        puckGO.transform.position = position;

        var localPos = puckGO.transform.localPosition;
        puckGO.transform.localPosition = new Vector3(localPos.x, localPos.y, 0);

        puckGO.name = "Puck";
        puckGO.SetActive(true);

        pucks.Add(puck);
    }

    public Parameters GetParameters()
    {
        return waveController.parameters;
    }

    #region Selection
    public void StartSelection()
    {
        coordinator.StartSelection();
    }

    public void StopSelection()
    {
        coordinator.StopSelection();
        coordinator.SelectionChanged(selectedPucks);
    }

    public void SelectionBox(Vector3 firstWorldPos, Vector3 secondWorldPos)
    {
        var newSelectedPucks = pucks
            .Where(p =>
                p.transform.position.x >= firstWorldPos.x && p.transform.position.x <= secondWorldPos.x &&
                p.transform.position.y >= firstWorldPos.y && p.transform.position.y <= secondWorldPos.y
            )
            .ToList();

        UpdateSelected(newSelectedPucks);
    }

    private void UpdateSelected(List<PointSourceControl> newSelected)
    {
        foreach (var puck in selectedPucks)
            puck.Unselect();

        selectedPucks = newSelected;

        foreach (var puck in selectedPucks)
            puck.Select();
    }

    public void Select(PointSourceControl puck)
    {
        if (!selectedPucks.Contains(puck))
        {
            selectedPucks.Add(puck);
            puck.Select();
            coordinator.SelectionChanged(selectedPucks);
        }
    }

    public void Unselect(PointSourceControl puck)
    {
        if (selectedPucks.Contains(puck))
        {
            selectedPucks.Remove(puck);
            puck.Unselect();
            coordinator.SelectionChanged(selectedPucks);
        }
    }

    public void UnselectAll()
    {
        UpdateSelected(new List<PointSourceControl>());
        coordinator.SelectionChanged(selectedPucks);
    }
    #endregion

    #region Movement
    public void MovePucks(PointSourceControl commandingPuck, Vector3 difference)
    {
        if (selectedPucks.Count > 0)
        {
            if (selectedPucks.Contains(commandingPuck))
            {
                foreach (var puck in selectedPucks)
                    puck.transform.position += difference;
            }
            else
            {
                UnselectAll();
                commandingPuck.transform.position += difference;
            }
        }
        else
        {
            commandingPuck.transform.position += difference;
        }
    }
    #endregion

    public void AddPuckAtMouse()
    {
        var curMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Spawn(curMousePosition);
    }

    public void DeletePucks()
    {
        var pucksToRemove = selectedPucks;

        UnselectAll();

        foreach (var puck in pucksToRemove)
        {
            pucks.Remove(puck);
            puck.Destroy();
        }
    }
}
