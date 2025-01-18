using System;
using UnityEngine;

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
    
    private void OnDestroy()
    {
        _playerInputActions.Player.Disable();
    }

    private void Update()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        if ( inputVector == Vector2.zero ) return;

        transform.position += new Vector3( inputVector.x, 0, inputVector.y ) * ( movementSpeed * Time.deltaTime );
    }
}