using UnityEngine;
using Q17pD.Brachistochrone.Player;
using Q17pD.Brachistochrone.Player.Canvas;

namespace Q17pD.Brachistochrone.Items
{
    public class InGameItem : MonoBehaviour, IObvservable, IInterectable
    {
        public ItemID ItemID;
        private string _itemName;
        public string _itemDescription;
        public Sprite _itemIcon;

        private Outline _outline;
        public string Name { get => _itemName; }
        public Outline Outline { get => _outline; }
        private void Start() 
        {
            _itemName = ItemID.ToString();
            _outline = GetComponent<Outline>(); 
        }
        public void Interact(PlayerInventory inventory = null, PlayerCanvas playerCanvas = null)
        {
            inventory.AddItem(gameObject, ItemID);
            playerCanvas.AddItem(this);
        }
    } 
}
