using DG.Tweening;
using UnityEngine;
using Zenject;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _doorLeft, _doorRight;
    private Vector3
        _defaultDoorLeftPosition,
        _defaultDoorRightPosition,
        _changedDoorLeftPosition,
        _changedDoorRightPosition;
    private bool _moving;
    private bool _opened;
    private EventBus _bus;
    private bool _used;
    private Sequence _sequence;

    public void Init(EventBus eventBus) { _bus = eventBus; }

    private void Start() 
    {
        _sequence = DOTween.Sequence();
        _defaultDoorLeftPosition = _doorLeft.position + _doorLeft.TransformDirection(Vector3.down);
        _defaultDoorRightPosition = _doorRight.position + _doorRight.TransformDirection(Vector3.down);
        _changedDoorLeftPosition = _doorLeft.position;
        _changedDoorRightPosition = _doorRight.position;
    }

    private void OnTriggerEnter(Collider other) { Open(); }
    private void OnTriggerExit(Collider other) { Close(); }
    private void Open()
    {
        if(!_used) { _bus.DoorOpened?.Invoke(); _used = true; }
        //0.985f
        _sequence.Append(_doorLeft.DOMove(_defaultDoorLeftPosition, 0.5f));
        _sequence.Join(_doorRight.DOMove(_defaultDoorRightPosition, 0.5f).OnComplete(() => { _moving = false; }));
        
    }
    private void Close()
    {
        if(!_sequence.active)
        {
            _sequence.Append(_doorLeft.DOMove(_changedDoorLeftPosition, 0.5f));
            _sequence.Join(_doorRight.DOMove(_changedDoorRightPosition, 0.5f).OnComplete(() => { _moving = false; }));
        }
    }
}
