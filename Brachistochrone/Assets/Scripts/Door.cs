using DG.Tweening;
using UnityEngine;
using Zenject;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _doorLeft, _doorRight;
    private bool _moving = false;
    private EventBus _bus;

    [Inject]
    private void Construct(EventBus eventBus)
    {
        _bus = eventBus;
    }

    private void OnTriggerEnter(Collider other) { Open(); }
    private void OnTriggerExit(Collider other) { Close(); }
    private void Open()
    {
        if (_moving) { Open(); }
        else
        {
            //_bus.DoorOpened.Invoke();
            _moving = true;
            _doorLeft.DOMove(_doorLeft.position + new Vector3(0, 0, 0.985f), 0.5f);
            _doorRight.DOMove(_doorRight.position + new Vector3(0, 0, -0.985f), 0.5f).OnComplete(() => { _moving = false; });
        }

    }
    private void Close()
    {
        if (_moving) { Close(); }
        else
        {
            _moving = true;
            _doorLeft.DOMove(_doorLeft.position + new Vector3(0, 0, -0.985f), 0.5f);
            _doorRight.DOMove(_doorRight.position + new Vector3(0, 0, 0.985f), 0.5f).OnComplete(() => { _moving = false; });
        }
    }
}
