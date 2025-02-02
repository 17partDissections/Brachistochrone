using UnityEngine;
using System.Collections.Generic;
using Zenject;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class LevelGeneration : MonoBehaviour
{
    private SceneGrid _grid;
    private Vector3 _nextNodeVector;
    [SerializeField] private Room _startMineRoom;
    [SerializeField] private List<Room> _rooms;
    [SerializeField] private List<Transform> _roomEnds;

    [Inject]
    private void Construct(SceneGrid grid)
    {
        _grid = grid;
    }
    private void Start()
    {
        _roomEnds.Add(_startMineRoom.RoomEnds[0]);
        _nextNodeVector = _startMineRoom.RoomEnds[0].position + _startMineRoom.RoomEnds[0].TransformVector(Vector3.forward * 10);
        GenerateLevel();
    }
    private void GenerateLevel()
    {
        do
        {
            if (_grid.GetNode(_grid.TranscodeNode(_nextNodeVector), out Node nextRoomNode) && !nextRoomNode.IsBusy)
            {
                List<Directions> freeNeighboursDirections = _grid.GetFreeNeighbours(nextRoomNode, out List<Directions> primaryDirections);
                if (primaryDirections.Count > 0)
                {
                    var attemps = 0;
                ReGet:      
                    Room roomToSpawn = GetRandomNCorrectRoom
                    (
                    freeNeighboursDirections,
                    primaryDirections.Contains(Directions.Up),
                    primaryDirections.Contains(Directions.Down),
                    primaryDirections.Contains(Directions.Left),
                    primaryDirections.Contains(Directions.Right)
                    );
                    if (_roomEnds.Count <= 2)
                        if (roomToSpawn.DeadEndRoom)
                        {
                            attemps++;
                            if(attemps < 5)
                                goto ReGet;
                        }
                    var room = GameObject.Instantiate(roomToSpawn);
                    room.transform.position += _nextNodeVector;
                    _roomEnds.AddRange(room.RoomEnds);
                    nextRoomNode.IsBusy = true;
                    nextRoomNode.DoorUpBusy = room.Up;
                    nextRoomNode.DoorDownBusy = room.Down;
                    nextRoomNode.DoorLeftBusy = room.Left;
                    nextRoomNode.DoorRightBusy = room.Right;
                    _nextNodeVector = _roomEnds[0].position + _roomEnds[0].TransformVector(Vector3.forward).normalized * 10;
                }
            }
            else
            {
                if (_roomEnds.Count != 0)
                {
                    _roomEnds.RemoveAt(0);
                    if (_roomEnds.Count != 0)
                        _nextNodeVector = _roomEnds[0].position + _roomEnds[0].TransformVector(Vector3.forward).normalized * 10;
                }
            }
        }
        while (_roomEnds.Count != 0);
    }
    private Room GetRandomNCorrectRoom(List<Directions> freeNeighboursDirections, bool isPrimaryUpDoor, bool isPrimaryDownDoor, bool isPrimaryLeftDoor, bool isPrimaryRightDoor)
    {
        bool savedPrimaryUpDoor, savedPrimaryDownDoor, savedPrimaryLeftDoor, savedPrimaryRightDoor;
        savedPrimaryUpDoor = isPrimaryUpDoor; savedPrimaryDownDoor = isPrimaryDownDoor; savedPrimaryLeftDoor = isPrimaryLeftDoor; savedPrimaryRightDoor = isPrimaryRightDoor;
        List<Room> tempRooms = new List<Room>();
        var randomRes = 0;
        foreach (Directions direction in freeNeighboursDirections)
            switch (direction)
            {
                case Directions.Up:
                    randomRes = UnityEngine.Random.Range(0, 2);
                    isPrimaryUpDoor = (randomRes == 1) ? true : false;
                    break;
                case Directions.Down:
                    randomRes = UnityEngine.Random.Range(0, 2);
                    isPrimaryDownDoor = (randomRes == 1) ? true : false;
                    break;
                case Directions.Left:
                    randomRes = UnityEngine.Random.Range(0, 2);
                    isPrimaryLeftDoor = (randomRes == 1) ? true : false;
                    break;
                case Directions.Right:
                    randomRes = UnityEngine.Random.Range(0, 2);
                    isPrimaryRightDoor = (randomRes == 1) ? true : false;
                    break;
            }
        tempRooms.AddRange(_rooms.FindAll(x => x.Up == isPrimaryUpDoor && x.Down == isPrimaryDownDoor && x.Left == isPrimaryLeftDoor && x.Right == isPrimaryRightDoor));
        return tempRooms[UnityEngine.Random.Range(0, tempRooms.Count)];
    }
}
