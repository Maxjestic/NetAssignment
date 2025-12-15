using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PlayerNetworkPoints : NetworkBehaviour
{
    private readonly NetworkVariable<int> _pointsVar = new(0);
    public UnityAction<int> onPointsChanged;

    public int Points => _pointsVar.Value;

    public void AddPoint()
    {
        if (IsServer)
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
        onPointsChanged?.Invoke( newValue );
    }
}