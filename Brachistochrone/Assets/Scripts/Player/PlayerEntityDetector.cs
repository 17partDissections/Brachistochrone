using System.Collections;
using Q17pD.Brachistochrone.Items;
using Q17pD.Brachistochrone.Player.Canvas;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Q17pD.Brachistochrone.Player
{
    public class PlayerEntityDetector : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private PlayerCanvas _playerCanvas;
        private RectTransform _crosshair;
        private WaitForSeconds _sleep = new WaitForSeconds(0.1f);
        private TextMeshProUGUI _nameText;
        private IObvservable _lastObservableItem;
        private PlayerInventory _playerInventory;

        [Inject]
        private void Construct(PlayerActionMap map)
        {
            _playerCanvas = GetComponentInChildren<PlayerCanvas>();
            _playerInventory = GetComponent<PlayerInventory>();
            _nameText = _playerCanvas.ItemNameText;
            _crosshair = _playerCanvas.Crosshair.rectTransform;
            map.Player.Enable();
            map.Player.Interact.performed += Interact;
        }
        private IEnumerator Start()
        {
            while (true)
            {
                yield return _sleep;

                Ray ray = RectTransformUtility.ScreenPointToRay(_camera, _crosshair.position);
                if (Physics.Raycast(ray, out RaycastHit hit, 5))
                {
                    if (hit.transform.TryGetComponent<IObvservable>(out IObvservable observableItem))
                    {
                        observableItem.ShowName(_nameText);
                        _lastObservableItem = observableItem;
                    }
                    else if (_lastObservableItem != null)
                    {
                        _lastObservableItem.HideName(_nameText);
                        _lastObservableItem = null;
                    }

                }
            }
        }
        private void Interact(InputAction.CallbackContext context)
        {
            Ray ray = RectTransformUtility.ScreenPointToRay(_camera, _crosshair.position);
            if (Physics.Raycast(ray, out RaycastHit hit, 5))
            {
                if (hit.transform.TryGetComponent<IInterectable>(out IInterectable interectableItem))
                    if(interectableItem is InGameItem) //добавить отдельный enum для типов interectableItem
                        interectableItem.Interact(_playerInventory, _playerCanvas);
            }
        }

    }
}
