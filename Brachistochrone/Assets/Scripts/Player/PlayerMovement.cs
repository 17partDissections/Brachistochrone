using System.Collections;
using Q17pD.Brachistochrone.Player;
using Q17pD.Brachistochrone.PLayer;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Q17pD.Brachistochrone.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        private Vector3 _direction;
        private WaitForSeconds _sleep = new WaitForSeconds(0.5f);
        private float _horInput, _verInput;
        [SerializeField] private GameObject _rotation;

        [SerializeField] private float _jumpForce;
        [HideInInspector] public bool Grounded;

        private PlayerSounds _playerSounds;
        private AudioHandler _audioHandler;
        private PlayerActionMap _playerActionmap;

        [Inject]
        private void Construct(AudioHandler handler, PlayerActionMap map)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerSounds = GetComponentInChildren<PlayerSounds>();
            _audioHandler = handler;
            _playerActionmap = map;
            _playerActionmap.Player.Jump.performed += Jump;
            StartCoroutine(CheckMovementCoroutine());
        }
        private void FixedUpdate()
        {
            GroundedRigidbodySync(); ControlTheSpeed();
            if (_playerActionmap.Player.Move.IsPressed())
                _direction = _rotation.transform.TransformDirection(new Vector3(_playerActionmap.Player.Move.ReadValue<Vector2>().x, 0, _playerActionmap.Player.Move.ReadValue<Vector2>().y));
            else
                _direction = Vector3.zero;
            _rigidbody.AddForce(_direction.normalized * _speed * 10f, ForceMode.Force);
        }
        private IEnumerator CheckMovementCoroutine()
        {
            while (true)
            {
                if (_direction != Vector3.zero && Grounded)
                {
                    _audioHandler.PlaySFX(_playerSounds.RandomizeSound(_playerSounds.WalkSounds_Rock), 1);
                }
                yield return _sleep;
            }
        }
        void GroundedRigidbodySync()
        {
            if (Grounded)
            {
                _rigidbody.linearDamping = 5f;
            }
            else
            {
                _rigidbody.linearDamping = 0;
            }
        }
        void Jump(InputAction.CallbackContext context)
        {
            if (Grounded)
            {
                _audioHandler.PlaySFX(_playerSounds.RandomizeSound(_playerSounds.JumpSounds_Rock), 1);
                _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

            }
        }
        void ControlTheSpeed()
        {
            Vector3 currentVel = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);

            if (currentVel.magnitude > _speed)
            {
                Vector3 ControlledSpeed = currentVel.normalized * _speed;
                _rigidbody.linearVelocity = new Vector3(ControlledSpeed.x, _rigidbody.linearVelocity.y, ControlledSpeed.z);
            }
        }
    }
}
