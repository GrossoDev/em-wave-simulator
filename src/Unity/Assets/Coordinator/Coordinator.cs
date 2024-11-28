using System;
using System.Collections.Generic;
using UnityEngine;

public class Coordinator : MonoBehaviour
{
    public UIManager uiManager;
    public SimulationManager simulationManager;

    private void Awake()
    {
        if (uiManager == null)
        {
            Debug.LogError("'uiManager' not set.");
        }
        if (simulationManager == null)
        {
            Debug.LogError("'simulationManager' not set.");
        }
    }

    public Parameters GetParameters()
    {
        return simulationManager.GetParameters();
    }

    public void ToggleParametersPanel()
    {
        uiManager.ToggleParametersPanel();
    }

    public void ToggleSourcePanel()
    {
        uiManager.ToggleSourcePanel();
    }

    public void CancelAllActions()
    {
        uiManager.CloseAllPanels();
        simulationManager.UnselectAll();
    }

    #region Selection
    public void StartSelection()
    {
        uiManager.StartSelection();
    }

    public void StopSelection()
    {
        uiManager.StopSelection();
    }

    public void SelectionBox(Vector3 firstWorldPos, Vector3 secondWorldPos)
    {
        simulationManager.SelectionBox(firstWorldPos, secondWorldPos);
    }

    public void SelectionChanged(List<PointSourceControl> selectedPucks)
    {
        uiManager.SelectionChanged(selectedPucks);
    }
    #endregion

    public void AddPuck()
    {
        uiManager.CloseAllPanels();
        simulationManager.AddPuckAtMouse();
    }

    public void DeletePucks()
    {
        uiManager.CloseAllPanels();
        simulationManager.DeletePucks();
    }
}
