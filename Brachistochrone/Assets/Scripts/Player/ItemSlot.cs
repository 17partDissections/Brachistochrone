using Q17pD.Brachistochrone.Items;
using Q17pD.Brachistochrone.Player.Canvas;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Q17pD.Brachistochrone
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDropHandler
    {
        public ItemID ItemID;
        public Image BgImage;
        public Image OutlineImage;
        public Image IconImage;
        public Sprite EmptyIconSprite;
        public TextMeshProUGUI ItemName;
        public TextMeshProUGUI ItemDescription;

        private int _clicks;
        private PointerStorage _pointerStorage;

        [Inject] private void Construct(PointerStorage pointerStorage)
        {
            _pointerStorage = pointerStorage;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ItemDescription.enabled = true;

        }
        public void OnPointerExit(PointerEventData eventData) { ItemDescription.enabled = false; }
        public void OnPointerClick(PointerEventData eventData)
        {
            _clicks++;
            if(_clicks == 2)
            {
                if (!OutlineImage.enabled)
                    OutlineImage.enabled = true;
                else
                    OutlineImage.enabled = false;
                _clicks = 0;
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerStorage.ItemSlot = this;
            Debug.Log("onpointerdown:");
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("slot: " + _pointerStorage.ItemSlot);
            Debug.Log("itemID: " + ItemID);

            if (_pointerStorage.ItemSlot != null && ItemID == ItemID.Empty)
            {
                Debug.Log("if true");
                ItemID = _pointerStorage.ItemSlot.ItemID;
                ItemName.text = _pointerStorage.ItemSlot.ItemName.text;
                ItemDescription.text = _pointerStorage.ItemSlot.ItemDescription.text;
                IconImage.sprite = _pointerStorage.ItemSlot.IconImage.sprite;

                _pointerStorage.ItemSlot.ItemID = ItemID.Empty;
                _pointerStorage.ItemSlot.ItemName.text = string.Empty;
                _pointerStorage.ItemSlot.ItemDescription.text = string.Empty;
                _pointerStorage.ItemSlot.IconImage.sprite = _pointerStorage.ItemSlot.EmptyIconSprite;

                _pointerStorage.ItemSlot = null;
            }
        }


    }
}
