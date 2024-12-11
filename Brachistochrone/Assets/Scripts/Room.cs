using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType RoomType;
    public bool DeadEndRoom;
    public List<Door> Doors;
    public List<Transform> MonsterSpawnTransforms;

    [SerializeField] private List<ItemSpawnPosition> ItemSpawnsPositions;

    private void Start()
    {
        //foreach (ItemSpawnPosition pos in ItemSpawnsPositions)
        //{
        //    //обращаться к генератору предметов по типу, с типом, указанным в pos.Type
        //    break;
        //}
    }
    //public Vector3 ChangePositionByDoor(Transform door, Vector3 position)
    //{
    //    var savedPosition = (DoorsTransforms[index].position + RoomCorner1.position + RoomCorner2.position) / 3;
    //    DoorsTransforms[index].position = position;
    //    var newPosition = (DoorsTransforms[index].position + RoomCorner1.position + RoomCorner2.position) / 3;
    //    var distance = newPosition - savedPosition;
    //    RoomCorner1.position += distance;
    //    RoomCorner2.position += distance;
    //}

}
public enum RoomType
{
    Common,
    Rare,
    Plot
}