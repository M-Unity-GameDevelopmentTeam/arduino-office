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
    [SerializeField] private Rooms[] RoomsX;
    [SerializeField] private Rooms[] RoomsZ;
    [SerializeField] private float MoveIn = 0.2f;
    [SerializeField] private float MoveOut = -2;
    [SerializeField] private int Padding;
    private int RoomXTempPosition;
    private int RoomZTempPosition;
    private int CurrentRoomXIndex;
    private int CurrentRoomZIndex;
    private void Awake()
    {
        foreach (Rooms RoomX in RoomsX)
        {
            RoomX.Room.transform.DOLocalMoveX(RoomXTempPosition, 0);
            RoomXTempPosition += Padding;
        }
        foreach (Rooms RoomZ in RoomsZ)
        {
            RoomZ.Room.transform.DOLocalMoveZ(RoomZTempPosition, 0);
            RoomZTempPosition += Padding;
        }

    }
    public void RotateRoom()
    {

    }
    private void Update()
    {
        print(CurrentRoomXIndex);
    }
    public void ChangeRoomsX(int Direction)
    {
        if (CurrentRoomZIndex != 0)
            return;
        CurrentRoomXIndex += Direction;
        transform.DOLocalMoveX(CurrentRoomXIndex * -Padding, 1);
    }
    public void ChangeRoomsZ(int Direction)
    {
        if (CurrentRoomXIndex != 0)
            return;
        CurrentRoomZIndex += Direction;
        transform.DOLocalMoveZ(CurrentRoomZIndex * -Padding, 1);
    }
}