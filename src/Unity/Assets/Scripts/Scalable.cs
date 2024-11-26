using UnityEngine;

public class Scalable : MonoBehaviour
{
    public Vector3 direction = Vector3.left;

    public void Scale(float scale)
    {
        transform.localScale = Vector3.Scale(transform.localScale, scale * direction);
    }
}
