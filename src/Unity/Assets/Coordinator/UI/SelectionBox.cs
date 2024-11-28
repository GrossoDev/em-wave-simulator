using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SelectionBox : MonoBehaviour
{
    public RectTransform canvas;
    public UIManager uiManager;

    private Vector2 lastMousePos;
    private RectTransform rectT;

    private void Awake()
    {
        rectT = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        lastMousePos = Input.mousePosition;
    }

    private void Update()
    {
        var curMousePos = Input.mousePosition;

        var minX = Mathf.Min(lastMousePos.x, curMousePos.x);
        var minY = Mathf.Min(lastMousePos.y, curMousePos.y);
        var maxX = Mathf.Max(lastMousePos.x, curMousePos.x);
        var maxY = Mathf.Max(lastMousePos.y, curMousePos.y);

        var first = new Vector2(minX, minY);
        var second = new Vector2(maxX, maxY);

        rectT.anchoredPosition = first;
        rectT.sizeDelta = second - first;

        var firstWorld = Camera.main.ScreenToWorldPoint(first);
        var secondWorld = Camera.main.ScreenToWorldPoint(second);

        uiManager.SelectionBox(firstWorld, secondWorld);
    }
}
