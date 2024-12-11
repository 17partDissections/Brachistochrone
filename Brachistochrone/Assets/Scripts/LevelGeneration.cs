using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private Door _startRoomDoor;
    [SerializeField] private List<Room> _rooms;
    [SerializeField] private List<Door> _doors = new List<Door>();

    private int qwer;

    [Inject] private void Construct(EventBus bus)
    {
        bus.DoorOpened += GenerateRoom;
        _doors.Add(_startRoomDoor);
        GenerateRoom();

    }

    private void GenerateRoom()
    {
        var rarity = Random.Range(0, 85);
        if (CheckRoomRarity(rarity, 0, 85, RoomType.Common)) { }
        else if (CheckRoomRarity(rarity, 85, 95, RoomType.Rare)) { }
        else if (CheckRoomRarity(rarity, 85, 95, RoomType.Plot)) { }
        //Debug.Log("rarity: " + rarity);
    }
    public bool CheckRoomRarity(int rarity, int min, int max, RoomType roomType)
    {
        if (rarity >= min && rarity <= max)
        {
            List<Room> roomByType = _rooms.FindAll(x => x.RoomType == roomType);
            Checks(roomByType[Random.Range(0, roomByType.Count)]);
            return true;
        }
        else
            return false;
    }
    private void Checks(Room room)
    {
        if (_doors.Count <= 2)
        {
            if (room.DeadEndRoom)
                GenerateRoom();
            else
                CreateRoom(room);
        }
        else
            CreateRoom(room);
    }

    private void CreateRoom(Room room)
    {
        Room copyOfRoom = Instantiate<Room>(room);
        int newRoomRandomDoorIndex = Random.Range(0, copyOfRoom.Doors.Count);
        Transform door = copyOfRoom.Doors[newRoomRandomDoorIndex].transform;
        int doorsRandomIndex = Random.Range(0, _doors.Count);
        Vector3 directionA = -transform.TransformVector(_doors[doorsRandomIndex].transform.forward);
        Vector3 directionB = transform.TransformVector(door.forward);
        float angle = Vector3.Angle(directionA, directionB);
        float sideAngle = Vector3.SignedAngle(directionA, directionB, Vector3.up);
        
        if (sideAngle < 0) { sideAngle += 360; }
        float angleToTurn = 360 - sideAngle;
        copyOfRoom.transform.Rotate(Vector3.up, angleToTurn);

        copyOfRoom.transform.position = _doors[doorsRandomIndex].transform.position;
        copyOfRoom.transform.position += copyOfRoom.transform.position - door.position;
        copyOfRoom.Doors.RemoveAt(newRoomRandomDoorIndex);
        _doors.RemoveAt(doorsRandomIndex);
        _doors.AddRange(copyOfRoom.Doors.FindAll(x => x != door));
        qwer++;
        if (qwer >= 12) return;
        GenerateRoom();
    }
}
