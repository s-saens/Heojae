using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camEaseDuration = 0.05f;

    private ObjectBundle objects;
    public void Init(ObjectBundle ob)
    {
        this.objects = ob;
    }

    public void Update()
    {
        objects.cam.transform.position = Vector3.Lerp(objects.cam.transform.position, objects.camPivot.transform.position, camEaseDuration);
    }
}