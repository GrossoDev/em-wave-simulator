using UnityEngine;

public class GlobalInputController : MonoBehaviour
{
    public Coordinator coordinator;

    private void Update()
    {
        if (Input.GetKeyDown("p"))
            coordinator.ToggleParametersPanel();

        if (Input.GetKeyDown("i"))
            coordinator.ToggleSourcePanel();

        if (Input.GetKeyDown(KeyCode.Escape))
            coordinator.CancelAllActions();
        
        if (Input.GetKeyDown(KeyCode.Delete))
            coordinator.DeletePucks();

        if (Input.GetMouseButtonUp((int)MouseButton.Right))
            coordinator.AddPuck();
    }
}
