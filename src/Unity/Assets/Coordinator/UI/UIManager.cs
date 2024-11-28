using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ParametersPanel parametersPanel;
    public SourcePanel sourcePanel;
    public ManySourcesPanel manySourcesPanel;
    public SelectionBox selectionBox;

    public GameObject backdrop;
    public GameObject interactivityLock;

    public Coordinator coordinator;

    private List<PointSourceControl> selectedPucks = new List<PointSourceControl>();

    private void Awake()
    {
        if (coordinator == null)
        {
            Debug.LogError("The variable 'coordinator' is not set.");
            this.enabled = false;
        }
        if (parametersPanel == null)
        {
            Debug.LogError("The variable 'parametersPanel' is not set.");
            this.enabled = false;
        }
        if (sourcePanel == null)
        {
            Debug.LogError("The variable 'sourcePanel' is not set.");
            this.enabled = false;
        }
        if (manySourcesPanel == null)
        {
            Debug.LogError("The variable 'manySourcesPanel' is not set.");
            this.enabled = false;
        }
    }

    public Parameters GetParameters()
    {
        return coordinator.GetParameters();
    }

    public void ToggleParametersPanel()
    {
        var wasOpen = parametersPanel.gameObject.activeSelf;

        CloseAllPanels();

        if (!wasOpen)
            OpenParametersPanel();
    }

    public void ToggleSourcePanel()
    {
        var wasOpen =
            sourcePanel.gameObject.activeSelf ||
            manySourcesPanel.gameObject.activeSelf;

        CloseAllPanels();

        if (!wasOpen)
        {
            if (selectedPucks.Count > 1)
                OpenManySourcesPanel(selectedPucks);
            else if (selectedPucks.Count == 1)
                OpenSourcePanel(selectedPucks.First());
        }
    }

    public void OpenParametersPanel()
    {
        CloseAllPanels();

        parametersPanel.gameObject.SetActive(true);
        SetBackdrop(true);
    }

    public void OpenSourcePanel(PointSourceControl puck)
    {
        CloseAllPanels();

        if (selectedPucks.Count == 1)
        {
            sourcePanel.puck = puck;
            sourcePanel.gameObject.SetActive(true);
            SetBackdrop(true);
        }
    }

    public void OpenManySourcesPanel(List<PointSourceControl> pucks)
    {
        CloseAllPanels();

        if (selectedPucks.Count > 1)
        {
            manySourcesPanel.pucks = pucks;
            manySourcesPanel.gameObject.SetActive(true);
            SetBackdrop(true);
        }
    }

    public void CloseAllPanels()
    {
        parametersPanel.gameObject.SetActive(false);
        sourcePanel.gameObject.SetActive(false);
        manySourcesPanel.gameObject.SetActive(false);
        SetBackdrop(false);
    }

    #region Selection
    public void StartSelection()
    {
        selectionBox.gameObject.SetActive(true);
    }

    public void StopSelection()
    {
        selectionBox.gameObject.SetActive(false);
    }

    public void SelectionBox(Vector3 firstWorldPos, Vector3 secondWorldPos)
    {
        coordinator.SelectionBox(firstWorldPos, secondWorldPos);
    }

    public void SelectionChanged(List<PointSourceControl> selectedPucks)
    {
        this.selectedPucks = selectedPucks;

        CloseAllPanels();
    }
    #endregion

    private void SetBackdrop(bool value)
    {
        backdrop.SetActive(value);
        interactivityLock.SetActive(value);
    }
}
