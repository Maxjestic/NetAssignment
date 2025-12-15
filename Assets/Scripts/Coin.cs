using System;
using Unity.Netcode;
using UnityEngine;

public class Coin : NetworkBehaviour
{
    private void OnTriggerEnter( Collider other )
    {
        if (!IsServer) return;

        if ( !other.TryGetComponent( out PlayerNetworkPoints playerPoints ) ) return;

        playerPoints.AddPoint();
        
        GetComponent<NetworkObject>().Despawn();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DespawnCoinServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}
