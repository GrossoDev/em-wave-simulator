using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Serializable]
    public class ZoomChangedEvent : UnityEvent<float> { }

    public float minZoom = 1.1f;
    public float maxZoom = 1.1f;
    public float zoomScale = 1.1f;

    public ZoomChangedEvent onZoomChanged;

    private Vector3 lastMousePosition = Vector3.zero;

    private void Update()
    {
        HandlePan();
        HandleZoom();
    }

    private void HandlePan()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.Middle) || Input.GetKeyDown("m"))
        {
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton((int)MouseButton.Middle) || Input.GetKey("m"))
        {
            var currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var difference = currentMousePosition - lastMousePosition;

            transform.position = transform.position - difference;

            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void HandleZoom()
    {
        var scroll = Input.mouseScrollDelta.y;
        var aboveMin = Camera.main.orthographicSize > minZoom;
        var belowMax = Camera.main.orthographicSize < maxZoom;

        if (scroll > 0 && belowMax)
        {
            Camera.main.orthographicSize *= zoomScale;

            onZoomChanged.Invoke(zoomScale);
        }
        else if (scroll < 0 && aboveMin)
        {
            Camera.main.orthographicSize /= zoomScale;

            onZoomChanged.Invoke(1f / zoomScale);
        }
    }
}
