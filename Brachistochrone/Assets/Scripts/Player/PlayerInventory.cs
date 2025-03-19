using Q17pD.Brachistochrone.Items;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using Q17pD.Brachistochrone.Player.Canvas;
using Q17pD.Brachistochrone.Factories;

namespace Q17pD.Brachistochrone.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [HideInInspector] public int InventoryCapacity;
        public int MaxInventoryCapacity;
        [SerializeField] private List<VisualItem> _visualItems;
        private List<ItemID> _playerInventory = new List<ItemID>();
        private ItemID _currentItem;
        private AudioHandler _audioHandler;
        private ItemObjectPool _itemObjectPool;

        [Inject] private void Construct(AudioHandler audioHandler, ItemObjectPool itemObjectPool)
        {
            _audioHandler = audioHandler;
            _itemObjectPool = itemObjectPool;
        }
        public void AddItem(GameObject itemObject, ItemID itemID)
        {
            if (InventoryCapacity + 1 <= MaxInventoryCapacity)
            {
                _playerInventory.Add(itemID);
                ShowOrHideVisualItem(itemID, true);
                itemObject.SetActive(false);
                Debug.Log($"Added item with ItemID:{itemID} in player inventory");
            }
        }
        public void DropItem(ItemID itemID)
        {
            VisualItem item = ShowOrHideVisualItem(itemID, false);
            _audioHandler.PlaySFX(item.ItemSound_Drop, 1);
            InGameItem itemFromPool = _itemObjectPool.GetFromPool(itemID);
            itemFromPool.transform.position = transform.position + transform.TransformDirection(Vector3.fwd);
            Debug.Log($"Removed item with ItemID:{itemID} from player inventory");
        }
        public VisualItem ShowOrHideVisualItem(ItemID itemID, bool visible)
        {
            foreach (var item in _visualItems)
            {
                if (item.ItemID == itemID)
                    if (visible)
                    {
                        if (_currentItem == ItemID.Empty) item.gameObject.SetActive(true);
                        else Debug.Log("add item to visual inventory or smtng like that idk");
                        _audioHandler.PlaySFX(item.ItemSound_Pickup, 1);
                    }
                    else item.gameObject.SetActive(false);

                return item;
            }
            return null;
        }
    }
}
