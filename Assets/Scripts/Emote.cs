using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Emote : NetworkBehaviour
{
    [SerializeField]
    private GameObject emotePrefab;

    private bool hasEmote;
    private GameObject emoteSpawned;
    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.E ) )
        {
            if ( IsOwner )
            {
                SpawnEmoteServerRpc();
            }
        }
    }

    [ServerRpc( RequireOwnership = false )]
    private void SpawnEmoteServerRpc()
    {
        if ( emotePrefab == null ) return;

        if ( hasEmote )
        {
            emoteSpawned.GetComponent<NetworkObject>().Despawn();
        }
        else
        {
            GameObject emote = Instantiate( emotePrefab, transform );
            emote.GetComponent<NetworkObject>().Spawn();
            emoteSpawned = emote;
        }

        hasEmote = !hasEmote;
    }
}