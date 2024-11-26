using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIScale : MonoBehaviour
{
    public double accumulatedScale = 1.0;
    public ScaleStop currentStop;

    public List<ScaleStop> scaleStops;

    public Text textElement;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        if (scaleStops.Count < 1)
        {
            Debug.LogError("No stops in UIScale.");
            Destroy(this);
            return;
        }
        else
        {
            FindCurrentStop();
            InverseScale(1);
        }
    }

    public void InverseScale(float scale)
    {
        accumulatedScale /= (double)scale;
        FindCurrentStop();

        var width = (double)currentStop.width * accumulatedScale;
        var height = rectTransform.sizeDelta.y;

        rectTransform.sizeDelta = new Vector2((float)width, height);

        if (textElement != null)
        {
            textElement.text = currentStop.label;
        }
    }

    private void FindCurrentStop()
    {
        var newCurrentStop = scaleStops.FirstOrDefault(stop => stop.minScale < accumulatedScale && accumulatedScale < stop.maxScale);
        if (newCurrentStop != null) currentStop = newCurrentStop;
    }

    [Serializable]
    public class ScaleStop
    {
        public double width;
        public double minScale;
        public double maxScale;
        public string label;
    }
}
