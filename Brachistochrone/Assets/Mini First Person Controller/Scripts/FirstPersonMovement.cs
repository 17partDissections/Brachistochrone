﻿using InputSystem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;

    Rigidbody rigidbody;
    private InputSystem_Actions _inputSys;

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides { get; set; } = new List<System.Func<float>>();


    [Inject] private void Construct(InputSystem_Actions inputSys)
    {
        _inputSys = inputSys;
    }


    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && _inputSys.Player.Sprint.IsPressed();

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2(_inputSys.Player.Move.ReadValue<Vector2>().x * targetMovingSpeed, _inputSys.Player.Move.ReadValue<Vector2>().y * targetMovingSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
    }
}