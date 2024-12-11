using InputSystem;
using UnityEngine;
using Zenject;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;
    private InputSystem_Actions _inputSys;

    [Inject]
    private void Construct(InputSystem_Actions inputSys)
    {
        _inputSys = inputSys;
    }
    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if (_inputSys.Player.Jump.ReadValue<float>() == 1 && (!groundCheck || groundCheck.isGrounded))
        {
            rigidbody.AddForce(Vector3.up * 25 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
