using UnityEngine;
using DG.Tweening;
public class Room : MonoBehaviour
{
    [SerializeField] private string RoomID;
    [SerializeField] private float Duration;
    public string FRoomID => RoomID;
    [SerializeField] private GameObject Walls1;
    [SerializeField] private GameObject Walls2;
    private bool Direction;
    private int DirectionAngle;
    [SerializeField] private float MoveIn = 0;
    [SerializeField] private float MoveOut = -4;
    public void RotateRoom()
    {
        Direction = !Direction;
        DirectionAngle = !Direction ? 0 : 180;
        Walls1.transform.DOLocalMoveZ(!Direction ? MoveIn : MoveOut, Duration);
        Walls2.transform.DOLocalMoveZ(Direction ? MoveIn : MoveOut, Duration);
        transform.DOLocalRotate(Vector3.up * DirectionAngle, Duration);
    }
}
