using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    [SerializeField] private Room Room;
    [SerializeField] private GameObject Camera;
    private bool IsInteracted;
    public void InteractWithItem()
    {
        print(Room.FRoomID);
        IsInteracted = !IsInteracted;
        Camera.SetActive(IsInteracted);
    }
}
