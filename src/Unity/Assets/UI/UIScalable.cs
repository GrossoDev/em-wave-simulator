using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIScalable : MonoBehaviour
{
    public Vector2 direction = Vector2.right;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Scale(float scale)
    {
        rectTransform.sizeDelta = Vector3.Scale(rectTransform.sizeDelta, scale * direction);
    }

    public void InverseScale(float scale)
    {
        rectTransform.sizeDelta = Vector3.Scale(rectTransform.sizeDelta, (1f/scale) * direction);
    }
}
