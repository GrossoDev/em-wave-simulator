using System;
using UnityEngine;
using UnityEngine.Events;

public class DoubleClickable : MonoBehaviour
{
    [Serializable]
    public class DoubleClickEvent : UnityEvent { }

    public float maxIntervalInSeconds = 0.5f;
    public DoubleClickEvent onDoubleClick;

    private float lastClickTime = 0f;

    private void OnMouseUp()
    {
        if (Time.time - lastClickTime < maxIntervalInSeconds)
        {
            onDoubleClick.Invoke();
        }

        lastClickTime = Time.time;
    }
}
