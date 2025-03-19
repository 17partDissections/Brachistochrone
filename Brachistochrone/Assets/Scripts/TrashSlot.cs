using Q17pD.Brachistochrone.Items;
using Q17pD.Brachistochrone.Player;
using Q17pD.Brachistochrone.Player.Canvas;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Q17pD.Brachistochrone
{
    public class TrashSlot : MonoBehaviour, IDropHandler
    {
        private PlayerInventory _playerInventory;
        private PointerStorage _pointerStorage;
        [Inject] private void Construct(PointerStorage pointerStorage)
        {
            _pointerStorage = pointerStorage;
            _playerInventory = transform.parent.parent.parent.GetComponent<PlayerInventory>();
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (_pointerStorage.ItemSlot != null)
            {
                _playerInventory.DropItem(_pointerStorage.ItemSlot.ItemID);
                _pointerStorage.ItemSlot.ItemID = ItemID.Empty;
                _pointerStorage.ItemSlot.ItemName.text = string.Empty;
                _pointerStorage.ItemSlot.ItemDescription.text = string.Empty;
                _pointerStorage.ItemSlot.IconImage.sprite = _pointerStorage.ItemSlot.EmptyIconSprite;

                _pointerStorage.ItemSlot = null;
            }
        }
    }
}
