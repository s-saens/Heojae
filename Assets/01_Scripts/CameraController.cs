using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject targetObject;
    public float camEaseDuration = 0.05f;

    public void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, targetObject.transform.position, camEaseDuration);
    }
}