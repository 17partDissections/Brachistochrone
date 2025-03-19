using System.Collections.Generic;
using Q17pD.Brachistochrone.Enemy;
using Q17pD.Brachistochrone.Items;
using Q17pD.Brachistochrone.Player;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType RoomType;
    public bool DeadEndRoom;
    public List<Transform> RoomEnds;
    public bool Right, Left, Up, Down;
    public List<Transform> MonsterSpawnTransforms;
    [SerializeField] private List<ItemSpawnPosition> ItemSpawnsPositions;
    private bool _isPlayerInRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null) { _isPlayerInRoom = true; }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null) { _isPlayerInRoom = false; }
        else if (other.TryGetComponent<Skinny>(out Skinny skinny)) { if(!_isPlayerInRoom) skinny.CheckChaseStatus(); }
    }
}
public enum RoomType
{
    Common,
    Rare,
    Plot
}