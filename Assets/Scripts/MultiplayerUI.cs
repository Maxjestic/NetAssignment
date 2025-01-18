using Unity.Netcode;
using UnityEngine;

public class MultiplayerUI : MonoBehaviour
{
    public void OnHostClick()
    {
        NetworkManager.Singleton.StartHost();
        Hide();
    }
    
    public void OnJoinClick()
    {
        NetworkManager.Singleton.StartClient();
        Hide();
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
