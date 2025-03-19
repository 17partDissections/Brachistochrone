using UnityEngine;

namespace Q17pD.Brachistochrone.Items
{
    public class ItemSpawnPosition : MonoBehaviour
    {
        public ItemType Type;
    }
    public enum ItemType
    {
        common,
        rare,
        plot
    }
}
