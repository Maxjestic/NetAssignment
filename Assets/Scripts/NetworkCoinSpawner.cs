using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class NetworkCoinSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private float spawnCooldown;


    private float _spawnTimer;

    private void Update()
    {
        if ( !IsServer ) return;
        _spawnTimer += Time.deltaTime;
        if ( !( _spawnTimer >= spawnCooldown ) ) return;

        _spawnTimer = 0f;
        float x = Random.Range( -20f, 20f );
        float z = Random.Range( -20f, 20f );
        GameObject coinInstance = Instantiate( coinPrefab, new Vector3( x, 0.5f, z ), Quaternion.identity );
        var networkObject = coinInstance.GetComponent<NetworkObject>();
        networkObject?.Spawn();
    }
}