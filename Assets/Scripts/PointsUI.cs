using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    public TMP_Text pointsText;

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
    }

    private void OnDestroy()
    {
        if ( NetworkManager.Singleton )
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnect;
        }
    }

    private void OnClientConnected( ulong clientId )
    {
        if (IsLocalClient(clientId))
        {
            NetworkObject playerNetworkObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            PlayerNetworkPoints playerNetworkPoints = playerNetworkObject.GetComponent<PlayerNetworkPoints>();

            if (playerNetworkPoints) {
                OnPlayerPointsChanged(playerNetworkPoints.Points);
                playerNetworkPoints.OnPointsChanged += OnPlayerPointsChanged;
            }
        }
    }

    private void OnClientDisconnect( ulong clientId )
    {
        if (IsLocalClient(clientId))
        {
            OnPlayerPointsChanged(-1);
        }
    }
    
    private bool IsLocalClient(ulong clientId)
    {
        return NetworkManager.Singleton.LocalClientId == clientId;
    }

    private void OnPlayerPointsChanged( int newPointsValue )
    {
        pointsText.text = newPointsValue.ToString();
    }
}
