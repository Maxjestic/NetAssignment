using Unity.Netcode;
using UnityEngine;

public class ConnectionSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private float spawnRadius = 10f;

    [SerializeField]
    private LayerMask playerLayer;

    private void Start()
    {
        if ( NetworkManager.Singleton == null ) return;

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnDestroy()
    {
        if ( NetworkManager.Singleton != null )
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }

    private void OnClientConnected( ulong clientId )
    {
        if ( !NetworkManager.Singleton.IsServer ) return;
        Vector3 spawnPosition = GetRandomPosition();
        GameObject playerInstance = Instantiate( playerPrefab, spawnPosition, Quaternion.identity );
        playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject( clientId );
    }

    private Vector3 GetRandomPosition()
    {
        for ( int i = 0; i < 10; i++ )
        {
            float x = Random.Range( -spawnRadius, spawnRadius );
            float z = Random.Range( -spawnRadius, spawnRadius );
            var randomPos = new Vector3( x, 0, z );

            if ( !Physics.CheckSphere( randomPos, 1f, playerLayer ) )
            {
                return randomPos;
            }
        }

        return Vector3.zero;
    }
}