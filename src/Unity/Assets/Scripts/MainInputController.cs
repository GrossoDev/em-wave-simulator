using UnityEngine;

public class MainInputController : MonoBehaviour
{
    public PuckSpawner puckSpawner;
    public GameObject parametersPanel;

    private void Update()
    {
        if (Input.GetKeyDown("p"))
            HandleParametersPanel();
        
        if (Input.GetKeyDown(KeyCode.Delete))
            HandleDestroyPuck();

        if (Input.GetKeyDown(KeyCode.Escape))
            HandleUnselectAllPucks();

        if (Input.GetMouseButtonUp((int)MouseButton.Right))
            HandleAddPuck();
    }

    private void HandleParametersPanel()
    {
        if (parametersPanel != null)
        {
            parametersPanel.SetActive(!parametersPanel.activeSelf);
        }
        else
        {
            Debug.LogWarning("'parametersPanel' is not set.");
        }
    }

    private void HandleAddPuck()
    {
        if (puckSpawner != null)
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            puckSpawner.Spawn(position);
        }
        else
        {
            Debug.LogWarning("'puckSpawner' is not set.");
        }
    }

    private void HandleDestroyPuck()
    {
        if (puckSpawner != null)
        {
            puckSpawner.DestroySelected();
        }
        else
        {
            Debug.LogWarning("'puckSpawner' is not set.");
        }
    }

    private void HandleUnselectAllPucks()
    {
        if (puckSpawner != null)
        {
            puckSpawner.UnselectPuck();
        }
        else
        {
            Debug.LogWarning("'puckSpawner' is not set.");
        }
    }
}
