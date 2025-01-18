using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PlayerNetworkPoints : NetworkBehaviour
{
    private NetworkVariable<int> pointsVar = new(0);
    public UnityAction<int> OnPointsChanged;

    public int Points => pointsVar.Value;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        pointsVar.OnValueChanged += OnPointsValueChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        pointsVar.OnValueChanged -= OnPointsValueChanged;
    }

    private void OnPointsValueChanged( int previousValue, int newValue )
    {
        OnPointsChanged?.Invoke( newValue );
    }

    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.J ) )
        {
            if ( IsOwner )
            {
                pointsVar.Value += 1;
            }
        }
    }
}