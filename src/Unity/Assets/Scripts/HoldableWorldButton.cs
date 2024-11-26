using System;
using UnityEngine;
using UnityEngine.Events;

public class HoldableWorldButton : MonoBehaviour
{
    [Serializable]
    public class MouseDownEvent : UnityEvent { }
    
    [Serializable]
    public class MouseUpEvent : UnityEvent { }

    public MouseButton button;
    public MouseDownEvent onMouseDown;
    public MouseUpEvent onMouseUp;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown((int)button))
            onMouseDown.Invoke();
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp((int)button))
            onMouseUp.Invoke();
    }
}
