using InputSystem;
using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions _inputSys;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GameObject _Camera;
    [SerializeField] private float _speed = 1, _sensivityX = 1, _sensivityY = 1;
    private Coroutine _moveCoroutine, _loolCoroutine;
    private float _currentRotationAngle;


    //[Inject] private void Construct(InputSystem_Actions inputSys)
    //{
    //    _inputSys = inputSys;
    //    _inputSys.Player.Move.started += x => _moveCoroutine = StartCoroutine(MoveCoroutine());
    //    _inputSys.Player.Move.canceled += x => StartCoroutine(CheckGround());
    //    _inputSys.Player.Look.started += x => _loolCoroutine = StartCoroutine(LookCoroutine());
    //}



    private IEnumerator MoveCoroutine()
    {
        while(true)
        {
            var MoveDirection = _inputSys.Player.Move.ReadValue<Vector2>();
            var newVector = new Vector3(MoveDirection.x, 0, MoveDirection.y);
            _characterController.SimpleMove(transform.TransformVector(newVector) * _speed);

            yield return null;
        }
    }
    private IEnumerator CheckGround()
    {
        while (true)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hitlnfo, 10))
            {
                if (hitlnfo.distance <= 1.1)
                    StopAllCoroutines();
            }
            yield return null;
        }

    }
    private IEnumerator LookCoroutine()
    {
        float mouseY = _inputSys.Player.Look.ReadValue<Vector2>().y * _sensivityY;
        Debug.Log("MOUSE Y: " + mouseY);
        _currentRotationAngle += mouseY;
        _currentRotationAngle = UnityEngine.Mathf.Clamp(_currentRotationAngle, -70, 70);
        
        var LookDirection = _inputSys.Player.Look.ReadValue<Vector2>();
        transform.Rotate(Vector3.up * LookDirection.normalized.x * _sensivityX);
        float mousex = _inputSys.Player.Look.ReadValue<Vector2>().x * _sensivityX * Time.deltaTime;
        Debug.Log(mousex);
        _Camera.transform.localRotation = Quaternion.Euler(-_currentRotationAngle, 0, 0);
        
        yield return null;
    }
}
