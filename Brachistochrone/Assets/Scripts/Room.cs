using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType RoomType;
    public bool DeadEndRoom;
    public List<Transform> RoomEnds;
    public bool Right, Left, Up, Down;
    public List<Transform> MonsterSpawnTransforms;
    [SerializeField] private List<ItemSpawnPosition> ItemSpawnsPositions;
}
public enum RoomType
{
    Common,
    Rare,
    Plot
}