using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;
public class ItemInteract : MonoBehaviour
{
    private RaycastHit hit;
    private Vector3 MousePosition;
    [SerializeField] private LayerMask ItemsLayer;
    void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //print(MousePosition);
        Physics.Raycast(MousePosition, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ItemsLayer);
        if (hit.collider != null && hit.collider.TryGetComponent(out Item It) && Mouse.current.leftButton.wasPressedThisFrame)
            It.InteractWithItem();

    }
}
