using Unity.Netcode;
using UnityEngine;

public class Emote : NetworkBehaviour
{
    [SerializeField]
    private GameObject emotePrefab;

    private bool _hasEmote;
    private GameObject _spawnedEmote;

    private void Update()
    {
        if ( !Input.GetKeyDown( KeyCode.E ) ) return;

        if ( IsOwner )
        {
            SpawnEmoteServerRpc();
        }
    }

    [ServerRpc( RequireOwnership = false )]
    private void SpawnEmoteServerRpc()
    {
        if ( !emotePrefab ) return;

        if ( _hasEmote )
        {
            if ( _spawnedEmote != null )
            {
                _spawnedEmote.GetComponent<NetworkObject>().Despawn();
            }
        }
        else
        {
            GameObject emote = Instantiate( emotePrefab, transform );

            var netObj = emote.GetComponent<NetworkObject>();
            netObj.Spawn();
            netObj.TrySetParent( transform );

            _spawnedEmote = emote;
        }

        _hasEmote = !_hasEmote;
    }
}