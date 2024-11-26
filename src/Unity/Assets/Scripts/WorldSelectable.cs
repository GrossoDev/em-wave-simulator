using System;
using UnityEngine;
using UnityEngine.Events;

public class WorldSelectable : MonoBehaviour
{
    [Serializable]
    public class SelectionChangeEvent : UnityEvent<bool> { }
    [Serializable]
    public class SelectedEvent : UnityEvent { }
    [Serializable]
    public class UnselectedEvent : UnityEvent { }

    public MouseButton button;
    public bool notifyBeforeStart;
    public bool notifyOnStart;
    public bool isSelected;

    public SelectionChangeEvent onSelectionChanged;
    public SelectedEvent onSelected;
    public UnselectedEvent onUnselected;
    public SelectionChangeEvent beforeSelectionChanged;
    public SelectedEvent beforeSelected;
    public UnselectedEvent beforeUnselected;

    private void Start()
    {
        if (notifyOnStart)
            InvokeBeforeEvents();

        if (notifyOnStart)
            InvokeOnEvents();
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp((int)button))
        {
            ChangeSelection(!isSelected);
        }
    }

    private void ChangeSelection(bool value)
    {
        InvokeBeforeEvents();
        isSelected = value;
        InvokeOnEvents();
    }

    private void InvokeBeforeEvents()
    {
        beforeSelectionChanged.Invoke(isSelected);

        if (isSelected)
            beforeSelected.Invoke();
        else
            beforeUnselected.Invoke();
    }

    private void InvokeOnEvents()
    {
        onSelectionChanged.Invoke(isSelected);

        if (isSelected)
            onSelected.Invoke();
        else
            onUnselected.Invoke();
    }

    public void Select()
    {
        ChangeSelection(true);
    }

    public void Unselect()
    {
        ChangeSelection(false);
    }
}
