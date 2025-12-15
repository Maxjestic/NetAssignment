using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    private PlayerInputActions _playerInputActions;
    private Rigidbody _playerRigidbody;

    private Vector2 _inputVector;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerRigidbody = GetComponent<Rigidbody>();
    }

    public override void OnDestroy()
    {
        _playerInputActions?.Player.Disable();

        base.OnDestroy();
    }

    private void Update()
    {
        if ( !IsOwner )
        {
            return;
        }

        _inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if ( !IsOwner ) return;
        _playerRigidbody.linearVelocity = new Vector3( _inputVector.x, 0, _inputVector.y ) * movementSpeed;
    }
}