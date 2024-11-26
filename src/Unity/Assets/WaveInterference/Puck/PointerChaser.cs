using UnityEngine;

public class PointerChaser : MonoBehaviour
{
    private Vector3? lastMousePosition = null;

    private void Update()
    {
        HandlePan();
    }

    private void HandlePan()
    {
        if (lastMousePosition != null)
        {
            var currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var difference = currentMousePosition - lastMousePosition.Value;

            transform.position = transform.position + difference;
        }

        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetLastMousePosition()
    {
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
