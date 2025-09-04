using UnityEngine;
using DG.Tweening;
public class Room : MonoBehaviour
{
    [SerializeField] private string RoomID;
    [SerializeField] private GameObject Walls1;
    [SerializeField] private GameObject Walls2;
    [SerializeField] bool Direction;
    [SerializeField] int DirectionAngle;
    [SerializeField] private float MoveIn = 0.2f;
    [SerializeField] private float MoveOut = -5;
    public string FRoomID => RoomID;
    public void RotateRoom()
    {
        Direction = !Direction;
        DirectionAngle = !Direction ? 0 : 180;
        Walls1.transform.DOLocalMoveY(!Direction ? MoveIn : MoveOut, 1);
        Walls2.transform.DOLocalMoveY(Direction ? MoveIn : MoveOut, 1);
        transform.DOLocalRotate(Vector3.up * DirectionAngle, 1);
        //TODO: Figure Out
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.collider.CompareTag("Butt1"))
        //else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.collider.CompareTag("Butt2"))
        //else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.collider.CompareTag("Butt3"))
    }
}
