using UnityEngine;
using UnityEngine.Rendering.Universal;
public class CameraBootStrap : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.SetPositionAndRotation(Camera.main.transform.position, Camera.main.transform.rotation);
        gameObject.GetComponent<UniversalAdditionalCameraData>().renderType = CameraRenderType.Overlay;
        Camera.main.GetComponent<UniversalAdditionalCameraData>().cameraStack.Add(gameObject.GetComponent<Camera>());
    }
}
