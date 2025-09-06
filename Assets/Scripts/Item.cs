using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Room Room;
    [SerializeField] private GameObject Camera;
    void Start()
    {

    }
    void Update()
    {

    }
    public void InteractWithItem()
    {
        print(Room.FRoomID);
        Camera.SetActive(true);
    }
}
