using UnityEngine;
using System.Collections.Generic;
using Zenject;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using System;
using Q17pD.Brachistochrone;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class LevelGeneration : MonoBehaviour
{
    private SceneGrid _grid;
    private Vector3 _nextNodeVector;
    [SerializeField] private Room _startMineRoom;
    [SerializeField] private List<Room> _roomsPrefabs;
    private List<Transform> _roomEnds = new List<Transform>();
    private List<RoomSaveData> _rooms = new List<RoomSaveData>();
    private int _roomsCount;
    [InjectOptional] private int _sceneBroders;

    private MasterSave _masterSave;


    [Inject]
    private void Construct(SceneGrid grid, MasterSave masterSave)
    {
        _grid = grid;
        _masterSave = masterSave;
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
            if (_grid.GetNode(_grid.TranscodeVectorToNode(_nextNodeVector), out Node nextRoomNode) && !nextRoomNode.IsBusy)
            {
                
                List<Directions> freeNeighboursDirections = _grid.GetFreeNeighbours(nextRoomNode, out List<Directions> primaryDirections);
                if (primaryDirections.Count > 0)
                {
                    Room roomToSpawn = GetRandomNCorrectRoom
                    (
                    freeNeighboursDirections,
                    primaryDirections.Contains(Directions.Up),
                    primaryDirections.Contains(Directions.Down),
                    primaryDirections.Contains(Directions.Left),
                    primaryDirections.Contains(Directions.Right)
                    );
                    Room room = GameObject.Instantiate(roomToSpawn);
                    _roomsCount++;
                    var y = room.transform.position.y;
                    room.transform.position = _nextNodeVector;
                    room.transform.position = new Vector3(room.transform.position.x, y, room.transform.position.z);
                    _rooms.Add(new RoomSaveData(roomToSpawn.name, room.transform.position));
                    _roomEnds.AddRange(room.RoomEnds);
                    nextRoomNode.Room = room;
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
            _masterSave.SaveData.RoomSaveData = _rooms.ToArray();
            //if (_roomsCount > 300) { break; }
        }
        while (_roomEnds.Count != 0);
        Debug.Log("i = " + _roomsCount);
        if (_roomsCount < Mathf.RoundToInt(Mathf.Pow(_sceneBroders, 2)*0.4f)) SceneManager.LoadScene("GameScene");
        

    }
    private Room GetRandomNCorrectRoom(List<Directions> freeNeighboursDirections, bool isPrimaryUpDoor, bool isPrimaryDownDoor, bool isPrimaryLeftDoor, bool isPrimaryRightDoor)
    {
        Dictionary<Directions, bool> doorStates = new Dictionary<Directions, bool>
    {
        { Directions.Up, isPrimaryUpDoor },
        { Directions.Down, isPrimaryDownDoor },
        { Directions.Left, isPrimaryLeftDoor },
        { Directions.Right, isPrimaryRightDoor }
    };
        foreach (var direction in freeNeighboursDirections)
        {
            doorStates[direction] = (Random.Range(0, 2) == 0) ? false : true;
        }
        List<Room> tempRooms = _roomsPrefabs.FindAll(room =>
        room.Up == doorStates[Directions.Up] &&
        room.Down == doorStates[Directions.Down] &&
        room.Left == doorStates[Directions.Left] &&
        room.Right == doorStates[Directions.Right]
);
        if (tempRooms.Count == 0)
            throw new Exception("Ќе найдено ни одной комнаты с заданными двер€ми");
        return tempRooms[Random.Range(0, tempRooms.Count)];
    }
}
