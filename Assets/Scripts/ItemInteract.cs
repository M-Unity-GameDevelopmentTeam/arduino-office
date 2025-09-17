using UnityEngine;
using UnityEngine.InputSystem;
public class ItemInteract : MonoBehaviour
{
    private RaycastHit hit;
    private Vector3 MousePosition;
    [SerializeField] private LayerMask ItemsLayer;
    [SerializeField] private Camera CameraForInteract;
    private void Update()
    {
        MousePosition = CameraForInteract.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //Debug.DrawRay(MousePosition, transform.TransformDirection(Vector3.forward), Color.green, float.PositiveInfinity);
        if (Physics.Raycast(MousePosition, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.collider.TryGetComponent(out IItem It) && Mouse.current.leftButton.wasPressedThisFrame)
            It.InteractWithItem();
    }
}
