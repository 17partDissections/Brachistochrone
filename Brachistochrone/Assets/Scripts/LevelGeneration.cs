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

    [Inject] private void Construct(SceneGrid grid)
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
        int i = 1;
        do
        {
            if (_grid.GetNode(_grid.TranscodeNode(_nextNodeVector), out Node nextRoomNode)/* && !nextRoomNode.IsBusy*/)
            {
                List<Directions> freeNeighboursDirections = _grid.GetFreeNeighbours(nextRoomNode, out List<Directions> primaryDirections);
                if (primaryDirections.Count > 0)
                {
                    Debug.Log("primaryDirections.Count > 0");
                ReGet:
                    Room roomToSpawn = GetRandomNCorrectRoom
                    (
                    freeNeighboursDirections,
                    primaryDirections.Contains(Directions.Up),
                    primaryDirections.Contains(Directions.Down),
                    primaryDirections.Contains(Directions.Left),
                    primaryDirections.Contains(Directions.Right)
                    );
                    //if (_roomEnds.Count <= 2)
                    //    if (roomToSpawn.DeadEndRoom)
                    //        goto ReGet;
                    Debug.Log("room spawn");
                    var room = GameObject.Instantiate(roomToSpawn);
                    i++;
                    room.transform.position += _nextNodeVector;
                    _roomEnds.AddRange(room.RoomEnds);
                    _nextNodeVector = _roomEnds[0].localPosition + _roomEnds[0].TransformVector(Vector3.forward * 10);
                    _roomEnds.RemoveAt(0);
                }
            }
            else
            {
                _nextNodeVector = _roomEnds[0].localPosition + _roomEnds[0].TransformVector(Vector3.forward * 10);
                _roomEnds.RemoveAt(0);
            }
        } 
        while (i < 2000);
    }
    private Room GetRandomNCorrectRoom(List<Directions> freeNeighboursDirections, bool isDoorOnUp, bool isDoorOnDown, bool isDoorOnLeft, bool isDoorOnRight)
    {
        Debug.Log("GetRandomRoom");
        List<Room> tempRooms = new List<Room>();
        var randomRes = 0;
        foreach (Directions direction in freeNeighboursDirections)
        switch (direction)
        {
            case Directions.Up:
                randomRes = UnityEngine.Random.Range(0,1);
                isDoorOnUp = (randomRes == 1)? true : false;
                break;
            case Directions.Down:
                randomRes = UnityEngine.Random.Range(0, 1);
                isDoorOnDown = (randomRes == 1) ? true : false;
                break;
            case Directions.Left:
                randomRes = UnityEngine.Random.Range(0, 1);
                isDoorOnLeft = (randomRes == 1) ? true : false;
                break;
            case Directions.Right:
                randomRes = UnityEngine.Random.Range(0, 1);
                    isDoorOnRight = (randomRes == 1) ? true : false;
                break;
        }
        tempRooms.AddRange(_rooms.FindAll(x => x.Up == isDoorOnUp && x.Down == isDoorOnDown && x.Left == isDoorOnLeft && x.Right == isDoorOnRight));
        return tempRooms[UnityEngine.Random.Range(0, tempRooms.Count)];
    }
}
