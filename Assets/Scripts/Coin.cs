using System;
using Unity.Netcode;
using UnityEngine;

public class Coin : NetworkBehaviour
{
    private void OnTriggerEnter( Collider other )
    {
        if ( !other.TryGetComponent( out PlayerNetworkPoints playerNetworkPoints ) ) return;
        
        playerNetworkPoints.CoinCollected();
        DespawnCoinServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DespawnCoinServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}
