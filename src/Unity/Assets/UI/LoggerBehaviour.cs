using UnityEngine;

public class LoggerBehaviour : MonoBehaviour {

    public void Log(string message)
    {
        Debug.Log(message);
    }

    public void LogWarn(string message)
    {
        Debug.LogWarning(message);
    }

    public void LogError(string message)
    {
        Debug.LogError(message);
    }
}
