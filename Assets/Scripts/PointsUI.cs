using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    public static PointsUI Instance;

    public TMP_Text pointsText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetTargetPlayer( PlayerNetworkPoints player )
    {
        pointsText.text = player.Points.ToString();

        player.onPointsChanged -= OnPlayerPointsChanged;
        player.onPointsChanged += OnPlayerPointsChanged;
    }

    private void OnPlayerPointsChanged( int newPointsValue )
    {
        pointsText.text = newPointsValue.ToString();
    }
}