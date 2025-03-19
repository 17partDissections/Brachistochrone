using System.Collections.Generic;
using System.Linq;
using Q17pD.Brachistochrone.Items;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Q17pD.Brachistochrone.Player.Canvas
{
    public class PlayerCanvas : MonoBehaviour
    {
        [SerializeField]  private GameObject _HUD;
        [HideInInspector] public Image Crosshair;
        [HideInInspector] public TextMeshProUGUI ItemNameText;
        [SerializeField] private GameObject _inventory;
        [SerializeField] private PlayerLook _playerLook;
        [SerializeField] private List<ItemSlot> _itemSlots;

        [Inject] private void Construct(PlayerActionMap map)
        {
            Crosshair = _HUD.GetComponentInChildren<Image>();
            ItemNameText = _HUD.GetComponentInChildren<TextMeshProUGUI>();
            map.Player.Enable();
            map.Player.Inventory.started += OpenOrCloseVisualInventory;
        }
        private void OpenOrCloseVisualInventory(InputAction.CallbackContext context)
        {
            if (!_inventory.activeSelf)
            {
                _HUD.gameObject.SetActive(false);
                _inventory.gameObject.SetActive(true);
                Cursor.visible = true; Cursor.lockState = CursorLockMode.None; _playerLook.enabled = false;

            }
            else
            {
                _HUD.gameObject.SetActive(true);
                _inventory.gameObject.SetActive(false);
                Cursor.visible = false; Cursor.lockState = CursorLockMode.Locked; _playerLook.enabled = true;
            }
        }

        public void AddItem(InGameItem inGameItem) 
        {
            ItemSlot freeSlot = _itemSlots.FirstOrDefault(x => x.ItemID == ItemID.Empty);
            freeSlot.ItemID = inGameItem.ItemID;
            freeSlot.ItemName.text = inGameItem.Name;
            freeSlot.ItemDescription.text = inGameItem._itemDescription;
            freeSlot.IconImage.sprite = inGameItem._itemIcon;
            freeSlot.IconImage.gameObject.SetActive(true);
        }
    }
}
