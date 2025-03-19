using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Q17pD.Brachistochrone.Player.Canvas
{
    public class InventorySlotHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Image _image;
        private RectTransform _rectTransform;

        private Transform _parent;
        private Transform _parentParent;
        private int _siblingIndex;
        private void Start() { _image = GetComponent<Image>(); _rectTransform = GetComponent<RectTransform>(); _parentParent = transform.parent.parent; _parent = transform.parent; }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _siblingIndex = _image.transform.GetSiblingIndex();
            _image.raycastTarget = false;
            _image.transform.SetParent(_parentParent);
            _image.transform.SetAsLastSibling();
        }
        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            _image.transform.SetParent(_parent);
            _image.transform.SetSiblingIndex(_siblingIndex);
            _image.raycastTarget = true;
            _rectTransform.DOAnchorPos(Vector2.zero, 0.1f);
        }
    }
}
