using UnityEngine;
using DG.Tweening;
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
    [SerializeField] private Rooms[] roomes;
    [SerializeField] private float movein = 0.2f;
    [SerializeField] private float moveout = -2f;
    private int currentIndex = 0;
    public void RotateRoom(int direction)
    {
        roomes[currentIndex].Walls1.transform.DOLocalMoveY(direction == 0 ? movein : moveout, 1);
        roomes[currentIndex].Walls2.transform.DOLocalMoveY(direction == 180 ? movein : moveout, 1);
        transform.DOLocalRotate(Vector3.up * direction, 1);
    }
    public void ChangeRoom(int direction)
    {
        RotateRoom(0);
        roomes[currentIndex].Room.SetActive(false);
        currentIndex = (currentIndex + direction + roomes.Length) % roomes.Length;
        roomes[currentIndex].Room.SetActive(true);
    }
}