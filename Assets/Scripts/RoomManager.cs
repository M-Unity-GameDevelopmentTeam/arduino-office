using UnityEngine;
using System;
[Serializable]
public struct Rooms
{
    public GameObject Room;
    public GameObject Walls1;
    public GameObject Walls2;
}
public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private Rooms[] roomes;
    private int currentIndex = 0;

    public void RotateRoom(int direction)
    {
        transform.Rotate(Vector3.up, 180 * direction, 0);
        print(transform.localEulerAngles.y);
        roomes[currentIndex].Walls1.SetActive(transform.localEulerAngles.y == 0);
        roomes[currentIndex].Walls2.SetActive(transform.localEulerAngles.y == 180);
    }

    public void ChangeRoom(int direction)
    {
        roomes[currentIndex].Room.SetActive(false);
        currentIndex = (currentIndex + direction + roomes.Length) % roomes.Length;
        roomes[currentIndex].Room.SetActive(true);
    }
}