using UnityEngine;
namespace Q17pD.Brachistochrone.Items
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Item List")]
    public class Item : ScriptableObject
    {
        [Header("Name")]
        public ItemID ItemName;
        public string ItemDescription;
        public Sprite ItemIcon;
        [Header("Sounds")]
        public AudioClip ItemSound_Pickup;
        public AudioClip ItemSound_Drop;
        public AudioClip ItemSound_Use;
    }
}