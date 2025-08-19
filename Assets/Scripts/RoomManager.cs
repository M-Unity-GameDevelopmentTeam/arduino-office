using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;
    private int currentIndex = 0;

    public void RotateRoom(int direction)
    {
        transform.Rotate(Vector3.up, 90 * direction, 0);
    }

    public void ChangeRoom(int direction)
    {
        rooms[currentIndex].SetActive(false);
        currentIndex = (currentIndex + direction + rooms.Length) % rooms.Length;
        rooms[currentIndex].SetActive(true);
    }
}