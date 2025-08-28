using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//using System.Collections;
public class RoomsController : MonoBehaviour
{
    [SerializeField] private GameObject[] Rooms;
    [SerializeField] private int Padding;
    [SerializeField] private int ColumnSize;
    [SerializeField] private LayerMask RoomsMask;
    private int RowSize;
    private Room CurrentRoom;
    private int RoomXTempPosition;
    private int RoomZTempPosition;
    private int CurrentRoomXIndex;
    private int CurrentRoomZIndex;
    private void Awake()
    {
        CurrentRoom = Rooms[0].GetComponent<Room>();
        foreach (GameObject RoomToPlace in Rooms)
        {
            RoomToPlace.transform.DOLocalMoveX(RoomXTempPosition, 0);
            RoomToPlace.transform.DOLocalMoveZ(RoomZTempPosition, 0);
            switch ((RoomXTempPosition / Padding) < ColumnSize - 1)
            {
                case true:
                    RoomXTempPosition += Padding;
                    break;
                default:
                    RoomXTempPosition = 0;
                    RoomZTempPosition += Padding;
                    break;
            }
            RowSize = RoomZTempPosition / Padding + 1;
        }
        //StartCoroutine(nameof(SEPlacement));
    }
    //private IEnumerator SEPlacement()
    //{
    //    yield return new WaitForSeconds(10);
    //    foreach (Rooms RoomToPlace in Rooms)
    //    {
    //        yield return new WaitForSeconds(2);
    //        RoomToPlace.Room.transform.DOLocalMoveX(RoomXTempPosition, 5);
    //        RoomToPlace.Room.transform.DOLocalMoveZ(RoomZTempPosition, 5);
    //        switch ((RoomXTempPosition / Padding) < ColumnSize - 1)
    //        {
    //            case true:
    //                print("X:" + RoomXTempPosition);
    //                print("Z:" + RoomZTempPosition);
    //                yield return new WaitForSeconds(2);
    //                RoomXTempPosition += Padding;
    //                break;
    //            default:
    //                print("X:" + RoomXTempPosition);
    //                print("Z:" + RoomZTempPosition);
    //                yield return new WaitForSeconds(2);
    //                RoomXTempPosition = 0;
    //                RoomZTempPosition += Padding;
    //                break;
    //        }
    //        RowSize = RoomZTempPosition / Padding;
    //    }
    //}
    public void RotateRoom()
    {
        CurrentRoom.RotateRoom();
    }
    public void ChangeRoomsX(int Direction)
    {
        if ((CurrentRoomXIndex + Direction) >= ColumnSize || (CurrentRoomXIndex + Direction) < 0)
            CurrentRoomXIndex = 0;
        else
            CurrentRoomXIndex += Direction;
        transform.DOLocalMoveX(CurrentRoomXIndex * -Padding, 1).OnComplete(() => GetCurrentRoom());
    }
    public void ChangeRoomsZ(int Direction)
    {
        if ((CurrentRoomZIndex + Direction) >= RowSize || (CurrentRoomZIndex + Direction) < 0)
            CurrentRoomZIndex = 0;
        else 
            CurrentRoomZIndex += Direction;
        transform.DOLocalMoveZ(CurrentRoomZIndex * -Padding, 1).OnComplete(() => GetCurrentRoom());
    }
    private void GetCurrentRoom()
    {
        CurrentRoom = Physics.OverlapBox(Vector3.zero, Vector3.one, Quaternion.identity, RoomsMask)[0].GetComponent<Room>();
        print(CurrentRoom.FRoomID);
    }
}