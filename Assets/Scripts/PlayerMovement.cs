using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    private PlayerInputActions _playerInputActions;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Disable();
    }

    private void Update()
    {
        _playerRigidbody.linearVelocity = Vector3.zero;
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        if ( inputVector == Vector2.zero ) return;

        _playerRigidbody.linearVelocity = new Vector3( inputVector.x, 0, inputVector.y ) * movementSpeed;
    }
}