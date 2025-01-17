using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    private PlayerInputActions _playerInputActions;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    private void Update()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        if ( inputVector != Vector2.zero )
        {
            Debug.Log(inputVector.magnitude);
            transform.position += new Vector3( inputVector.x, 0, inputVector.y ) * (movementSpeed * Time.deltaTime);
        }
    }
}
