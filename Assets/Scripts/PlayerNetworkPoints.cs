using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PlayerNetworkPoints : NetworkBehaviour
{
    private NetworkVariable<int> _pointsVar = new(0, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);
    public UnityAction<int> OnPointsChanged;

    public int Points => _pointsVar.Value;

    public void CoinCollected()
    {
        if(IsOwner)
        {
            _pointsVar.Value += 1;
        }
    }
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        _pointsVar.OnValueChanged += OnPointsValueChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        _pointsVar.OnValueChanged -= OnPointsValueChanged;
    }

    private void OnPointsValueChanged( int previousValue, int newValue )
    {
        OnPointsChanged?.Invoke( newValue );
    }
}